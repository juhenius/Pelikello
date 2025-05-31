using System.Diagnostics;

namespace Pelikello.App.SystemActions;

public class WindowsSystemControl : ISystemControl
{
  public void Shutdown()
  {
    Process.Start(new ProcessStartInfo("shutdown", "/s /t 0")
    {
      CreateNoWindow = true,
      UseShellExecute = false
    });
  }
}
