using EventEase.Models;

namespace EventEase.Services
{
    public interface IUserSessionService
    {
        UserSession CurrentSession { get; }
        event Action? OnSessionChanged;
        Task StartSessionAsync(string userId);
        Task EndSessionAsync();
        Task UpdateLastActivityAsync();
        Task<bool> IsSessionActiveAsync();
        Task<TimeSpan> GetSessionDurationAsync();
    }

    public class UserSession
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string, object> SessionData { get; set; } = new();

        public TimeSpan Duration => DateTime.Now - StartTime;
        public TimeSpan TimeSinceLastActivity => DateTime.Now - LastActivity;
    }

    public class UserSessionService : IUserSessionService
    {
        private UserSession _currentSession = new();
        private Timer? _sessionTimer;
        private readonly TimeSpan _sessionTimeout = TimeSpan.FromMinutes(30);

        public UserSession CurrentSession => _currentSession;

        public event Action? OnSessionChanged;

        public async Task StartSessionAsync(string userId)
        {
            await Task.Run(() =>
            {
                _currentSession = new UserSession
                {
                    UserId = userId,
                    StartTime = DateTime.Now,
                    LastActivity = DateTime.Now,
                    IsActive = true
                };

                // 启动会话超时计时器
                _sessionTimer?.Dispose();
                _sessionTimer = new Timer(CheckSessionTimeout, null, _sessionTimeout, _sessionTimeout);

                OnSessionChanged?.Invoke();
            });
        }

        public async Task EndSessionAsync()
        {
            await Task.Run(() =>
            {
                _currentSession.IsActive = false;
                _sessionTimer?.Dispose();
                _sessionTimer = null;

                OnSessionChanged?.Invoke();
            });
        }

        public async Task UpdateLastActivityAsync()
        {
            await Task.Run(() =>
            {
                if (_currentSession.IsActive)
                {
                    _currentSession.LastActivity = DateTime.Now;
                    OnSessionChanged?.Invoke();
                }
            });
        }

        public async Task<bool> IsSessionActiveAsync()
        {
            await Task.Delay(1);
            return _currentSession.IsActive &&
                   _currentSession.TimeSinceLastActivity < _sessionTimeout;
        }

        public async Task<TimeSpan> GetSessionDurationAsync()
        {
            await Task.Delay(1);
            return _currentSession.Duration;
        }

        private void CheckSessionTimeout(object? state)
        {
            if (_currentSession.IsActive &&
                _currentSession.TimeSinceLastActivity >= _sessionTimeout)
            {
                _ = EndSessionAsync();
            }
        }

        public void Dispose()
        {
            _sessionTimer?.Dispose();
        }
    }
}
