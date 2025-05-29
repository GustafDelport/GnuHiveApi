namespace GnuHiveApi.Common.config;

public interface ILoggingConfig
{
    int BufferSize { get; }
    int FlushTimeoutInSeconds { get; }
    bool EnableLogBuffering { get; }
}