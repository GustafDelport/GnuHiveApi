using ErrorOr;
using GnuHiveApi.AntiCorruption.MqttPublisher;
using GnuHiveApi.Common.config;
using GnuHiveApi.Common.Constants;
using GnuHiveApi.Common.Extensions;
using GnuHiveApi.Common.Logging;
using MQTTnet;
using MQTTnet.Client;

namespace GnuHiveApi.Integrations.MqttPublisher;

// TODO : NB => This is not perfect when you await this._mqttClient.ConnectAsync(options); you disconnect after pub it might be the scoped lifecycle. Try to find a way merge the background listener and this pub service to use the same IMqttClient or different once.
public class MqttPublisherService : IMqttPublisherService
{
    private readonly IApplicationConfig _config;
    private IMqttClient? _mqttClient;
    
    public MqttPublisherService(IApplicationConfig config)
    {
        this._config = config;
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
            .WithClientId("GnuHiveApi")
            .WithTcpServer(this._config.MqttConfig.Url, this._config.MqttConfig.Port)
            .WithCredentials(this._config.MqttConfig.UserName, this._config.MqttConfig.Password)
            .WithCleanSession()
            .WithTlsOptions(o => o.UseTls())
            .Build();
        
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(genericPublisherMessage)
            .Build();

        try
        {
            await this._mqttClient.ConnectAsync(options);
            Console.WriteLine("Connected to mqtt");
            
            await this._mqttClient.PublishAsync(message);
            Console.WriteLine("Message published");
            
            /*await this._mqttClient.DisconnectAsync();
            Console.WriteLine("Disconnected from mqtt");*/
            
            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: "Unable to connect to the MQTT broker.");
        }
    }
}