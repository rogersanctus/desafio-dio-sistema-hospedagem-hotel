namespace SistemaHospedagem.Dominio.Model;

public class Pessoa
{
  private string _Nome = "";
  private string _Sobrenome = "";
  private string _Usuario = "";

  public string Nome
  {
    get => this._Nome;

    set
    {
      if (string.IsNullOrEmpty(value))
      {
        throw new ArgumentException("O Primeiro Nome deve ser informado");
      }

      this._Nome = value.ToUpper();
    }
  }

  public string Sobrenome
  {
    get => this._Sobrenome;

    set
    {
      if (string.IsNullOrEmpty(value))
      {
        throw new ArgumentException("O Sobrenome deve ser informado");
      }

      this._Sobrenome = value.ToUpper();
    }
  }

  public string NomeCompleto
  {
    get => $"{this._Nome} {this._Sobrenome}";
  }

  public string Usuario
  {
    get => this._Usuario;

    set
    {
      if (string.IsNullOrEmpty(value))
      {
        throw new ArgumentException("O Nome de Usuario deve ser informado");
      }

      if (value.Length < 3)
      {
        throw new ArgumentException("O Nome de Usuario deve ter pelo menos 3 caracteres");
      }

      this._Usuario = value;
    }
  }

  public Pessoa() { }


  public Pessoa(string nome, string sobrenome, string usuario)
  {
    this.Nome = nome;
    this.Sobrenome = sobrenome;
    this.Usuario = usuario;
  }

  public override string ToString()
  {
    return $"Usuário: {this._Usuario} - Nome completo: {this.NomeCompleto}";
  }
}
