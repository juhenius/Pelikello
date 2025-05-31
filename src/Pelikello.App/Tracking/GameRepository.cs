using Pelikello.App.Data;
using Microsoft.EntityFrameworkCore;

namespace Pelikello.App.Tracking;

public class GameRepository(IDbContextFactory<PelikelloDbContext> dbFactory) : IGameRepository
{
  public async Task AddGame(Game game)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    db.Games.Add(game);
    await db.SaveChangesAsync();
  }

  public async Task UpdateGame(Game game)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    db.Games.Update(game);
    await db.SaveChangesAsync();
  }

  public async Task DeleteGame(Game game)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    db.Games.Remove(game);
    await db.SaveChangesAsync();
  }

  public async Task<Game?> GetGame(Guid id)
  {
    using var db = await dbFactory.CreateDbContextAsync();
    return await db.Games.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
  }

  public async Task<List<Game>> GetGames()
  {
    using var db = await dbFactory.CreateDbContextAsync();
    return await db.Games.AsNoTracking().ToListAsync();
  }
}
