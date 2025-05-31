namespace Pelikello.App.Tracking;

public interface ITrackingEvents
{
  public event Action? GamesUpdated;
  public event Action? PlaySessionsUpdated;
}
