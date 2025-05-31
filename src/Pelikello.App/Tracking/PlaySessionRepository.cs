using Pelikello.App.Data;
using Microsoft.EntityFrameworkCore;

namespace Pelikello.App.Tracking;

public class PlaySessionRepository(IDbContextFactory<PelikelloDbContext> dbFactory) : IPlaySessionRepository
{
  public async Task AddPlaySession(PlaySession playSession)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    db.PlaySessions.Add(playSession);
    await db.SaveChangesAsync();
  }

  public async Task UpdatePlaySession(PlaySession playSession)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    db.PlaySessions.Update(playSession);
    await db.SaveChangesAsync();
  }

  public async Task DeletePlaySession(PlaySession playSession)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    db.PlaySessions.Remove(playSession);
    await db.SaveChangesAsync();
  }

  public async Task<PlaySession?> GetPlaySession(Guid id)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    return await db.PlaySessions.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
  }

  public async Task<List<PlaySession>> GetPlaySessions()
  {
    using var db = await dbFactory.CreateDbContextAsync();
    return await db.PlaySessions.AsNoTracking().ToListAsync();
  }

  public async Task<List<PlaySession>> GetActiveSessions(int timeoutMinutes)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    var threshold = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(timeoutMinutes));
    return await db.PlaySessions.Where(s => s.EndTime > threshold).AsNoTracking().ToListAsync();
  }

  public async Task<List<PlaySession>> GetHistory(TimeSpan duration)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    var threshold = DateTime.UtcNow.Subtract(duration);
    return await db.PlaySessions.Where(s => s.EndTime > threshold).AsNoTracking().ToListAsync();
  }
}
