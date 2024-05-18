using SistemaHospedagem.Lib.MVVM.ViewModel;

namespace SistemaHospedagem.Lib.MVVM.View;

public abstract class ViewBase
{
  protected virtual ViewModelBase ViewModel { get; private set; }

  protected ViewBase(ViewModelBase viewModel)
  {
    this.ViewModel = viewModel;
    this.ViewModel.AdicionarView(this);
  }

  public abstract void AoNotificar(string evento, string? argumento = null);
}
