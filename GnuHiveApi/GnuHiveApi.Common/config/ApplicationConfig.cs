using Microsoft.Extensions.Configuration;

namespace GnuHiveApi.Common.config;

public class ApplicationConfig : IApplicationConfig
{
    private readonly IConfiguration _configuration;

    public ApplicationConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Environment => this._configuration["Environment"]!;
    
    public string SwaggerContextRoot => this._configuration["Swagger:ContextRoot"]!;
}