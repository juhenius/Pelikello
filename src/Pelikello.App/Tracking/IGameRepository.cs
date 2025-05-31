namespace Pelikello.App.Tracking;

public interface IGameRepository
{
  Task AddGame(Game game);
  Task UpdateGame(Game game);
  Task DeleteGame(Game game);
  Task<Game?> GetGame(Guid id);
  Task<List<Game>> GetGames();
}
