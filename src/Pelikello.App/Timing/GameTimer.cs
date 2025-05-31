namespace Pelikello.App.Timing;

public class GameTimer : IDisposable
{
  private TimeSpan _timeLeft = TimeSpan.FromMinutes(60);
  private bool _isRunning = false;
  private readonly Timer? _timer;
  public event Action? RunningChanged;
  public event Action? TimeChanged;
  public event Action? TimeExpired;
  private DateTimeOffset _lastUpdate = DateTimeOffset.UtcNow;
  private readonly ILogger<GameTimer> _logger;

  public TimeSpan TimeLeft => _timeLeft;
  public bool Running => Active && _isRunning;
  public bool Active => _timeLeft.TotalSeconds > 0;

  public GameTimer(ILogger<GameTimer> logger)
  {
    _logger = logger;
    _timer = new Timer(_ =>
    {
      if (Running)
      {
        var now = DateTimeOffset.UtcNow;
        var elapsed = now - _lastUpdate;
        _lastUpdate = now;

        AdjustRemainingTime(-elapsed);
      }
      else
      {
        _lastUpdate = DateTimeOffset.UtcNow;
      }
    }, null, 1000, 1000);
  }

  public void AddTime(TimeSpan delta)
  {
    _logger.LogInformation("Adding time: {Delta}", delta);
    AdjustRemainingTime(delta);
  }

  public void SubtractTime(TimeSpan delta)
  {
    _logger.LogInformation("Subtracting time: {Delta}", delta);
    AdjustRemainingTime(-delta);
  }

  private void AdjustRemainingTime(TimeSpan delta)
  {
    var expires = _timeLeft > TimeSpan.Zero && _timeLeft + delta < TimeSpan.Zero;
    _timeLeft = expires ? TimeSpan.Zero : _timeLeft + delta;
    TimeChanged?.Invoke();

    if (expires)
    {
      _logger.LogInformation("Time expired");
      Pause();
      TimeExpired?.Invoke();
    }
  }

  public void TogglePause()
  {
    _logger.LogInformation("Toggling pause: {IsRunning}", _isRunning);
    if (_isRunning)
    {
      Pause();
    }
    else
    {
      Run();
    }
  }

  public void Pause()
  {
    if (_isRunning)
    {
      _logger.LogInformation("Pausing");
      _isRunning = false;
      RunningChanged?.Invoke();
    }
  }

  public void Run()
  {
    if (!_isRunning)
    {
      _logger.LogInformation("Running");
      _isRunning = true;
      RunningChanged?.Invoke();
    }
  }

  public void Dispose()
  {
    _timer?.Dispose();
    GC.SuppressFinalize(this);
  }
}
