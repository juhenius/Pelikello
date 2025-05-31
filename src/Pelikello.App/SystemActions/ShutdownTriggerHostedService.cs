namespace Pelikello.App.SystemActions;

public class ShutdownTriggerHostedService(
    IServiceProvider serviceProvider
) : BackgroundService
{
  private IServiceScope? _scope;
  private ShutdownTrigger? _trigger;

  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _scope = serviceProvider.CreateScope();
    _trigger = _scope.ServiceProvider.GetRequiredService<ShutdownTrigger>();
    return Task.CompletedTask;
  }

  public override void Dispose()
  {
    _trigger?.Dispose();
    _scope?.Dispose();
    base.Dispose();
  }
}
