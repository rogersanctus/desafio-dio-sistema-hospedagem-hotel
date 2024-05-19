using System.Collections.ObjectModel;
using SistemaHospedagem.Dominio.Model;
using SistemaHospedagem.Lib.MVVM.ViewModel;

namespace SistemaHospedagem.Dominio.ViewModel;

public class PessoaViewModel : ViewModelBase
{
  private GerentePessoas gerentePessoas;

  public PessoaViewModel(GerentePessoas gerentePessoas)
  {
    this.gerentePessoas = gerentePessoas;
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
      this.NotificarViews("AdicionarPessoa:Sucesso", pessoa.ToString());
    }
    catch (InvalidOperationException ex)
    {
      this.NotificarViews("AdicionarPessoa:Erro", ex.Message);
    }
  }

  public ReadOnlyCollection<Pessoa> ListaPessoas()
  {
    return this.gerentePessoas.Pessoas;
  }
}
