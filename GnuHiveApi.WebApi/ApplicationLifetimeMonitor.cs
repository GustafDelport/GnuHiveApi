using GnuHiveApi.Common.config;

namespace GnuHiveApi.WebApi;

public class ApplicationLifetimeMonitor : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IApplicationConfig _appConfig;

    public ApplicationLifetimeMonitor(
        IHostApplicationLifetime appLifetime, 
        IApplicationConfig appConfig)
    {
        _appLifetime = appLifetime;
        _appConfig = appConfig;
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void OnApplicationStarted()
    {
        // TODO Send Email or SMS
        /*this._alertingService.PostAlertAsync("Accounts api has started.", "RPP_AccountsApi",
            this._applicationConfig.TeamsNotificationChannelConfig.Ops).Wait();*/
    }

    private void OnApplicationStopped()
    {
        // TODO Send Email or SMS
        /*this._alertingService.PostAlertAsync("Accounts api has stopped.", "RPP_AccountsApi", this._applicationConfig.TeamsNotificationChannelConfig.Ops).Wait();*/
    }
}