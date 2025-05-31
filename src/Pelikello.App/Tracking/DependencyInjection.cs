namespace Pelikello.App.Tracking;

public static class DependencyInjection
{
  public static IServiceCollection AddPelikelloTracking(this IServiceCollection services)
  {
    return services
      .AddSingleton<TrackingEventsInterceptor>()
      .AddScoped<IGameRepository, GameRepository>()
      .AddScoped<IPlaySessionRepository, PlaySessionRepository>()
      .AddScoped<IGameScanner, GameScanner>()
      .AddScoped<IPlaySessionTracker, PlaySessionTracker>()
      .AddSingleton<TrackingEvents>()
      .AddHostedService<PlaySessionHostedService>();
  }
}
