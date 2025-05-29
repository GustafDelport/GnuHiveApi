using System.Text;
using GnuHiveApi.BackgroundProcessing.Constants;
using GnuHiveApi.Common.config;
using GnuHiveApi.Common.Extensions;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;

namespace GnuHiveApi.BackgroundProcessing.BackgroundListeners;

public class MqttBackgroundService : BackgroundService
{
    private IApplicationConfig _config;
    private IMqttClient? _mqttClient;

    public MqttBackgroundService(IApplicationConfig config)
    {
        this._config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

        this._mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            
            Console.WriteLine($"Received MQTT message: Topic = {topic}, Payload = {payload}");
            
            return Task.CompletedTask;
        };

        this._mqttClient.ConnectedAsync += async e =>
        {
            Console.WriteLine("Connected to MQTT Broker.");
            
            await this._mqttClient.SubscribeAsync(ListenerTopic.GreenHouseSensorData, cancellationToken: stoppingToken);
            Console.WriteLine($"Subscribed to topic: {ListenerTopic.GreenHouseSensorData}");
        };

        this._mqttClient.DisconnectedAsync += e =>
        {
            Console.WriteLine("Disconnected from MQTT Broker. Will retry...");
            return Task.CompletedTask;
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            if (!this._mqttClient.IsConnected)
            {
                try
                {
                    await this._mqttClient.ConnectAsync(options, stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetFullErrorMessage());
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (this._mqttClient?.IsConnected == true)
        {
            await this._mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
        }

        await base.StopAsync(cancellationToken);
    }
}