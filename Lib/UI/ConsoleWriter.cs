namespace SistemaHospedagem.Lib.UI;

public static class ColoredConsole
{
  public static void Write(string message, ConsoleColor color = ConsoleColor.White)
  {
    Console.ForegroundColor = color;
    Console.Write(message);
    Console.ResetColor();
  }

  public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
  {
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
  }

  public static void WriteLine() => Console.WriteLine();
}
