using System.Reflection;
using GnuHiveApi.AntiCorruption.MqttPublisher;
using GnuHiveApi.BackgroundProcessing.BackgroundListeners;
using GnuHiveApi.Common;
using GnuHiveApi.Common.config;
using GnuHiveApi.Common.DateTime;
using GnuHiveApi.Common.Extensions;
using GnuHiveApi.Common.Logging;
using GnuHiveApi.Common.Utils;
using GnuHiveApi.Integrations.MqttPublisher;
using GnuHiveApi.Persistence.Command;
using GnuHiveApi.Persistence.Common;
using GnuHiveApi.UseCaseFacade.GreenHouse;
using GnuHiveApi.WebApi.Swagger;
using Microsoft.Extensions.Options;
using NLog;
using Swashbuckle.AspNetCore.SwaggerGen;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace GnuHiveApi.WebApi.Installers;

public abstract class WebApiInstaller
{
    private readonly Assembly _adoAssembly;
    private readonly Assembly _dapperAssembly;

    public WebApiInstaller()
    {
        this._adoAssembly = Assembly.GetExecutingAssembly();
        this._dapperAssembly = Assembly.GetExecutingAssembly();
    }

    public static WebApiInstaller Create(string environment)
    {
        switch (environment)
        {
            case GnuHiveApiEnvironments.Staging: return new WebApiInstallerDevelopment();
            case GnuHiveApiEnvironments.Development: return new WebApiInstallerDevelopment();
            case GnuHiveApiEnvironments.RemoteDevelopment: return new WebApiInstallerRemoteDevelopment();
            case GnuHiveApiEnvironments.Sit: return new WebApiInstallerSit();
            case GnuHiveApiEnvironments.Production: return new WebApiInstallerProd();
            default: return new WebApiInstallerDevelopment();
        }
    }

    public IServiceCollection RegisterServices(IServiceCollection services, IConfiguration builderConfiguration)
    {
        this.RegisterCommon(services);
        this.RegisterPersistence(services);
        this.RegisterInfrastructure(services, builderConfiguration);
        this.RegisterIntegrations(services);
        this.RegisterFacade(services);
        this.RegisterWebApi(services);
        this.RegisterMappers(services);
        this.RegisterLogging(services);
        this.RegisterValidators(services);
        this.RegisterHttpClients(services);
        this.RegisterSanitizers(services);
        this.RegistrationBackgroundJobs(services);
        this.RegisterDomainServices(services);
        
        return services;
    }

    protected virtual void RegisterCommon(IServiceCollection services)
    {
        services.AddSingleton<IApplicationConfig, ApplicationConfig>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
    }

    protected virtual void RegisterPersistence(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddScoped<IDatabaseConnectionProvider, SqlDatabaseConnectionProvider>();
        
        // services.RegisterByConvention(this._dapperAssembly, ".*DataReader");
        // services.RegisterByConvention(this._adoAssembly, ".*Command");
    }
    
    protected virtual void RegisterInfrastructure(IServiceCollection services, IConfiguration builderConfiguration)
    {
        // TODO Add when needed
    }
    
    protected virtual void RegisterIntegrations(IServiceCollection services)
    {
        services.AddScoped<IMqttPublisherService, MqttPublisherService>();
    }
    
    protected virtual void RegisterFacade(IServiceCollection services)
    {
        services.AddScoped<IGreenHouseUseCaseFacade, GreenHouseUseCaseFacade>();
    }
    
    protected virtual void RegisterWebApi(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddSwaggerGen(o =>
        {
            var filePath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, filePath));

            o.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            o.OperationFilter<SwaggerDefaultValues>();

            /*o.AddSecurityDefinition("Bearer",
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
            });*/

            o.CustomSchemaIds(type => type.ToString());
        });
        
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
    }
    
    protected virtual void RegisterMappers(IServiceCollection services)
    {
        // TODO Add when needed
    }
    
    protected virtual void RegisterLogging(IServiceCollection services)
    {
        services.AddScoped<ILogLogger, NLogLogger>();
        services.AddSingleton<NLog.ILogger>(sp =>
        {
            var config = sp.GetService<IApplicationConfig>();
            var logMapper = sp.GetRequiredService<NLogLogLevelMapper>();
            
            var logger = LogManager.Setup(x => x.LoadConfiguration(builder =>
            {
                builder.Configuration =
                    new NLogConfigurationBuilder(config!.AppName, config, logMapper)
                        .WithDbLogging(config.MainDbConnectionString)
                        .WithConsoleLogging()
                        .Build();

                LogManager.Configuration = builder.Configuration;
            })).GetLogger(config!.AppName ?? "WebApi");
            
            return logger;
        });
    }
    
    protected virtual void RegisterValidators(IServiceCollection services)
    {
        // TODO Add when needed
    }
    
    protected virtual void RegisterHttpClients(IServiceCollection services)
    {
        // TODO Add when needed
    }
    
    protected virtual void RegisterSanitizers(IServiceCollection services)
    {
        // TODO Add when needed
    }
    
    protected virtual void RegistrationBackgroundJobs(IServiceCollection services)
    {
        // TODO Add when needed
        services.AddHostedService<MqttBackgroundService>();
    }
    
    protected virtual void RegisterDomainServices(IServiceCollection services)
    {
        // TODO Add when needed
    }
}