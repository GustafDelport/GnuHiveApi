using System.Text.Json;
using ErrorOr;
using GnuHiveApi.Common.config;
using NLog;

namespace GnuHiveApi.Common.Logging;

public class NLogLogger : ILogLogger
{
    private readonly NLog.ILogger _logger;

    private static readonly List<LogLevel> LogLevels = new List<LogLevel>
    {
        LogLevel.Trace,
        LogLevel.Debug,
        LogLevel.Info,
        LogLevel.Warn,
        LogLevel.Error,
        LogLevel.Fatal,
        LogLevel.Off
    }.OrderBy(x => x.Ordinal).ToList();

    public NLogLogger(NLog.ILogger logger, IApplicationConfig applicationConfig)
    {
        this._logger = logger;

        var minLevel = LogLevels.FirstOrDefault(x => x.Ordinal == (int)applicationConfig.LogLevel);


        foreach (var rule in LogManager.Configuration.LoggingRules)
        {
            rule.SetLoggingLevels(minLevel, LogLevel.Fatal);
        }

        LogManager.ReconfigExistingLoggers();
    }

    public virtual void Debug(string message)
    {
        this.ConfigureContext();
        this._logger.Debug(message);
    }

    public virtual void Debug(string message, Exception exception)
    {
        this.ConfigureContext();
        this._logger.Debug(exception, message);
    }

    public virtual void DebugFormat(string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Debug(format, args);
    }

    public virtual void DebugFormat(Exception exception, string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Debug(exception, format, args);
    }

    public virtual void Error(string message)
    {
        this.ConfigureContext();
        this._logger.Error(message);
    }

    public void Error(Error error)
    {
        this.Error(JsonSerializer.Serialize(error));
    }

    public void Errors(IEnumerable<Error> errors)
    {
        this.Error(JsonSerializer.Serialize(errors));
    }

    public virtual void Error(string message, Exception exception)
    {
        this.ConfigureContext();
        this._logger.Error(exception, message);
    }

    public virtual void ErrorFormat(string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Error(format, args);
    }

    public virtual void ErrorFormat(Exception exception, string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Error(exception, format, args);
    }

    public virtual void Fatal(string message)
    {
        this.ConfigureContext();
        this._logger.Fatal(message);
    }

    public virtual void Fatal(string message, Exception exception)
    {
        this.ConfigureContext();
        this._logger.Fatal(exception, message);
    }

    public virtual void FatalFormat(string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Fatal(format, args);
    }

    public virtual void FatalFormat(Exception exception, string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Fatal(exception, format, args);
    }

    public virtual void Info(string message)
    {
        this.ConfigureContext();
        this._logger.Info(message);
    }

    public virtual void Info(string message, Exception exception)
    {
        this.ConfigureContext();
        this._logger.Info(exception, message);
    }

    public virtual void InfoFormat(string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Info(format, args);
    }

    public virtual void InfoFormat(Exception exception, string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Info(exception, format, args);
    }

    public virtual void Warn(string message)
    {
        this.ConfigureContext();
        this._logger.Warn(message);
    }

    public virtual void Warn(string message, Exception exception)
    {
        this.ConfigureContext();
        this._logger.Warn(exception, message);
    }

    public virtual void WarnFormat(string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Warn(format, args);
    }

    public virtual void WarnFormat(Exception exception, string format, params object[] args)
    {
        this.ConfigureContext();
        this._logger.Warn(exception, format, args);
    }

    public virtual void Progress(float progress)
    {
        this.ConfigureContext();
    }

    public virtual void Progress(float progress, string processName)
    {
        this.ConfigureContext();
    }

    private void ConfigureContext()
    {
        //MappedDiagnosticsContext.Set("user_identity", this.userContextProvider.GetUser().UserName);
    }
}