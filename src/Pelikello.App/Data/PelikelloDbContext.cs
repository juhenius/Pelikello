using Microsoft.EntityFrameworkCore;
using Pelikello.App.Tracking;

namespace Pelikello.App.Data;

public class PelikelloDbContext(DbContextOptions<PelikelloDbContext> options) : DbContext(options)
{
  public TrackingFlags TrackingFlags { get; set; } = new();
  public DbSet<PlaySession> PlaySessions { get; set; } = null!;
  public DbSet<Game> Games { get; set; } = null!;
}

public class TrackingFlags
{
  public bool GamesChanged { get; set; } = false;
  public bool SessionsChanged { get; set; } = false;
}
