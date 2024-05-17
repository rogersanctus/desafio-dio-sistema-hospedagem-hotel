using System.Globalization;
using SistemaHospedagem.Apresentacao.View;
using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Dominio.ViewModel;
using SistemaHospedagem.Lib.UI;

var configuracaoHotel = new ConfiguracaoHotel();
var gerenteSuites = new GerenteSuites(configuracaoHotel);

var configuracaoInicialViewModel = new ConfiguracaoInicialViewModel(configuracaoHotel);
var suiteViewModel = new SuiteViewModel(gerenteSuites);

var configuracaoInicialView = new ConfiguracaoInicialView(configuracaoInicialViewModel);
var suiteView = new SuiteView(suiteViewModel);

bool sair = false;

var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

Console.Clear();

Console.WriteLine("Sistema de Hospedagem de Hotel");
Console.WriteLine("==============================");

Console.WriteLine();
ColoredConsole.WriteLine($"Importante: O separador decimal para valores de preço é o caractere: '{decimalSeparator}'", ConsoleColor.Cyan);
Console.WriteLine();

configuracaoInicialView.ConfigurarHotel();

while (!sair)
{
  Console.WriteLine("Menu Principal");
  Console.WriteLine("---");

  Console.WriteLine("# Configuração Hotel");
  Console.WriteLine("1 - Configurar Quantidade de Vagas no Hotel");
  Console.WriteLine("2 - Criar Suíte");
  Console.WriteLine("3 - Listar Suítes");
  Console.WriteLine();
  Console.WriteLine("# Reservas");
  Console.WriteLine();
  Console.WriteLine("0 - Sair");
  Console.WriteLine();

  var input = Console.ReadLine();
  Console.WriteLine();

  switch (input)
  {
    case "0":
      sair = true;
      break;
    case "1":
      configuracaoInicialView.ConfigurarHotel();
      break;
    case "2":
      suiteView.CriarSuite();
      break;
    case "3":
      suiteView.ListarSuites();
      break;
    default:
      ColoredConsole.WriteLine("Opção Inválida", ConsoleColor.Red);
      break;
  }
}
