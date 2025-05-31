namespace Pelikello.App.Tracking;

public interface IPlaySessionRepository
{
  Task AddPlaySession(PlaySession playSession);
  Task UpdatePlaySession(PlaySession playSession);
  Task DeletePlaySession(PlaySession playSession);
  Task<PlaySession?> GetPlaySession(Guid id);
  Task<List<PlaySession>> GetPlaySessions();
  Task<List<PlaySession>> GetActiveSessions(int timeoutMinutes);
  Task<List<PlaySession>> GetHistory(TimeSpan duration);
}
