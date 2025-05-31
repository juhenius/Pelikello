using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Pelikello.App.Data;

namespace Pelikello.App.Tracking;

public class TrackingEventsInterceptor(TrackingEvents trackingEvents) : SaveChangesInterceptor
{
  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
  {
    ApplyTrackingEvents(eventData.Context);
    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
      DbContextEventData eventData,
      InterceptionResult<int> result,
      CancellationToken cancellationToken = default)
  {
    ApplyTrackingEvents(eventData.Context);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
  {
    EmitTrackingEvents(eventData.Context);
    return base.SavedChanges(eventData, result);
  }

  public override async ValueTask<int> SavedChangesAsync(
      SaveChangesCompletedEventData eventData,
    int result,
    CancellationToken cancellationToken = default)
  {
    EmitTrackingEvents(eventData.Context);
    return await base.SavedChangesAsync(eventData, result, cancellationToken);
  }

  private static void ApplyTrackingEvents(DbContext? context)
  {
    if (context is not PelikelloDbContext pelikelloDbContext)
    {
      return;
    }

    pelikelloDbContext.TrackingFlags.GamesChanged = ShouldEmitTrackingEvents<Game>(context);
    pelikelloDbContext.TrackingFlags.SessionsChanged = ShouldEmitTrackingEvents<PlaySession>(context);
  }

  private void EmitTrackingEvents(DbContext? context)
  {
    if (context is not PelikelloDbContext pelikelloDbContext)
    {
      return;
    }

    if (pelikelloDbContext.TrackingFlags.GamesChanged)
    {
      trackingEvents.EmitGamesUpdated();
    }

    if (pelikelloDbContext.TrackingFlags.SessionsChanged)
    {
      trackingEvents.EmitPlaySessionsUpdated();
    }
  }

  private static bool ShouldEmitTrackingEvents<T>(DbContext context) where T : class
  {
    return context.ChangeTracker.Entries<T>()
      .Any(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);
  }
}
