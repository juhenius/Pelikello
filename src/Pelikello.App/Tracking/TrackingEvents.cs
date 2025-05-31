namespace Pelikello.App.Tracking;

public class TrackingEvents : ITrackingEvents
{
  public event Action? GamesUpdated;
  public event Action? PlaySessionsUpdated;

  public void EmitGamesUpdated()
  {
    GamesUpdated?.Invoke();
  }

  public void EmitPlaySessionsUpdated()
  {
    PlaySessionsUpdated?.Invoke();
  }
}
