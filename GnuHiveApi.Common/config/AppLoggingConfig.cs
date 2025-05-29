using Microsoft.Extensions.Configuration;

namespace GnuHiveApi.Common.config;

public class AppLoggingConfig : ILoggingConfig
{
    private readonly IConfiguration _configuration;

    public int BufferSize => this._configuration.GetValue<int>("AppLogging:BufferSize");
    public int FlushTimeoutInSeconds => this._configuration.GetValue<int>("AppLogging:FlushTimeoutInSeconds");
    public bool EnableLogBuffering => this._configuration.GetValue<bool>("AppLogging:EnableLogBuffering");

    public AppLoggingConfig(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
}