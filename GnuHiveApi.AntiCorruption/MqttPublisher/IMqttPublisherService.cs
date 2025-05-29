using ErrorOr;

namespace GnuHiveApi.AntiCorruption.MqttPublisher;

public interface IMqttPublisherService
{
    Task<ErrorOr<Success>> SetModuleStateAsync(string moduleState);
    
    Task<ErrorOr<Success>> SetModulesStateAsync(string moduleState);
}