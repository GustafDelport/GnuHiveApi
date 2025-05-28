using GnuHiveApi.WebApi.Installers;
using NLog.Extensions.Hosting;
using NLog.Web;

namespace GnuHiveApi.WebApi;

public class WebApiBuilder 
{
    public static WebApplication Build(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var env = builder.Environment.EnvironmentName;
        var environment = builder.Environment;
        
        var sharedFolder = Path.Combine(builder.Environment.ContentRootPath, "..", "SharedConfig");
        
        if (!environment.IsDevelopment())
        {
            sharedFolder = "SharedConfig";
        }
        
        builder.Configuration
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{env}.json", false, true)
            .AddJsonFile(Path.Combine(sharedFolder, "appsettings.json"), false, true)
            .AddJsonFile(Path.Combine(sharedFolder, $"appsettings.{env}.json"), false, true)
            .AddEnvironmentVariables();
        
        builder.Logging.ClearProviders();
        ConfigureExtensions.UseNLog(AspNetExtensions.UseNLog(builder.Host));
        builder.Logging.AddConsole();
        builder.Services.AddHostedService<ApplicationLifetimeMonitor>();
        
        var iocInstaller = WebApiInstaller.Create(environment.EnvironmentName);

        iocInstaller.RegisterServices(builder.Services, builder.Configuration);
        
        /*SqlMapper.AddTypeHandler(typeof(ICollection<string>), new JsonTypeHandler(true));
        SqlMapper.AddTypeHandler(typeof(IDictionary<string, string>), new JsonTypeHandler(true));*/
        
        var host = builder.Build();
        return host;
    }
}