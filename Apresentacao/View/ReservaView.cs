namespace SistemaHospedagem.Apresentacao.View;

using SistemaHospedagem.Lib.MVVM.View;
using SistemaHospedagem.Lib.MVVM.ViewModel;
using SistemaHospedagem.Dominio.ViewModel;
using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Lib.UI;

public class ReservaView : ViewBase
{
  private Reserva? reservaAtual = null;
  public ReservaView(ViewModelBase viewModel) : base(viewModel) { }

  public ReservaViewModel _viewModel { get => (ReservaViewModel)this.ViewModel; }

  public override void AoNotificar(string evento, string? argumento = null)
  {
    switch (evento)
    {
      case "AtribuirSuiteReserva:Sucesso":
        ColoredConsole.WriteLine("Reserva efetuada com sucesso.", ConsoleColor.Green);
        ColoredConsole.WriteLine();
        break;
      case "AtribuirSuiteReserva:Erro":
        ColoredConsole.WriteLine("Falha ao atribuir a suíte", ConsoleColor.Red);
        ColoredConsole.WriteLine();
        break;
    }
  }

  private void IniciarReserva()
  {
    if (this.reservaAtual != null)
    {
      ColoredConsole.WriteLine("Uma reserva já está sendo efetuada. Uma nova será iniciada do início");
    }

    reservaAtual = new Reserva();
  }


  public void AtribuirSuite()
  {
    ColoredConsole.WriteLine("Atribuindo nova suite");
    ColoredConsole.WriteLine("---");
    ColoredConsole.WriteLine();

    ColoredConsole.Write("Informe o Número da suíte: ");
    var numeroSuiteStr = Console.ReadLine();

    if (int.TryParse(numeroSuiteStr, out int numeroSuite))
    {
      this._viewModel.AtribuirSuiteReserva(this.reservaAtual!, numeroSuite);
    }
    else
    {
      ColoredConsole.WriteLine("Número da suíte inválido", ConsoleColor.Red);
      ColoredConsole.WriteLine();
    }
  }

}
