namespace SistemaHospedagem.Dominio.Model;

public class Suite
{
  private int _Capacidade = 0;
  private string _Nome = "";
  private decimal _PrecoDiaria = 0;
  private int _Numero = 0;

  public int Capacidade
  {
    get => this._Capacidade;

    set
    {
      if (value <= 0)
      {
        throw new ArgumentException("A Capacidade deve ser ao menos de uma pessoa");
      }

      this._Capacidade = value;
    }
  }

  public string Nome
  {
    get => this._Nome;

    set
    {
      if (string.IsNullOrEmpty(value))
      {
        throw new ArgumentException("O Nome deve ser informado");
      }

      this._Nome = value.ToUpper();
    }
  }

  public decimal PrecoDiaria
  {
    get => this._PrecoDiaria;

    set
    {
      if (value < 0)
      {
        throw new ArgumentException("O Preço deve ser maior ou igual a zero");
      }

      this._PrecoDiaria = value;
    }
  }

  public int Numero
  {
    get => _Numero;
    set
    {
      if (value <= 0)
      {
        throw new ArgumentException("O Número da suite deve ser maior que zero");
      }

      this._Numero = value;
    }
  }

  public override string ToString()
  {
    return $"Número: {this._Numero}, Nome: {this._Nome}, Capacidade: {this._Capacidade}, Preço/Diária: {this._PrecoDiaria}";
  }
}
