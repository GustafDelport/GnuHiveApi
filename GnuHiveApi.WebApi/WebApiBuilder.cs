using System.Reflection;
using Asp.Versioning;
using GnuHiveApi.WebApi.Installers;
using GnuHiveApi.WebApi.Swagger;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Hosting;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        
        builder.Services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.ReportApiVersions = true;
            o.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("ver"));
        });

        builder.Services.AddApiVersioning(
                options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        
        builder.Services.AddSwaggerGen(o =>
        {
            var filePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, filePath));

            o.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            o.OperationFilter<SwaggerDefaultValues>();

            o.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            // Add bearer token to each endpoint
            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
                    },
                    Array.Empty<string>()
                }
            });

            o.CustomSchemaIds(type => type.ToString());
        });
        
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        
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