namespace Pelikello.App.Tracking;

public class ProcessMatcher
{
  public bool IsMatch(string pattern, string processName)
  {
    if (string.IsNullOrWhiteSpace(pattern))
    {
      return false;
    }

    return processName.Contains(pattern, StringComparison.InvariantCultureIgnoreCase);
  }
}
