using System.Globalization;
using SistemaHospedagem.Apresentacao.View;
using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Dominio.ViewModel;
using SistemaHospedagem.Lib.Mediator;
using SistemaHospedagem.Lib.UI;

var configuracaoHotel = new ConfiguracaoHotel();
var gerenteSuites = new GerenteSuites(configuracaoHotel);
var gerentePessoas = new GerentePessoas();

var mediator = new Mediator();

var configuracaoInicialViewModel = new ConfiguracaoInicialViewModel(configuracaoHotel);
var suiteViewModel = new SuiteViewModel(gerenteSuites);
var pessoaViewModel = new PessoaViewModel(gerentePessoas, mediator);
var reservaViewModel = new ReservaViewModel(gerenteSuites, gerentePessoas);

var configuracaoInicialView = new ConfiguracaoInicialView(configuracaoInicialViewModel);
var suiteView = new SuiteView(suiteViewModel);
var pessoaView = new PessoaView(pessoaViewModel);
var reservaView = new ReservaView(reservaViewModel, pessoaView, mediator);

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
  Console.WriteLine("1 - Configurar Capacidade Geral do Hotel");
  Console.WriteLine("2 - Criar Suíte");
  Console.WriteLine("3 - Exibir Capacidade Geral do Hotel");
  Console.WriteLine("4 - Listar Suítes");
  Console.WriteLine();
  Console.WriteLine("# Reservas");
  Console.WriteLine("5 - Criar Hospede");
  Console.WriteLine("6 - Criar Reserva");
  Console.WriteLine("7 - Liberar Reserva (finalizar)");
  Console.WriteLine("8 - Listar Hospedes");
  Console.WriteLine("9 - Listar Reservas");
  Console.WriteLine();
  Console.WriteLine("0 - Sair");
  Console.WriteLine();

  Console.Write("Escolha uma opção: ");
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
      configuracaoInicialView.ExibirQuantidadeVagas();
      break;
    case "4":
      suiteView.ListarSuites();
      break;
    case "5":
      pessoaView.CriarPessoa();
      break;
    case "6":
      reservaView.CriarReserva();
      break;
    case "7":
      reservaView.LiberarReserva();
      break;
    case "8":
      pessoaView.ListarPessoas();
      break;
    case "9":
      reservaView.ListarReservas();
      break;
    default:
      ColoredConsole.WriteLine("Opção Inválida", ConsoleColor.Red);
      break;
  }
}

ColoredConsole.WriteLine("Saindo...", ConsoleColor.Blue);
