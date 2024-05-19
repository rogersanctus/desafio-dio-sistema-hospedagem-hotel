using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Dominio.ViewModel;
using SistemaHospedagem.Lib.MVVM.View;
using SistemaHospedagem.Lib.MVVM.ViewModel;
using SistemaHospedagem.Lib.UI;

namespace SistemaHospedagem.Apresentacao.View;

enum EtapaCriarPessoa
{
  Nenhuma,
  AtribuirNome,
  AtribuirSobrenome,
  AtribuirUsuario,
  AdicionarPessoa
}

public class PessoaView : ViewBase
{
  private EtapaCriarPessoa etapaConcluida = EtapaCriarPessoa.Nenhuma;
  public PessoaView(ViewModelBase viewModel) : base(viewModel) { }
  public PessoaViewModel _viewModel { get => (PessoaViewModel)this.ViewModel; }
  private int etapaCriarPessoa = 0;

  public override void AoNotificar(string evento, string? argumento = null)
  {
    switch (evento)
    {
      case "AtribuirNome:Sucesso":
        ColoredConsole.WriteLine("Nome atribuído com sucesso.", ConsoleColor.Green);
        etapaConcluida = EtapaCriarPessoa.AtribuirNome;
        break;
      case "AtribuirNome:Erro":
        Utils.MostrarErro("Falha ao atribuir o Nome.", argumento);
        break;
      case "AtribuirSobrenome:Sucesso":
        ColoredConsole.WriteLine("Sobrenome atribuído com sucesso.", ConsoleColor.Green);
        etapaConcluida = EtapaCriarPessoa.AtribuirSobrenome;
        break;
      case "AtribuirSobrenome:Erro":
        Utils.MostrarErro("Falha ao atribuir o Sobrenome.", argumento);
        break;
      case "AtribuirUsuario:Sucesso":
        ColoredConsole.WriteLine("Nome de Usuário atribuído com sucesso.", ConsoleColor.Green);
        etapaConcluida = EtapaCriarPessoa.AtribuirUsuario;
        break;
      case "AtribuirUsuario:Erro":
        Utils.MostrarErro("Falha ao atribuir o Nome de Usuário.", argumento);
        break;
      case "AdicionarPessoa:Sucesso":
        ColoredConsole.WriteLine("Hóspede adicionado com sucesso.", ConsoleColor.Green);
        break;
      case "AdicionarPessoa:Erro":
        Utils.MostrarErro("Falha ao adicionar a Hóspede.", argumento);
        break;
    }

    Console.WriteLine();
  }


  public void CriarPessoa()
  {
    etapaConcluida = EtapaCriarPessoa.Nenhuma;
    Pessoa pessoa = new Pessoa();

    ColoredConsole.WriteLine("Criando novo Hóspede");
    ColoredConsole.WriteLine("---");
    ColoredConsole.WriteLine();

    List<Func<Pessoa, bool>> etapas = new List<Func<Pessoa, bool>> {
      AtribuirNome,
      AtribuirSobrenome,
      AtribuirUsuario
    };

    bool success = true;
    foreach (var etapa in etapas)
    {
      success = etapa(pessoa);

      if (!success)
      {
        break;
      }
    }

    if (success)
    {
      _viewModel.AdicionarPessoa(pessoa);
    }
  }

  public void ListarPessoas()
  {
    var pessoas = _viewModel.ListaPessoas();

    ColoredConsole.WriteLine("Listando Hóspedes");
    ColoredConsole.WriteLine("---");
    ColoredConsole.WriteLine();

    if (pessoas.Count == 0)
    {
      ColoredConsole.WriteLine("Nenhum Hóspede cadastrado.", ConsoleColor.Cyan);
      ColoredConsole.WriteLine();
      return;
    }

    foreach (var pessoa in pessoas)
    {
      ColoredConsole.WriteLine($"{pessoa.ToString()}", ConsoleColor.Cyan);
      ColoredConsole.WriteLine();
    }
  }

  private bool AtribuirNome(Pessoa pessoa)
  {
    ColoredConsole.Write("# Informe o Nome: ");

    var nomePessoa = Console.ReadLine();

    if (nomePessoa == null)
    {
      ColoredConsole.WriteLine("Nenhum Nome foi passado.", ConsoleColor.Red);
      Console.WriteLine();
      return false;
    }

    _viewModel.AtribuirNome(pessoa, nomePessoa);

    return etapaConcluida == EtapaCriarPessoa.AtribuirNome;
  }

  private bool AtribuirSobrenome(Pessoa pessoa)
  {
    ColoredConsole.Write("# Informe o Sobrenome: ");

    var sobrenomePessoa = Console.ReadLine();

    if (sobrenomePessoa == null)
    {
      ColoredConsole.WriteLine("Nenhum Sobrenome foi passado.", ConsoleColor.Red);
      Console.WriteLine();
      return false;
    }

    _viewModel.AtribuirSobrenome(pessoa, sobrenomePessoa);

    return etapaConcluida == EtapaCriarPessoa.AtribuirSobrenome;
  }

  private bool AtribuirUsuario(Pessoa pessoa)
  {
    ColoredConsole.Write("# Informe o Nome de Usuário (login): ");

    var nomeUsuario = Console.ReadLine();

    if (nomeUsuario == null)
    {
      ColoredConsole.WriteLine("Nenhum Nome de Usuário foi passado.", ConsoleColor.Red);
      Console.WriteLine();
      return false;
    }

    _viewModel.AtribuirUsuario(pessoa, nomeUsuario);

    return etapaConcluida == EtapaCriarPessoa.AtribuirUsuario;
  }
}
