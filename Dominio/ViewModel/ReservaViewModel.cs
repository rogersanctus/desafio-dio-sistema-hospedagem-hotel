namespace SistemaHospedagem.Dominio.ViewModel;

using System.Collections.Generic;

using SistemaHospedagem.Lib.MVVM.ViewModel;
using SistemaHospedagem.Dominio.Model;

public class ReservaViewModel : ViewModelBase
{
  private List<Reserva> reservas = new List<Reserva>();
  private GerenteSuites gerenteSuites;
  private GerentePessoas gerentePessoas;

  public ReservaViewModel(GerenteSuites gerenteSuites, GerentePessoas gerentePessoas)
  {
    this.gerenteSuites = gerenteSuites;
    this.gerentePessoas = gerentePessoas;
  }

  public List<Suite> ListaSuitesDisponiveis()
  {
    return this.gerenteSuites.Suites
      .Where(suite =>
      {
        return !this.reservas.Any(reserva =>
        {
          return reserva.Suite == suite;
        });
      })
      .ToList();
  }

  public void AtribuirSuiteReserva(Reserva reserva, int numeroSuite)
  {
    List<Suite> suitesDisponiveis = this.ListaSuitesDisponiveis();
    Suite? suite = suitesDisponiveis.Find(suite => suite.Numero == numeroSuite);

    if (suite == null)
    {
      NotificarViews("AtribuirSuiteReserva:Erro", "Esta suíte não está disponível ou não existe");
      return;
    }

    try
    {
      reserva.Suite = suite;

      this.NotificarViews("AtribuirSuiteReserva:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarViews("AtribuirSuiteReserva:Erro", ex.Message);
    }
  }

  public void AtribuirDiasReserva(Reserva reserva, int dias)
  {

    try
    {
      reserva.DiasReserva = dias;
      this.NotificarViews("AtribuirDiasReserva:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarViews("AtribuirDiasReserva:Erro", ex.Message);
    }
  }

  public void AdicionarHospedeReserva(Reserva reserva, Pessoa pessoa)
  {
    try
    {
      reserva.AdicionarHospede(pessoa);
      this.NotificarViews("AdicionarHospedeReserva:Sucesso");
    }
    catch (InvalidOperationException ex)
    {
      this.NotificarViews("AdicionarHospedeReserva:Erro", ex.Message);
    }
  }

  public void RemoverHospedeReserva(Reserva reserva, string nomeUsuario)
  {
    try
    {
      reserva.RemoverHospede(nomeUsuario);
      this.NotificarViews("RemoverHospedeReserva:Sucesso");
    }
    catch (KeyNotFoundException ex)
    {
      this.NotificarViews("RemoverHospedeReserva:Erro", ex.Message);
    }
  }

  public void AdicionarReserva(Reserva reserva)
  {
    if (!reserva.IsValida)
    {
      NotificarViews("AdicionarReserva:Erro", "Reserva inválida");
      return;
    }

    try
    {
      this.reservas.Add(reserva);
      NotificarViews("AdicionarReserva:Sucesso");
    }
    catch (InvalidOperationException ex)
    {
      this.NotificarViews("AdicionarReserva:Erro", ex.Message);
    }
  }

  public Pessoa? ObterPessoaPorUsuario(string nomeUsuario)
  {
    return this.gerentePessoas.Pessoas.FirstOrDefault(pessoa => pessoa.Usuario == nomeUsuario);
  }
}

