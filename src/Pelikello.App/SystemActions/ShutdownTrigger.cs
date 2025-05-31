using Pelikello.App.Timing;

namespace Pelikello.App.SystemActions;

public class ShutdownTrigger : IDisposable
{
  private readonly GameTimer _gameTimer;
  private readonly ISystemControl _machineCtrl;
  private readonly ILogger<ShutdownTrigger> _logger;

  public ShutdownTrigger(GameTimer gameTimer, ISystemControl machineCtrl, ILogger<ShutdownTrigger> logger)
  {
    _machineCtrl = machineCtrl;
    _logger = logger;
    _gameTimer = gameTimer;
    _gameTimer.TimeExpired += OnTimeExpired;
  }

  private void OnTimeExpired()
  {
    try
    {
      _logger.LogWarning("Game time expired. Initiating system shutdown...");
      _machineCtrl.Shutdown();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to execute shutdown.");
    }
  }

  public void Dispose()
  {
    _gameTimer.TimeExpired -= OnTimeExpired;
  }
}
