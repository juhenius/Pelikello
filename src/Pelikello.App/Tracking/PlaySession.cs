namespace Pelikello.App.Tracking;

public class PlaySession
{
  public Guid Id { get; set; } = Guid.CreateVersion7();
  public string GameName { get; set; } = string.Empty;
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
  public bool TimerActive { get; set; }

  public TimeSpan Duration => EndTime - StartTime;
}
