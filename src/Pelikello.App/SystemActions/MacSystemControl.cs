using System.Diagnostics;

namespace Pelikello.App.SystemActions;

public class MacSystemControl : ISystemControl
{
  public void Shutdown()
  {
    Process.Start("shutdown", "-h now");
  }
}
