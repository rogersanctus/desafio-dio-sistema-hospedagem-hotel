namespace SistemaHospedagem.Apresentacao.View;

using SistemaHospedagem.Lib.MVVM.View;
using SistemaHospedagem.Lib.MVVM.ViewModel;
using SistemaHospedagem.Dominio.ViewModel;
using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Lib.UI;
using SistemaHospedagem.Lib.Mediator;
using System.Text.Json;

enum EtapaCriarReserva
{
  Nenhuma,
  AtribuirSuite,
  AdicionarHospedes,
  AtribuirDiasDaReserva,
}

public class ReservaView : ViewBase, IObserver
{
  private EtapaCriarReserva etapaConcluida = EtapaCriarReserva.Nenhuma;
  private PessoaView pessoaView;
  private Mediator mediator;
  private Pessoa? hospedeParaAdicionar = null;

  public ReservaViewModel _viewModel { get => (ReservaViewModel)this.ViewModel; }

  public ReservaView(ViewModelBase viewModel, PessoaView pessoaView, Mediator mediator) : base(viewModel)
  {
    this.pessoaView = pessoaView;
    this.mediator = mediator;

    this.mediator.AddObserver(this);
  }

  public override void AoNotificar(string evento, string? argumento = null)
  {
    switch (evento)
    {
      case "AtribuirSuiteReserva:Sucesso":
        etapaConcluida = EtapaCriarReserva.AtribuirSuite;
        ColoredConsole.WriteLine("Suite atribuída com sucesso.", ConsoleColor.Green);
        break;
      case "AtribuirSuiteReserva:Erro":
        Utils.MostrarErro("Falha ao atribuir a suíte", argumento);
        break;

      case "AdicionarReserva:Sucesso":
        ColoredConsole.WriteLine("Reserva efetuada com sucesso.", ConsoleColor.Green);
        break;
      case "AdicionarReserva:Erro":
        Utils.MostrarErro("Falha ao efetuar a reserva.", argumento);
        break;
      case "AdicionarHospedeReserva:Sucesso":
        ColoredConsole.WriteLine("Hospede adicionado com sucesso.", ConsoleColor.Green);
        break;
      case "AdicionarHospedeReserva:Erro":
        Utils.MostrarErro("Falha ao adicionar o hospede à reserva.", argumento);
        break;

      case "RemoverHospedeReserva:Sucesso":
        ColoredConsole.WriteLine("Hospede removido da reserva com sucesso.", ConsoleColor.Green);
        break;
      case "RemoverHospedeReserva:Erro":
        Utils.MostrarErro("Falha ao remover o hospede da reserva.", argumento);
        break;

      case "AtribuirDiasReserva:Sucesso":
        ColoredConsole.WriteLine("Dias da reserva atribuídos com sucesso.", ConsoleColor.Green);
        etapaConcluida = EtapaCriarReserva.AtribuirDiasDaReserva;
        break;
      case "AtribuirDiasReserva:Erro":
        Utils.MostrarErro("Falha ao atribuir os dias da reserva.", argumento);
        break;
    }

    ColoredConsole.WriteLine();
  }

  public void OnNotifyObserver(string @event, string? argument = null)
  {
    if (@event == "AdicionarPessoa:Sucesso")
    {
      ColoredConsole.WriteLine("Reserva::HospedeAdicionado", ConsoleColor.DarkGreen);
      Pessoa? pessoa = JsonSerializer.Deserialize<Pessoa>(argument!);

      if (pessoa == null)
      {
        ColoredConsole.WriteLine("Erro ao resgatar argumento de AdicionarPessoa:Sucesso como Pessoa", ConsoleColor.Red);
      }

      hospedeParaAdicionar = pessoa;
    }
    else if (@event == "AdicionarPessoa:Erro")
    {
      hospedeParaAdicionar = null;
    }
  }

  public void CriarReserva()
  {
    etapaConcluida = EtapaCriarReserva.Nenhuma;
    Reserva reserva = new Reserva();

    ColoredConsole.WriteLine("Criando uma nova reserva");
    ColoredConsole.WriteLine("---");
    ColoredConsole.WriteLine();

    List<Func<Reserva, bool>> etapas = new List<Func<Reserva, bool>>() {
      AtribuirSuite,
      AdicionarHospedes,
      AtribuirDiasDaReserva
    };

    bool success = true;
    foreach (var etapa in etapas)
    {
      success = etapa(reserva);

      if (!success)
      {
        break;
      }
    }

    if (success)
    {
      this._viewModel.AdicionarReserva(reserva);
    }
  }

  private bool AtribuirSuite(Reserva reserva)
  {
    ColoredConsole.Write("# Informe o Número da suíte: ");
    var numeroSuiteStr = Console.ReadLine();

    if (int.TryParse(numeroSuiteStr, out int numeroSuite))
    {
      this._viewModel.AtribuirSuiteReserva(reserva, numeroSuite);
    }
    else
    {
      ColoredConsole.WriteLine("Número da suíte inválido", ConsoleColor.Red);
      ColoredConsole.WriteLine();

      return false;
    }

    return etapaConcluida == EtapaCriarReserva.AtribuirSuite;
  }

