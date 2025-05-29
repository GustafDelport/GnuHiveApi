using Microsoft.Extensions.Configuration;

namespace GnuHiveApi.Common.config;

public class HiveMqConfig : IHiveMqConfig {
    private readonly IConfiguration _configuration;

    public HiveMqConfig(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public string UserName => this._configuration.GetValue<string>("Hive:UserName") ?? "";
    public string Password => this._configuration.GetValue<string>("Hive:Password") ?? "";
    public string Url => this._configuration.GetValue<string>("Hive:Url") ?? "";
    public string WebSocket => this._configuration.GetValue<string>("Hive:WebSocket") ?? "";
    public int Port => this._configuration.GetValue<int>("Hive:Port");
}