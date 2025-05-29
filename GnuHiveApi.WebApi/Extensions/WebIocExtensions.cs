using GnuHiveApi.Common.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace GnuHiveApi.WebApi.Extensions;

public static class WebIocExtensions
{
    public static void TryAddStackExchangeRedis(this ISignalRServerBuilder signalrBuilder, string? redisConnectionString)
    {
        if (redisConnectionString is null)
        {
            return;
        }

        try
        {
            signalrBuilder.AddStackExchangeRedis(redisConnectionString);
        }
        catch (Exception e)
        {
            Console.Write(e.GetFullErrorMessage());
        }
    }
}