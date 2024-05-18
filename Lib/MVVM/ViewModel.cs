using SistemaHospedagem.Lib.MVVM.View;

namespace SistemaHospedagem.Lib.MVVM.ViewModel;

public class ViewModelBase
{
  protected virtual List<ViewBase> Views { get; private set; } = new List<ViewBase>();

  public void AdicionarView(ViewBase view)
  {
    this.Views.Add(view);
  }

  protected void NotificarViews(string evento, string? argumento = null)
  {
    Views.ForEach(view => view.AoNotificar(evento, argumento));
  }
}
