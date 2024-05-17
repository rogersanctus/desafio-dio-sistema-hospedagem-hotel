namespace SistemaHospedagem.Dominio.Model;

public class GerenteSuites
{
  private ConfiguracaoHotel configuracaoHotel;
  private List<Suite> _Suites = new List<Suite>();

  public List<Suite> Suites { get => _Suites; }

  public GerenteSuites(ConfiguracaoHotel configuracaoHotel)
  {
    this.configuracaoHotel = configuracaoHotel;
  }

  public void AdicionarSuite(Suite suite)
  {
    suite.Numero = this._Suites.Count + 1;

    if (!suite.IsValida)
    {
      throw new InvalidOperationException("Nem todas as configurações foram realizadas com sucesso");
    }

    var vagasUtilizadas = this._Suites.Sum(suite => suite.Capacidade);

    if (vagasUtilizadas + suite.Capacidade > this.configuracaoHotel.QuatidadeVagas)
    {
      throw new InvalidOperationException("Vagas insuficientes");
    }

    this._Suites.Add(suite);
  }
}
