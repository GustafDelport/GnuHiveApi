using System.Net;
using Asp.Versioning.ApiExplorer;
using GnuHiveApi.Common;
using GnuHiveApi.Common.config;
using GnuHiveApi.WebApi;
using GnuHiveApi.WebApi.Hubs;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var app = WebApiBuilder.Build(args);
var versionsProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
var config = app.Services.GetService<IApplicationConfig>()!;

switch (config.Environment)
{
    case GnuHiveApiEnvironments.Staging:
    {
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(hostname => true)
            .WithExposedHeaders("content-disposition"));
        break;
    }
    default:
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseCors(x => x
            .WithExposedHeaders("content-disposition")
            .AllowCredentials());
        break;
}

var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All,
    ForwardLimit = 10,
};
forwardedHeadersOptions.KnownNetworks.Clear();
forwardedHeadersOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardedHeadersOptions);
app.UseStaticFiles();

// app.UseHttpLogging();

// TODO When adding SignalR
//app.MapHub<AppNotificationHub>(AppNotificationHub.HUB_ENDPOINT);

app.UseSwagger(c =>
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers =
        new List<OpenApiServer> { new() { Url = $"https://{httpReq.Host.Value}/{config.SwaggerContextRoot}" } }));

app.UseSwaggerUI(b =>
{
    foreach (var description in versionsProvider.ApiVersionDescriptions)
    {
        b.SwaggerEndpoint($"/{config.SwaggerContextRoot}swagger/{description.GroupName}/swagger.json",
            $"Gnu Hive - {description.GroupName.ToUpper()}");
    }
});

app.MapControllers();

ServicePointManager.SecurityProtocol =
    SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

app.Run();