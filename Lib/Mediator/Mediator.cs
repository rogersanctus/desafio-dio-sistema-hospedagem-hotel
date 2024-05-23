namespace SistemaHospedagem.Lib.Mediator;

public interface IObserver
{
  void OnNotifyObserver(string @event, string? argument = null);
}

public class Mediator
{
  public List<IObserver> observers = new List<IObserver>();

  public void Notify(string @event, string? argument = null)
  {
    foreach (var observer in observers)
    {
      observer.OnNotifyObserver(@event, argument);
    }
  }

  public void AddObserver(IObserver observer)
  {
    observers.Add(observer);
  }
}
