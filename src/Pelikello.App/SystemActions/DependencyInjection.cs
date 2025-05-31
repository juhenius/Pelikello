namespace Pelikello.App.SystemActions;

public static class DependencyInjection
{
  public static IServiceCollection AddPelikelloSystemActions(this IServiceCollection services)
  {
#if DEBUG
    services.AddSingleton<ISystemControl, LoggerSystemControl>();
#else
    if (OperatingSystem.IsWindows())
    {
      services.AddSingleton<ISystemControl, WindowsSystemControl>();
    }
    else if (OperatingSystem.IsLinux())
    {
      services.AddSingleton<ISystemControl, LinuxSystemControl>();
    }
    else if (OperatingSystem.IsMacOS())
    {
      services.AddSingleton<ISystemControl, MacSystemControl>();
    }
    else
    {
      throw new PlatformNotSupportedException("Unsupported OS.");
    }
#endif
    services.AddSingleton<ShutdownTrigger>();
    services.AddHostedService<ShutdownTriggerHostedService>();
    return services;
  }
}
