namespace Pelikello.App.Timing;

public static class DependencyInjection
{
  public static IServiceCollection AddPelikelloTiming(this IServiceCollection services)
  {
    return services
      .AddSingleton<GameTimer>();
  }
}
