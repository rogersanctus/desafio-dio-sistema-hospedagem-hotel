using System.Collections.ObjectModel;

namespace SistemaHospedagem.Dominio.Model;

public class GerentePessoas
{
  private List<Pessoa> _Pessoas = new List<Pessoa>();

  public ReadOnlyCollection<Pessoa> Pessoas { get => _Pessoas.AsReadOnly(); }

  public GerentePessoas()
  {
  }

  public void AdicionarPessoa(Pessoa pessoa)
  {
    var existente = _Pessoas.Find(pessoaBusca => pessoaBusca.Usuario == pessoa.Usuario);

    if (existente != null)
    {
      throw new InvalidOperationException("Nome de Usuário já está sendo usado");
    }

    this._Pessoas.Add(pessoa);
  }

  public Pessoa? ObterPessoaPorUsuario(string nomeUsuario)
  {
    return this._Pessoas.FirstOrDefault(pessoa => pessoa.Usuario == nomeUsuario);
  }

}

