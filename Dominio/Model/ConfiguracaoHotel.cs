namespace SistemaHospedagem.Dominio.Model;

public class ConfiguracaoHotel
{
  public int QuatidadeVagas { get; internal set; } = 0;

  public void ConfigurarVagas(int quatidadeVagas)
  {
    if (quatidadeVagas <= 0)
    {
      throw new ArgumentException("A quantidade de vagas deve ser maior que zero");
    }

    this.QuatidadeVagas = quatidadeVagas;
  }
}
