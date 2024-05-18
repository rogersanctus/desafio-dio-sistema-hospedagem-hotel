using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Dominio.ViewModel;
using SistemaHospedagem.Lib.UI;
using SistemaHospedagem.Lib.MVVM.View;
using SistemaHospedagem.Lib.MVVM.ViewModel;

namespace SistemaHospedagem.Apresentacao.View;

enum EtapaCriarSuite
{
  Nenhuma,
  AtribuirCapacidade,
  AtribuirNome,
  AtribuirPrecoDiaria
}

public class SuiteView : ViewBase
{
  private EtapaCriarSuite etapaCriarSuite = EtapaCriarSuite.Nenhuma;

  public SuiteView(ViewModelBase viewModel) : base(viewModel) { }

  public SuiteViewModel _viewModel { get => (SuiteViewModel)this.ViewModel; }

  public override void AoNotificar(string evento, string? argumento = null)
  {
    switch (evento)
    {
      case "AtribuirNome:Sucesso":
        etapaCriarSuite = EtapaCriarSuite.AtribuirNome;
        ColoredConsole.WriteLine("Nome da suíte alterado com sucesso.", ConsoleColor.Green);
        break;
      case "AtribuirNome:Erro":
        Utils.MostrarErro("Falha ao atribuir o nome da suíte.", argumento);
        break;
      case "AtribuirCapacidade:Sucesso":
        etapaCriarSuite = EtapaCriarSuite.AtribuirCapacidade;
        ColoredConsole.WriteLine("Capacidade da suíte alterada com sucesso.", ConsoleColor.Green);
        break;
      case "AtribuirCapacidade:Erro":
        Utils.MostrarErro("Falha ao atribuir a capacidade da suíte.", argumento);
        break;
      case "AtribuirPrecoDiaria:Sucesso":
        etapaCriarSuite = EtapaCriarSuite.AtribuirPrecoDiaria;
        ColoredConsole.WriteLine("Preço da diária alterado com sucesso.", ConsoleColor.Green);
        break;
      case "AtribuirPrecoDiaria:Erro":
        Utils.MostrarErro("Falha ao atribuir o preço da diária.", argumento);
        break;
      case "AdicionarSuite:Sucesso":
        ColoredConsole.WriteLine("Suite adicionada com sucesso.", ConsoleColor.Green);
        break;
      case "AdicionarSuite:Erro":
        Utils.MostrarErro("Falha ao adicionar a suite.", argumento);
        break;
    }

    ColoredConsole.WriteLine();
  }

  private Suite Inicializar()
  {
    etapaCriarSuite = EtapaCriarSuite.Nenhuma;

    return new Suite();
  }

  private bool ChecarEtapaFinalizada(EtapaCriarSuite etapaAtual)
  {
    return etapaCriarSuite == etapaAtual;
  }

  private bool AtribuirNome(Suite suite)
  {
    ColoredConsole.Write("# Informe o nome: ");

    var nomeStr = Console.ReadLine();

    if (nomeStr == null)
    {
      ColoredConsole.WriteLine("O Nome da suíte não pode ser vazio", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    this._viewModel.AtribuirNome(suite, nomeStr);

    return ChecarEtapaFinalizada(EtapaCriarSuite.AtribuirNome);
  }


  private bool AtribuirCapacidade(Suite suite)
  {
    ColoredConsole.Write("# Informe a capacidade: ");

    var capacidadeStr = Console.ReadLine();

    if (capacidadeStr == null)
    {
      ColoredConsole.WriteLine("A capacidade da suíte não pode ser vazia", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    if (!int.TryParse(capacidadeStr, out int capacidade))
    {
      ColoredConsole.WriteLine("Capacidade da suíte inválida.", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    this._viewModel.AtribuirCapacidade(suite, capacidade);
    return ChecarEtapaFinalizada(EtapaCriarSuite.AtribuirCapacidade);
  }

  private bool AtribuirPrecoDiaria(Suite suite)
  {
    ColoredConsole.Write("# Informe o preço da diária: ");

    var precoDiariaStr = Console.ReadLine();

    if (precoDiariaStr == null)
    {
      ColoredConsole.WriteLine("O Preço da diária não pode ser vazio", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    if (!decimal.TryParse(precoDiariaStr, out decimal precoDiaria))
    {
      ColoredConsole.WriteLine("Preço da diária inválido.", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    this._viewModel.AtribuirPrecoDiaria(suite!, precoDiaria);
    return ChecarEtapaFinalizada(EtapaCriarSuite.AtribuirPrecoDiaria);
  }

  public void CriarSuite()
  {
    Suite suite = Inicializar();

    List<Func<Suite, bool>> etapas = new List<Func<Suite, bool>>
    {
      AtribuirNome,
      AtribuirCapacidade,
      AtribuirPrecoDiaria
    };

    ColoredConsole.WriteLine("Criando uma nova suite");
    ColoredConsole.WriteLine("---");
    ColoredConsole.WriteLine();

    bool success = true;
    foreach (var etapa in etapas)
    {
      success &= etapa(suite);

      if (!success)
      {
        break;
      }
    }

    if (success)
    {
      _viewModel.AdicionarSuite(suite);
    }
  }

  public void ListarSuites()
  {
    ColoredConsole.WriteLine("Listando todas as suites");
    ColoredConsole.WriteLine("---");
    ColoredConsole.WriteLine();

    foreach (var suite in _viewModel.ListaSuites.ToList())
    {
      ColoredConsole.WriteLine(suite.ToString(), ConsoleColor.Cyan);
    }

    ColoredConsole.WriteLine();
  }
}
