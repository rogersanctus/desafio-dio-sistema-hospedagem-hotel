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
      this.NotificarViews("AtribuirNome:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarViews("AtribuirNome:Erro", ex.Message);
    }
  }

  public void AtribuirCapacidade(Suite suite, int capacidade)
  {
    try
    {
      suite.Capacidade = capacidade;
      this.NotificarViews("AtribuirCapacidade:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarViews("AtribuirCapacidade:Erro", ex.Message);
    }
  }

  public void AtribuirPrecoDiaria(Suite suite, decimal precoDiaria)
  {
    try
    {
      suite.PrecoDiaria = precoDiaria;
      this.NotificarViews("AtribuirPrecoDiaria:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarViews("AtribuirPrecoDiaria:Erro", ex.Message);
    }
  }

  public void AdicionarSuite(Suite suite)
  {
    try
    {
      this.gerenteSuites.AdicionarSuite(suite);
      this.NotificarViews("AdicionarSuite:Sucesso");
    }
    catch (InvalidOperationException ex)
    {
      this.NotificarViews("AdicionarSuite:Erro", ex.Message);
    }
  }

  public ReadOnlyCollection<Suite> ListaSuites
  {
    get => this.gerenteSuites.Suites.AsReadOnly();
  }
}
