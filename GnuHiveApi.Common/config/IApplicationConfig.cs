namespace GnuHiveApi.Common.config;

public interface IApplicationConfig
{
    string Environment { get; }
    string AppName { get; }
    string MainDbConnectionString { get; }
    string SwaggerContextRoot { get; }
    Microsoft.Extensions.Logging.LogLevel LogLevel { get; }
    ILoggingConfig Logging { get; }
    IMqttConfig MqttConfig { get; }
}