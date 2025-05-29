using ErrorOr;
using GnuHiveApi.AntiCorruption.MqttPublisher;

namespace GnuHiveApi.Integrations.MqttPublisher;

public class MqttPublisherService : IMqttPublisherService
{
    public Task<ErrorOr<Success>> SetModuleStateAsync(string moduleState)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<Success>> SetModulesStateAsync(string moduleState)
    {
        throw new NotImplementedException();
    }
}