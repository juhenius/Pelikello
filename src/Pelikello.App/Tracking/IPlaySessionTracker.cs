namespace Pelikello.App.Tracking;

public interface IPlaySessionTracker
{
  Task<List<PlaySession>> GetActiveSessions();
  Task<List<PlaySession>> GetHistory(TimeSpan duration);
  Task UpdatePlaySessions(List<string> runningGames, CancellationToken stoppingToken);
}
