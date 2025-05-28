namespace GnuHiveApi.Common.config;

public interface IApplicationConfig
{
    string Environment { get; }
    string SwaggerContextRoot { get; }
}