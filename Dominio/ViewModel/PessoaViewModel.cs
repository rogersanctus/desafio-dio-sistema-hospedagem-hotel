using System.Collections.ObjectModel;
using System.Text.Json;
using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Lib.Mediator;
using SistemaHospedagem.Lib.MVVM.ViewModel;

namespace SistemaHospedagem.Dominio.ViewModel;

public class PessoaViewModel : ViewModelBase
{
  private GerentePessoas gerentePessoas;
  private Mediator mediator;

  public PessoaViewModel(GerentePessoas gerentePessoas, Mediator mediator)
  {
    this.gerentePessoas = gerentePessoas;
    this.mediator = mediator;
  }

  public void AtribuirNome(Pessoa pessoa, string nome)
  {
    try
    {
      pessoa.Nome = nome;
      this.NotificarViews("AtribuirNome:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarViews("AtribuirNome:Erro", ex.Message);
    }
  }

  public void AtribuirSobrenome(Pessoa pessoa, string sobrenome)
  {
    try
    {
      pessoa.Sobrenome = sobrenome;
      this.NotificarViews("AtribuirSobrenome:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarViews("AtribuirSobrenome:Erro", ex.Message);
    }
  }

  public void AtribuirUsuario(Pessoa pessoa, string usuario)
  {
    try
    {
      pessoa.Usuario = usuario;
      this.NotificarViews("AtribuirUsuario:Sucesso");
    }
    catch (ArgumentException ex)
    {
      this.NotificarViews("AtribuirUsuario:Erro", ex.Message);
    }
  }

  public void AdicionarPessoa(Pessoa pessoa)
  {
    try
    {
      this.gerentePessoas.AdicionarPessoa(pessoa);
      var pessoaJson = JsonSerializer.Serialize(pessoa);
      this.NotificarViews("AdicionarPessoa:Sucesso", pessoaJson);
      this.mediator.Notify("AdicionarPessoa:Sucesso", pessoaJson);
    }
    catch (InvalidOperationException ex)
    {
      this.NotificarViews("AdicionarPessoa:Erro", ex.Message);
      this.mediator.Notify("AdicionarPessoa:Erro", ex.Message);
    }
  }

  public ReadOnlyCollection<Pessoa> ListaPessoas()
  {
    return this.gerentePessoas.Pessoas;
  }
}
