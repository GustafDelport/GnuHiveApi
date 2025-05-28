using NLog;

namespace GnuHiveApi.Common.Logging;

public class NLogLogLevelMapper
{
    public LogLevel MapFromDotNet(Microsoft.Extensions.Logging.LogLevel logLevel)
    {
        switch (logLevel)
        {
            case Microsoft.Extensions.Logging.LogLevel.None:
                return LogLevel.Off;
            case Microsoft.Extensions.Logging.LogLevel.Trace:
                return LogLevel.Trace;
            case Microsoft.Extensions.Logging.LogLevel.Debug:
                return LogLevel.Debug;
            case Microsoft.Extensions.Logging.LogLevel.Information:
                return LogLevel.Info;
            case Microsoft.Extensions.Logging.LogLevel.Warning:
                return LogLevel.Warn;
            case Microsoft.Extensions.Logging.LogLevel.Error:
                return LogLevel.Error;
            case Microsoft.Extensions.Logging.LogLevel.Critical:
                return LogLevel.Fatal;
            default:
                return LogLevel.Warn;
        }
    }
}