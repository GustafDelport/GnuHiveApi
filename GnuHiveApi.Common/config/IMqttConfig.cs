namespace GnuHiveApi.Common.config;

public interface IMqttConfig
{
    string UserName { get; }
    string Password { get; }
    string Url { get; }
    string WebSocket { get; }
    int Port { get; }
}