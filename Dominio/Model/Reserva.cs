namespace SistemaHospedagem.Dominio.Model;


public class Reserva
{
  private const decimal DESCONTO_DEZ_DIAS = 0.10M;
  private const int ETAPA_SUITES = 0b100;
  private const int ETAPA_HOSPEDES = 0b010;
  private const int ETAPA_DIAS = 0b001;
  private const int ETAPAS_NECESSARIAS = ETAPA_SUITES | ETAPA_HOSPEDES | ETAPA_DIAS;

  private Suite? _Suite = null;
  private List<Pessoa> _Hospedes = new List<Pessoa>();
  private int _DiasReserva = 0;
  private decimal _Desconto = 0M;
  private int etapasConfiguradas = 0;

  public Suite? Suite
  {
    get => _Suite;

    set
    {
      if (value == null)
      {
        throw new ArgumentException("A Suite deve ser informada");
      }

      _Suite = value;
      etapasConfiguradas |= ETAPA_SUITES;
    }

  }

  public List<Pessoa> Hospedes { get => _Hospedes; }

  public int DiasReserva
  {
    get => _DiasReserva;

    set
    {
      if (value <= 0)
      {
        throw new ArgumentException("A quantidade de dias da reserva deve ser maior que zero");
      }

      if (value > 10)
      {
        _Desconto = DESCONTO_DEZ_DIAS;
      }

      _DiasReserva = value;
      etapasConfiguradas |= ETAPA_DIAS;
    }
  }

  public decimal Desconto { get => _Desconto; }

  public bool IsValida
  {
    get => etapasConfiguradas == ETAPAS_NECESSARIAS;
  }

  public bool IsCapacidadeAlcancada
  {
    get => Suite == null || Hospedes.Count >= Suite.Capacidade;
  }

  public Reserva()
  {
  }

  public void AdicionarHospede(Pessoa pessoa)
  {
    if (IsCapacidadeAlcancada)
    {
      throw new InvalidOperationException("Não é possível adicionar mais hospédes nesta suíte");
    }

    this._Hospedes.Add(pessoa);
    etapasConfiguradas |= ETAPA_HOSPEDES;
  }

  public void RemoverHospede(string nomeUsuario)
  {
    if (_Hospedes.RemoveAll(pessoa => pessoa.Usuario == nomeUsuario) <= 0)
    {
      throw new KeyNotFoundException("Nenhum hóspede com este nome de usuário está na reserva");
    }
  }
}
