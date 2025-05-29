namespace GnuHiveApi.Common.config;

public interface IHiveMqConfig
{
    string UserName { get; }
    string Password { get; }
    string Url { get; }
    string WebSocket { get; }
    int Port { get; }
}