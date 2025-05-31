using System.Diagnostics;

namespace Pelikello.App.SystemActions;

public class LinuxSystemControl : ISystemControl
{
  public void Shutdown()
  {
    Process.Start("shutdown", "-h now");
  }
}