  private bool AdicionarHospedes(Reserva reserva)
  {
    bool voltar = false;

    while (!voltar)
    {
      ColoredConsole.WriteLine("Menu de Hóspedes x Reserva");
      ColoredConsole.WriteLine("***");
      ColoredConsole.WriteLine();

      ColoredConsole.WriteLine("1 - Adicionar Hóspede");
      ColoredConsole.WriteLine("2 - Remover Hóspede");
      ColoredConsole.WriteLine("3 - Listar Hóspede");
      ColoredConsole.WriteLine();
      ColoredConsole.WriteLine("0 - Continuar reserva");
      ColoredConsole.WriteLine();

      ColoredConsole.Write("Escolha uma opção: ");
      var opcao = Console.ReadLine();

      ColoredConsole.WriteLine();

      switch (opcao)
      {
        case "1":
          if (reserva.IsCapacidadeAlcancada)
          {
            ColoredConsole.WriteLine("Capacidade da Suite alcancada. Não é possível adicionar outro hóspede.", ConsoleColor.Yellow);
            ColoredConsole.WriteLine();
            break;
          }

          AdicionarHospede(reserva);
          break;
        case "2":
          this.RemoverHospede(reserva);
          break;
        case "3":
          this.ListarHospedes(reserva);
          break;
        case "0":
          if (reserva.Hospedes.Count == 0)
          {
            ColoredConsole.WriteLine("É necessário ao menos um hóspede para fazer a reserva e nenhum Hóspede foi adicionado. Tem certeza que deseja continuar?", ConsoleColor.Cyan);
            ColoredConsole.Write("S/N: ");

            var resposta = Console.ReadLine();

            ColoredConsole.WriteLine();

            if (resposta?.ToLower() == "s")
            {
              ColoredConsole.WriteLine("Reserva descartada", ConsoleColor.Yellow);
              ColoredConsole.WriteLine();
              voltar = true;
              break;
            }
          }
          else
          {
            voltar = true;
          }
          break;
      }
    }

    return reserva.Hospedes.Count > 0;
  }

  private void AdicionarHospede(Reserva reserva)
  {
    ColoredConsole.WriteLine("# Informe o Nome de Usuário (login) do Hóspede ou deixe em branco para cadastrar um novo Hóspede");
    ColoredConsole.Write(": ");

    var usuarioHospede = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(usuarioHospede))
    {
      pessoaView.CriarPessoa();

      if (hospedeParaAdicionar != null)
      {
        _viewModel.AdicionarHospedeReserva(reserva, hospedeParaAdicionar);
      }
    }
    else
    {
      var hospede = _viewModel.ObterPessoaPorUsuario(usuarioHospede);

      if (hospede == null)
      {
        ColoredConsole.WriteLine("Hóspede não encontrado. Tente novamente", ConsoleColor.Red);
        ColoredConsole.WriteLine();
      }
      else
      {
        _viewModel.AdicionarHospedeReserva(reserva, hospede);
      }
    }
  }

  private void RemoverHospede(Reserva reserva)
  {
    ColoredConsole.WriteLine("Removendo Hóspede");
    ColoredConsole.WriteLine("***");
    ColoredConsole.Write("# Informe o nome de Usuário: ");

    var nomeUsuario = Console.ReadLine();

    if (string.IsNullOrEmpty(nomeUsuario))
    {
      ColoredConsole.WriteLine("Nome de usuário não foi informado.", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return;
    }

    _viewModel.RemoverHospedeReserva(reserva, nomeUsuario);
  }

  private void ListarHospedes(Reserva reserva)
  {
    ColoredConsole.WriteLine("Listando Hóspedes");
    ColoredConsole.WriteLine("***");
    ColoredConsole.WriteLine();

    if (reserva.Hospedes.Count > 0)
    {
      reserva.Hospedes.ForEach(hospede => ColoredConsole.WriteLine(hospede.ToString(), ConsoleColor.Cyan));
    }
    else
    {
      ColoredConsole.WriteLine("Nenhum hóspede na Reserva", ConsoleColor.Cyan);
    }

    ColoredConsole.WriteLine();
  }

  private bool AtribuirDiasDaReserva(Reserva reserva)
  {
    ColoredConsole.WriteLine("Atribuindo dias da Reserva");
    ColoredConsole.WriteLine("***");
    ColoredConsole.WriteLine();
    ColoredConsole.Write("# Dias da Reserva: ");

    var diasStr = Console.ReadLine();

    ColoredConsole.WriteLine();

    if (string.IsNullOrEmpty(diasStr))
    {
      ColoredConsole.WriteLine("Dias da Reserva não foi informado.", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    if (!int.TryParse(diasStr, out int dias))
    {
      ColoredConsole.WriteLine("Dias da reserva precisam ser um número inteiro.");
      ColoredConsole.WriteLine();
      return false;
    }

    _viewModel.AtribuirDiasReserva(reserva, dias);

    return etapaConcluida == EtapaCriarReserva.AtribuirDiasDaReserva;
  }
}
