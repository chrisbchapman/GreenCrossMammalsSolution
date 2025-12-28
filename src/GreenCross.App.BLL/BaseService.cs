using Microsoft.Extensions.Logging;

namespace GreenCross.App.BLL;

public abstract partial class BaseService<T> where T : class
{
    protected readonly ILogger<T> Logger;

    protected BaseService(ILogger<T> logger)
    {
        Logger = logger;
    }

    // Use IsEnabled to avoid unnecessary allocations
    protected void LogError(Exception ex, string message, params object[] args)
    {
        if (Logger.IsEnabled(LogLevel.Error))
        {
            Logger.LogError(ex, message, args);
        }
    }

    protected void LogInformation(string message, params object[] args)
    {
        if (Logger.IsEnabled(LogLevel.Information))
        {
            Logger.LogInformation(message, args);
        }
    }

    protected void LogWarning(string message, params object[] args)
    {
        if (Logger.IsEnabled(LogLevel.Warning))
        {
            Logger.LogWarning(message, args);
        }
    }

    protected void LogDebug(string message, params object[] args)
    {
        if (Logger.IsEnabled(LogLevel.Debug))
        {
            Logger.LogDebug(message, args);
        }
    }
}
