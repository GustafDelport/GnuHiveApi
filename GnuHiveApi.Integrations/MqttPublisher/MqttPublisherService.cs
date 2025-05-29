using ErrorOr;
using GnuHiveApi.AntiCorruption.MqttPublisher;
using GnuHiveApi.Common.config;
using GnuHiveApi.Common.Constants;
using GnuHiveApi.Common.Extensions;
using GnuHiveApi.Common.Logging;
using MQTTnet;
using MQTTnet.Client;

namespace GnuHiveApi.Integrations.MqttPublisher;

public class MqttPublisherService : IMqttPublisherService
{
    private IMqttClient _mqttClient;
    private readonly IApplicationConfig _config;
    private readonly ILogLogger _logger;

    public MqttPublisherService(IMqttClient mqttClient,
        IApplicationConfig config,
        ILogLogger logger)
    {
        this._mqttClient = mqttClient;
        this._config = config;
        this._logger = logger;
    }

    public Task<ErrorOr<Success>> SetModuleStateAsync(string genericPublisherMessage, string topic)
    {
        return this.PublishAsync(genericPublisherMessage, topic);
    }

    public Task<ErrorOr<Success>> SetModulesStateAsync(string genericPublisherMessage)
    {
        return this.PublishAsync(genericPublisherMessage, MqttTopics.GreenHouseSetModuleState);
    }

    private async Task<ErrorOr<Success>> PublishAsync(string genericPublisherMessage, string topic)
    {
        var factory = new MqttFactory();
        this._mqttClient = factory.CreateMqttClient();
        
        var options = new MqttClientOptionsBuilder()
            .WithClientId("dotnetClient")
            .WithTcpServer(this._config.HiveMq.Url, this._config.HiveMq.Port)
            .WithCredentials(this._config.HiveMq.UserName, this._config.HiveMq.Password)
            .WithCleanSession()
            .WithTlsOptions(o => o.UseTls())
            .Build();
        
        try
        {
            await this._mqttClient.ConnectAsync(options);
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(genericPublisherMessage)
                .Build();
            
            await this._mqttClient.PublishAsync(message);
            await this._mqttClient.DisconnectAsync();
            
            return Result.Success;
        }
        catch (Exception ex)
        {
            this._logger.Error(ex.GetFullErrorMessage());
            return Error.Unexpected(description: "Unable to connect to the MQTT broker.");
        }
    }
}