namespace Pelikello.App;

public class PelikelloOptions
{
  public string Db { get; set; } = "Data Source=pelikello.db";
  public string Passcode { get; set; } = "demo1234";
  public int TrackingPollInterval { get; set; } = 10;
}
