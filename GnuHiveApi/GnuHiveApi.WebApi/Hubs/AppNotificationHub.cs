using Microsoft.AspNetCore.SignalR;

namespace GnuHiveApi.WebApi.Hubs;

public class AppNotificationHub : Hub
{
    public static string HUB_ENDPOINT = "/signalr/hub/app";
    
    // TODO 
}