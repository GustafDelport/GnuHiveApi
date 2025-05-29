using ErrorOr;

namespace GnuHiveApi.AntiCorruption.MqttPublisher;

public interface IMqttPublisherService
{
    Task<ErrorOr<Success>> SetModuleStateAsync(string genericPublisherMessage, string topic);
}