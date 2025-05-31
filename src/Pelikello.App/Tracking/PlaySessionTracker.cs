using Microsoft.EntityFrameworkCore;
using Pelikello.App.Data;
using Pelikello.App.Timing;

namespace Pelikello.App.Tracking;

public class PlaySessionTracker(
  IPlaySessionRepository playSessionRepository,
  GameTimer gameTimer,
  TrackingEvents trackingEvents,
  ILogger<PlaySessionTracker> logger) : IPlaySessionTracker
{
  const int SESSION_TIMEOUT_MINUTES = 1;

  public async Task UpdatePlaySessions(List<string> runningGames, CancellationToken stoppingToken)
  {
    var now = DateTime.UtcNow;
    var activeSessions = await GetActiveSessions();

    foreach (var game in runningGames)
    {
      var session = activeSessions
        .Where(s => s.GameName == game && s.TimerActive == gameTimer.Active)
        .FirstOrDefault();

      if (session != null)
      {
        session.EndTime = now;
        await playSessionRepository.UpdatePlaySession(session);
      }
      else
      {
        await playSessionRepository.AddPlaySession(new PlaySession
        {
          GameName = game,
          StartTime = now,
          EndTime = now,
          TimerActive = gameTimer.Active
        });

        logger.LogInformation("Started session for {game} at {now}", game, now);
      }
    }

    trackingEvents.EmitPlaySessionsUpdated();
  }

  public async Task<List<PlaySession>> GetActiveSessions()
  {
    return await playSessionRepository.GetActiveSessions(SESSION_TIMEOUT_MINUTES);
  }

  public async Task<List<PlaySession>> GetHistory(TimeSpan duration)
  {
    return await playSessionRepository.GetHistory(duration);
  }
}
