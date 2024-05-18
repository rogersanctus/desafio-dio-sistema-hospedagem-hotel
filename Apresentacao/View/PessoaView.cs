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
        ColoredConsole.WriteLine("Falha ao atribuir o nome.", ConsoleColor.Red);

        if (argumento != null)
        {
          ColoredConsole.WriteLine(argumento, ConsoleColor.Red);
        }
        break;
      case "AtribuirSobrenome:Sucesso":
        ColoredConsole.WriteLine("Sobrenome atribuído com sucesso.", ConsoleColor.Green);
        etapaConcluida = EtapaCriarPessoa.AtribuirSobrenome;
        break;
      case "AtribuirSobrenome:Erro":
        ColoredConsole.WriteLine("Falha ao atribuir o sobrenome.", ConsoleColor.Red);

        if (argumento != null)
        {
          ColoredConsole.WriteLine(argumento, ConsoleColor.Red);
        }
        break;
      case "AtribuirUsuario:Sucesso":
        ColoredConsole.WriteLine("Nome de Usuário atribuído com sucesso.", ConsoleColor.Green);
        etapaConcluida = EtapaCriarPessoa.AtribuirUsuario;
        break;
      case "AtribuirUsuario:Erro":
        ColoredConsole.WriteLine("Falha ao atribuir o Nome de Usuário.", ConsoleColor.Red);

        if (argumento != null)
        {
          ColoredConsole.WriteLine(argumento, ConsoleColor.Red);
        }
        break;
      case "AdicionarPessoa:Sucesso":
        ColoredConsole.WriteLine("Hóspede adicionado com sucesso.", ConsoleColor.Green);
        break;
      case "AdicionarPessoa:Erro":
        ColoredConsole.WriteLine("Falha ao adicionar a Hóspede.", ConsoleColor.Red);

        if (argumento != null)
        {
          ColoredConsole.WriteLine(argumento, ConsoleColor.Red);
        }
        break;
    }

    Console.WriteLine();
  }


  public void CriarPessoa()
  {
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
