using System.Diagnostics;

namespace Pelikello.App.Tracking;

public class GameScanner(IGameRepository gameRepository) : IGameScanner
{
  public async Task<List<string>> GetRunningGames()
  {
    var processNames = Process.GetProcesses().Select(p => p.ProcessName).ToHashSet();
    var games = await gameRepository.GetGames();
    var runningGames = new List<string>();
    var matcher = new ProcessMatcher();

    foreach (var game in games)
    {
      foreach (var process in processNames)
      {
        if (matcher.IsMatch(game.Pattern, process))
        {
          runningGames.Add(game.Name);
          break;
        }
      }
    }

    return runningGames;
  }
}
