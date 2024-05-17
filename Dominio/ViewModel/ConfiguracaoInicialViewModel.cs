namespace SistemaHospedagem.Dominio.ViewModel;

using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Lib.MVVM.ViewModel;

public class ConfiguracaoInicialViewModel : ViewModelBase
{
  private ConfiguracaoHotel configuracaoHotel;

  public ConfiguracaoInicialViewModel(ConfiguracaoHotel configuracaoHotel)
  {
    this.configuracaoHotel = configuracaoHotel;
  }

  public void AtualizarQuantidadeVagas(int quatidadeVagas)
  {
    try
    {
      this.configuracaoHotel.ConfigurarVagas(quatidadeVagas);
      this.NotificarView("AtualizarQuantidadeVagas:Sucesso");
    }
    catch (ArgumentException ex)
    {
      NotificarView("AtualizarQuantidadeVagas:Erro", ex.Message);

    }
  }

  public int ObterQuantidadeVagas()
  {
    return this.configuracaoHotel.QuatidadeVagas;
  }
}
