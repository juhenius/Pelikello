namespace Pelikello.App.Tracking;

public interface IGameScanner
{
  Task<List<string>> GetRunningGames();
}
