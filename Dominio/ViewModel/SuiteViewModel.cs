using System.Collections.ObjectModel;
using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Lib.MVVM.ViewModel;

namespace SistemaHospedagem.Dominio.ViewModel;

public class SuiteViewModel : ViewModelBase
{
  private GerenteSuites gerenteSuites;

  public SuiteViewModel(GerenteSuites gerenteSuites)
  {
    this.gerenteSuites = gerenteSuites;
  }

  public void AtribuirNome(Suite suite, string nome)
  {
    try
    {
      suite.Nome = nome;
      this.NotificarView("AtribuirNome:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarView("AtribuirNome:Erro", ex.Message);
    }
  }

  public void AtribuirCapacidade(Suite suite, int capacidade)
  {
    try
    {
      suite.Capacidade = capacidade;
      this.NotificarView("AtribuirCapacidade:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarView("AtribuirCapacidade:Erro", ex.Message);
    }
  }

  public void AtribuirPrecoDiaria(Suite suite, decimal precoDiaria)
  {
    try
    {
      suite.PrecoDiaria = precoDiaria;
      this.NotificarView("AtribuirPrecoDiaria:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarView("AtribuirPrecoDiaria:Erro", ex.Message);
    }
  }

  public void AdicionarSuite(Suite suite)
  {
    try
    {
      this.gerenteSuites.AdicionarSuite(suite);
      this.NotificarView("AdicionarSuite:Sucesso");
    }
    catch (InvalidOperationException ex)
    {
      this.NotificarView("AdicionarSuite:Erro", ex.Message);
    }
  }

  public ReadOnlyCollection<Suite> ListaSuites
  {
    get => this.gerenteSuites.Suites.AsReadOnly();
  }
}
