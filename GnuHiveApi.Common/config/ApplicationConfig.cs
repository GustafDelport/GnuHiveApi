using GnuHiveApi.Common.Constants;
using Microsoft.Extensions.Configuration;

namespace GnuHiveApi.Common.config;

public class ApplicationConfig : IApplicationConfig
{
    private readonly IConfiguration _configuration;

    public ApplicationConfig(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    
    public string Environment => this._configuration["Environment"]!;
    public string AppName => this._configuration["AppName"]!;
    public string MainDbConnectionString => this._configuration.GetConnectionString(ConnectionStringLookup.MainConnectionString)!;
    public string SwaggerContextRoot => this._configuration["Swagger:ContextRoot"]!;
    public Microsoft.Extensions.Logging.LogLevel LogLevel => Enum.Parse<Microsoft.Extensions.Logging.LogLevel>(this._configuration["Logging:LogLevel:Default"]!);
    public ILoggingConfig Logging => new AppLoggingConfig(this._configuration);
}