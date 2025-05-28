using System.Reflection;
using GnuHiveApi.Common;
using GnuHiveApi.Common.config;
using GnuHiveApi.Common.DateTime;

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
        this.RegisterApi(services);
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
        
    }
    
    protected virtual void RegisterInfrastructure(IServiceCollection services, IConfiguration builderConfiguration)
    {
        
    }
    
    protected virtual void RegisterIntegrations(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegisterApi(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegisterWebApi(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegisterMappers(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegisterLogging(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegisterValidators(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegisterHttpClients(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegisterSanitizers(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegistrationBackgroundJobs(IServiceCollection services)
    {
        
    }
    
    protected virtual void RegisterDomainServices(IServiceCollection services)
    {
        
    }
}