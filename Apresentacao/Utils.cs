using SistemaHospedagem.Lib.UI;

namespace SistemaHospedagem.Apresentacao;

public static class Utils
{
  public static void MostrarErro(string mensagem, string? argumento)
  {
    ColoredConsole.WriteLine(mensagem, ConsoleColor.Red);

    if (argumento != null)
    {
      ColoredConsole.WriteLine(argumento, ConsoleColor.Red);
    }
  }
}
