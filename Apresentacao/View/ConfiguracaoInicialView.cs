using SistemaHospedagem.Dominio.ViewModel;
using SistemaHospedagem.Lib.UI;
using SistemaHospedagem.Lib.MVVM.View;
using SistemaHospedagem.Lib.MVVM.ViewModel;

namespace SistemaHospedagem.Apresentacao.View;


public class ConfiguracaoInicialView : ViewBase
{
  private bool configuracaoInicial = true;
  private bool configuracaoRealizada = false;
  private ConfiguracaoInicialViewModel _viewModel
  {
    get => (ConfiguracaoInicialViewModel)this.ViewModel;
  }

  public ConfiguracaoInicialView(ViewModelBase viewModel) : base(viewModel)
  {
  }

  public override void AoNotificar(string evento, string? parametro = null)
  {
    switch (evento)
    {
      case "AtualizarQuantidadeVagas:Sucesso":
        configuracaoRealizada = true;
        configuracaoInicial = false;

        ColoredConsole.WriteLine("Configuração realizada com sucesso.", ConsoleColor.Green);
        break;
      case "AtualizarQuantidadeVagas:Erro":
        configuracaoRealizada = false;

        Utils.MostrarErro("Falha ao atualizar a quantidade de vagas do Hotel.", parametro);
        break;
    }

    ColoredConsole.WriteLine();
  }

  public void ConfigurarHotel()
  {
    ColoredConsole.WriteLine("Configurando o Hotel");
    ColoredConsole.WriteLine("---");
    ColoredConsole.WriteLine();

    ConfigurarQuantidadeVagas();

    if (configuracaoInicial && !configuracaoRealizada)
    {
      ColoredConsole.WriteLine("Por algum motivo, a configuração do Hotel não foi realizada. De todo modo, você pode realizá-la novamente pelo Menu.", ConsoleColor.Yellow);
      ColoredConsole.Write("Para isso acesse a opção: ", ConsoleColor.Yellow);
      ColoredConsole.WriteLine("Configurar Capacidade Geral do Hotel", ConsoleColor.Blue);
      ColoredConsole.WriteLine();
    }
  }

  private bool ConfigurarQuantidadeVagas()
  {
    ColoredConsole.Write("Informe a quantidade de vagas em TODO o hotel: ");

    var quatidadeVagasStr = Console.ReadLine();

    if (string.IsNullOrEmpty(quatidadeVagasStr))
    {
      ColoredConsole.WriteLine("A quantidade de vagas não pode ser vazia.", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    if (!int.TryParse(quatidadeVagasStr, out int quatidadeVagas))
    {
      ColoredConsole.WriteLine("Quantidade de vagas inválida. Precisa ser um inteiro.", ConsoleColor.Red);
      ColoredConsole.WriteLine();
      return false;
    }

    _viewModel.AtualizarQuantidadeVagas(quatidadeVagas);
    return true;
  }

  public void ExibirQuantidadeVagas()
  {
    ColoredConsole.WriteLine("Exibindo Capacidade de Vagas Geral do Hotel");
    ColoredConsole.WriteLine("---");

    ColoredConsole.WriteLine($"Capacidade: {this._viewModel.ObterQuantidadeVagas()}", ConsoleColor.Cyan);
    Console.WriteLine();
  }

}
