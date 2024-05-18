using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Dominio.ViewModel;
using SistemaHospedagem.Lib.UI;
using SistemaHospedagem.Lib.MVVM.View;
using SistemaHospedagem.Lib.MVVM.ViewModel;

namespace SistemaHospedagem.Apresentacao.View;

public class SuiteView : ViewBase
{
  private Suite? suiteAtual = null;

  public SuiteView(ViewModelBase viewModel) : base(viewModel) { }

  public SuiteViewModel _viewModel { get => (SuiteViewModel)this.ViewModel; }

  public override void AoNotificar(string evento, string? argumento = null)
  {
    switch (evento)
    {
      case "AtribuirNome:Sucesso":
        ColoredConsole.WriteLine("Nome da suíte alterado com sucesso.", ConsoleColor.Green);
        break;
      case "AtribuirNome:Erro":
        Utils.MostrarErro("Falha ao atribuir o nome da suíte.", argumento);
        break;
      case "AtribuirCapacidade:Sucesso":
        ColoredConsole.WriteLine("Capacidade da suíte alterada com sucesso.", ConsoleColor.Green);
        break;
      case "AtribuirCapacidade:Erro":
        Utils.MostrarErro("Falha ao atribuir a capacidade da suíte.", argumento);
        break;
      case "AtribuirPrecoDiaria:Sucesso":
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

  private void Inicializar()
  {
    if (this.suiteAtual != null)
    {
      ColoredConsole.WriteLine("Uma suite já estava sendo criada. Uma nova será iniciada do início");
    }

    this.suiteAtual = new Suite();
  }

  private bool ChecarInicializacaoSuite()
  {
    if (suiteAtual == null)
    {
      ColoredConsole.WriteLine("Nenhuma suite foi inicializada ainda", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    return true;
  }

  private bool ChecarEtapaFinalizada(int etapaAtual)
  {
    int result = suiteAtual!.EtapasConfiguradas & etapaAtual;

    return result == etapaAtual;
  }

  private bool AtribuirNome()
  {
    if (!ChecarInicializacaoSuite())
    {
      return false;
    }

    ColoredConsole.WriteLine("# Atribuindo nome à suíte");
    ColoredConsole.WriteLine("Informe o nome: ");

    var nomeStr = Console.ReadLine();

    if (string.IsNullOrEmpty(nomeStr))
    {
      ColoredConsole.WriteLine("O Nome da suíte não pode ser vazio", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    this._viewModel.AtribuirNome(suiteAtual!, nomeStr);

    return ChecarEtapaFinalizada(Suite.ETAPA_NOME);
  }


  private bool AtribuirCapacidade()
  {
    if (!ChecarInicializacaoSuite())
    {
      return false;
    }

    ColoredConsole.WriteLine("# Atribuindo capacidade da suíte");
    ColoredConsole.WriteLine("Informe a capacidade: ");

    var capacidadeStr = Console.ReadLine();

    if (string.IsNullOrEmpty(capacidadeStr))
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

    this._viewModel.AtribuirCapacidade(suiteAtual!, capacidade);
    return ChecarEtapaFinalizada(Suite.ETAPA_CAPACIDADE);
  }

  private bool AtribuirPrecoDiaria()
  {
    if (!ChecarInicializacaoSuite())
    {
      return false;
    }

    ColoredConsole.WriteLine("# Atribuindo o preço da diária");
    ColoredConsole.Write("Informe o preço da diária: ");

    var precoDiariaStr = Console.ReadLine();

    if (string.IsNullOrEmpty(precoDiariaStr))
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

    this._viewModel.AtribuirPrecoDiaria(suiteAtual!, precoDiaria);
    return ChecarEtapaFinalizada(Suite.ETAPA_PRECO_DIARIA);
  }

  public void CriarSuite()
  {
    Inicializar();

    List<Func<bool>> etapas = new List<Func<bool>>
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
      success &= etapa();

      if (!success)
      {
        break;
      }
    }

    if (success)
    {
      _viewModel.AdicionarSuite(suiteAtual!);
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
