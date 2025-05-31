namespace Pelikello.App.SystemActions;

public class LoggerSystemControl(ILogger<LoggerSystemControl> logger) : ISystemControl
{
  public void Shutdown()
  {
    logger.LogInformation("Shutdown");
  }
}
