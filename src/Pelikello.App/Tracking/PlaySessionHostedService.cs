namespace Pelikello.App.Tracking;

public class PlaySessionHostedService(
  IServiceProvider serviceProvider,
  PelikelloOptions options,
  ILogger<PlaySessionHostedService> logger) : BackgroundService
{
  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        using var scope = serviceProvider.CreateScope();
        var scanner = scope.ServiceProvider.GetRequiredService<IGameScanner>();
        var tracker = scope.ServiceProvider.GetRequiredService<IPlaySessionTracker>();
        var runningGames = await scanner.GetRunningGames();
        await tracker.UpdatePlaySessions(runningGames, stoppingToken);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Error updating play sessions in PlaySessionTracker");
      }

      await Task.Delay(TimeSpan.FromSeconds(options.TrackingPollInterval), stoppingToken);
    }
  }
}
