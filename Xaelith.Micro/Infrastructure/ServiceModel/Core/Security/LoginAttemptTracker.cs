namespace Xaelith.Micro.Infrastructure.ServiceModel.Core.Security;

using System.Collections.Concurrent;
using Xaelith.Micro.Infrastructure.DataModel.Core;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;

public class LoginAttemptTracker : ILoginAttemptTracker, IDisposable, IAsyncDisposable
{
    private readonly IConfigService _configService;
    private readonly Configuration _config;
    private Timer? _cleanupTimer;
    
    private readonly ConcurrentDictionary<string, UserLoginAttempt> _accountAttempts = new();
    private readonly ConcurrentDictionary<string, IpLoginAttempt> _ipAttempts = new();
    
    private int MaxUserFailedAttempts { get; set; } = 5;
    private int MaxIpFailedAttempts { get; set; } = 10;
    private int LockoutDurationMinutes { get; set; } = 15;
    private int RateLimitWindowMinutes { get; set; } = 5;
    private int ResetIntervalMinutes { get; set; } = 30;

    public LoginAttemptTracker(IConfigService configService)
    {
        _configService = configService;
        _config = configService.Root!;
        
        Initialize();
        
        configService.Modified += OnConfigurationModified;
    }

    public void RecordFailedAttempt(string username, string ipAddress)
    {
        var now = DateTime.UtcNow;

        _accountAttempts.AddOrUpdate(
            username,
            _ => new UserLoginAttempt
            {
                FailedAttempts = 1,
                FirstAttemptTime = now,
                LastAttemptTime = now,
                LockoutUntil = null
            },
            (_, existing) =>
            {
                existing.FailedAttempts++;
                existing.LastAttemptTime = now;

                if (existing.FailedAttempts >= MaxUserFailedAttempts)
                {
                    existing.LockoutUntil = now.AddMinutes(LockoutDurationMinutes);
                }

                return existing;
            }
        );

        _ipAttempts.AddOrUpdate(
            ipAddress,
            _ => new IpLoginAttempt
            {
                AttemptCount = 1,
                WindowStart = now
            },
            (_, existing) =>
            {
                if ((now - existing.WindowStart).TotalMinutes > RateLimitWindowMinutes)
                {
                    existing.AttemptCount = 1;
                    existing.WindowStart = now;
                }
                else
                {
                    existing.AttemptCount++;
                }

                return existing;
            }
        );
    }

    public void ClearFailedAttempts(string username)
    {
        _accountAttempts.TryRemove(username, out _);
    }

    public bool IsAccountLocked(string username)
    {
        if (!_accountAttempts.TryGetValue(username, out var record))
            return false;

        if (record.LockoutUntil.HasValue && record.LockoutUntil.Value > DateTime.UtcNow)
            return true;

        if (record.LockoutUntil.HasValue)
        {
            _accountAttempts.TryRemove(username, out _);
        }

        return false;
    }

    public bool IsIpRateLimited(string ipAddress)
    {
        if (!_ipAttempts.TryGetValue(ipAddress, out var record))
            return false;

        var now = DateTime.UtcNow;
        var windowElapsed = (now - record.WindowStart).TotalMinutes;

        if (windowElapsed > RateLimitWindowMinutes)
        {
            _ipAttempts.TryRemove(ipAddress, out _);
            return false;
        }

        return record.AttemptCount >= MaxIpFailedAttempts;
    }

    public int GetRemainingLockoutSeconds(string username)
    {
        if (!_accountAttempts.TryGetValue(username, out var record))
            return 0;

        if (!record.LockoutUntil.HasValue)
            return 0;

        var remaining = (record.LockoutUntil.Value - DateTime.UtcNow).TotalSeconds;
        return remaining > 0 ? (int)Math.Ceiling(remaining) : 0;
    }

    private void CleanupExpiredRecords(object? state)
    {
        var now = DateTime.UtcNow;

        foreach (var kvp in _accountAttempts)
        {
            if (kvp.Value.LockoutUntil.HasValue && kvp.Value.LockoutUntil.Value < now)
            {
                _accountAttempts.TryRemove(kvp.Key, out _);
            }
        }

        foreach (var kvp in _ipAttempts)
        {
            var windowElapsed = (now - kvp.Value.WindowStart).TotalMinutes;
            if (windowElapsed > RateLimitWindowMinutes)
            {
                _ipAttempts.TryRemove(kvp.Key, out _);
            }
        }
    }

    private void Initialize()
    {
        MaxUserFailedAttempts = _config.Security.UserMaxFailedLoginAttempts;
        MaxIpFailedAttempts = _config.Security.IpMaxFailedLoginAttempts;
        LockoutDurationMinutes = _config.Security.LockoutDurationMinutes;
        RateLimitWindowMinutes = _config.Security.RateLimitWindowMinutes;
        ResetIntervalMinutes = _config.Security.LockoutResetIntervalMinutes;

        _cleanupTimer?.Dispose();
        _cleanupTimer = new Timer(
            CleanupExpiredRecords,
            null,
            TimeSpan.Zero,
            TimeSpan.FromMinutes(ResetIntervalMinutes)
        );
    }

    private void OnConfigurationModified(object? sender, ConfigurationEventArgs e)
    {
        Initialize();
    }
    
    public void Dispose()
    {
        _cleanupTimer?.Dispose();
        _configService.Modified -= OnConfigurationModified;
    }

    public async ValueTask DisposeAsync()
    {
        if (_cleanupTimer != null) 
            await _cleanupTimer.DisposeAsync();
    }
}
