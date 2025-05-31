namespace Pelikello.App.Tracking;

public class Game
{
  public Guid Id { get; set; } = Guid.CreateVersion7();
  public string Name { get; set; } = string.Empty;
  public string Pattern { get; set; } = string.Empty;
}
