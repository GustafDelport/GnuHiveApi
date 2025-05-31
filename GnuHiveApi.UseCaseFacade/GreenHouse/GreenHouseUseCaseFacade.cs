using System.Text.Json;
using ErrorOr;
using GnuHiveApi.AntiCorruption.MqttPublisher;
using GnuHiveApi.Common.Constants;
using GnuHiveApi.Common.Logging;
using GnuHiveApi.UseCaseFacade.GreenHouse.Models;

namespace GnuHiveApi.UseCaseFacade.GreenHouse;

public class GreenHouseUseCaseFacade : IGreenHouseUseCaseFacade
{
    private readonly IMqttPublisherService _mqttPublisherService;
    //private readonly ILogLogger _logger;

    public GreenHouseUseCaseFacade(
        IMqttPublisherService mqttPublisherService)
    {
        this._mqttPublisherService = mqttPublisherService;
    }

    public async Task<ErrorOr<Success>> GetSensoryDataInRangeAsync(DateTime fromDate, DateTime toDate)
    {
        return Error.Unexpected(description: "Something went wrong");
    }

    public async Task<ErrorOr<Success>> SetGreenHouseModuleStateAsync(IGreenHouseModuleInputModel inputModel)
    {
        var genericMessage = JsonSerializer.Serialize(inputModel);
        
        var publishResult = await this._mqttPublisherService.SetModuleStateAsync(genericMessage, MqttTopics.GreenHouseSetModuleState);

        if (publishResult.IsError)
        {
            //this._logger.Error(publishResult.FirstError);
            return publishResult.FirstError;
        }

        //this._logger.Info($"Message: {genericMessage} => publish result: {publishResult.Value}");
        return Result.Success;
    }
}