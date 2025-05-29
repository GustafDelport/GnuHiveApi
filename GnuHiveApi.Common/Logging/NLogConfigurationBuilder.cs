using GnuHiveApi.Common.config;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace GnuHiveApi.Common.Logging;

public class NLogConfigurationBuilder
{
    private readonly string _appName;
    private readonly LoggingConfiguration _loggingConfiguration;
    private readonly IApplicationConfig _appConfig;
    private readonly NLogLogLevelMapper nLogLogLevelMapper;

    public NLogConfigurationBuilder(
        string appName,
        IApplicationConfig appConfig,
        NLogLogLevelMapper nLogLogLevelMapper)
    {
        this._appName = appName;
        this._appConfig = appConfig;
        this.nLogLogLevelMapper = nLogLogLevelMapper;
        this._loggingConfiguration = new LoggingConfiguration();
    }

    public NLogConfigurationBuilder WithDbLogging(string connectionString, string activityFormat = "aspnet-TraceIdentifier:ignoreActivityId=true")
    {
        var dbTarget = new DatabaseTarget
        {
            ConnectionString = connectionString,
            CommandText =
                @"INSERT INTO log.Log
                    (
                        Application,
                        Logged,
                        Level,
                        Message,
                        MachineName,
                        UserName,
                        CallSite,
                        Thread,
                        Exception,
                        Stacktrace,
                        ActivityId,
                        ExceptionData
                    ) 
                    VALUES 
                    (
                        @application,
                        @logged,
                        @level,
                        @message,
                        @machinename,
                        @user_name,
                        @call_site,
                        @threadid,
                        @log_exception,
                        @stacktrace,
                        @activityid,
                        @exceptiondata
                    );"
        };

        dbTarget.Parameters.Add(new DatabaseParameterInfo("@application",
            new SimpleLayout(this._appName)));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@activityid", new SimpleLayout("${" + activityFormat + "}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@logged", new SimpleLayout("${date}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@thread", new SimpleLayout("${threadid}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@level", new SimpleLayout("${level}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@message", new SimpleLayout("${message}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@machinename", new SimpleLayout("${machinename}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@user_name",
            new SimpleLayout("${mdc:item=user_identity}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@call_site",
            new SimpleLayout("${callsite:filename=true}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@threadid", new SimpleLayout("${threadid}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@log_exception",
            new SimpleLayout("${exception:format=tostring}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@exceptiondata", new SimpleLayout("${exception:Data}")));
        dbTarget.Parameters.Add(new DatabaseParameterInfo("@stacktrace",
            new SimpleLayout("${exception:stacktrace}")));
        dbTarget.Name = "database";

        var logLevelFromConfig = this.nLogLogLevelMapper.MapFromDotNet(this._appConfig.LogLevel);

        if (this._appConfig.Logging.EnableLogBuffering)
        {
            var dbBufferWrapper = new BufferingTargetWrapper(dbTarget, this._appConfig.Logging.BufferSize, (int)TimeSpan.FromSeconds(this._appConfig.Logging.FlushTimeoutInSeconds).TotalMilliseconds);
            dbBufferWrapper.OverflowAction = BufferingTargetWrapperOverflowAction.Flush;
            this._loggingConfiguration.AddTarget("BufferedDatabase", dbBufferWrapper);
            this._loggingConfiguration.LoggingRules.Add(new LoggingRule("*", logLevelFromConfig, dbBufferWrapper));
        }
        else
        {
            this._loggingConfiguration.AddTarget("BufferedDatabase", dbTarget);
            this._loggingConfiguration.LoggingRules.Add(new LoggingRule("*", logLevelFromConfig, dbTarget));
        }

        return this;
    }

    public NLogConfigurationBuilder WithConsoleLogging()
    {
        var consoleTarget = new ColoredConsoleTarget
        {
            Layout = "${date:format=HH\\:mm\\:ss} ${logger} ${message}"
        };

        var logLevelFromConfig = this.nLogLogLevelMapper.MapFromDotNet(this._appConfig.LogLevel);
        this._loggingConfiguration.AddTarget("consoleTarget", consoleTarget);
        this._loggingConfiguration.LoggingRules.Add(new LoggingRule("*", logLevelFromConfig, consoleTarget));

        LogManager.Configuration = this._loggingConfiguration;

        return this;
    }

    public NLogConfigurationBuilder WithFileLogging()
    {
        var fileTarget = new FileTarget { ArchiveEvery = FileArchivePeriod.Day };
        fileTarget.Name = "file";
        fileTarget.FileName = @"${basedir}/logs/${level}.${shortdate}.log.txt";
        fileTarget.Layout =
            "${longdate}|${level:uppercase=true}|${logger}|${message}" +
            "${onexception:inner=\n" +
            "${exception:format=Type,Message,StackTrace}" +
            "}";
        this._loggingConfiguration.AddTarget("fileTarget", fileTarget);
        this._loggingConfiguration.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));

        LogManager.Configuration = this._loggingConfiguration;

        return this;
    }

    public LoggingConfiguration Build()
    {
        return this._loggingConfiguration;
    }
}