namespace SistemaHospedagem.Dominio.Model;

public class Suite
{
  public const int ETAPA_CAPACIDADE = 0b0001;
  public const int ETAPA_NOME = 0b0010;
  public const int ETAPA_PRECO_DIARIA = 0b0100;
  public const int ETAPA_NUMERO = 0b1000;
  public const int ETAPAS_NECESSARIAS = ETAPA_CAPACIDADE | ETAPA_NOME | ETAPA_PRECO_DIARIA | ETAPA_NUMERO;

  private int etapas = 0;

  private int _Capacidade = 0;
  private string _Nome = "";
  private decimal _PrecoDiaria = 0;
  private int _Numero = 0;

  public bool IsValida
  {
    get => etapas == ETAPAS_NECESSARIAS;
  }

  public int EtapasConfiguradas { get => etapas; }

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
      etapas |= ETAPA_CAPACIDADE;
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
      etapas |= ETAPA_NOME;
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
      etapas |= ETAPA_PRECO_DIARIA;
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
      etapas |= ETAPA_NUMERO;
    }
  }

  public override string ToString()
  {
    return $"Número: {this._Numero}, Nome: {this._Nome}, Capacidade: {this._Capacidade}, Preço/Diária: {this._PrecoDiaria}";
  }
}
