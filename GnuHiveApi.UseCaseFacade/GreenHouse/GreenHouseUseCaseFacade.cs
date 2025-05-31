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

    public async Task<ErrorOr<Success>> SetGreenHouseModuleStateAsync(int nodeId, IGreenHouseModuleInputModel inputModel)
    {
        // TODO Use nodeId to do a lookup on what node / device we need to update
        
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

    public async Task<ErrorOr<Success>> ReceiveGreenHouseDataAsync(int nodeId,
        IGreenHouseNodeDataInputModel inputModel)
    {
        // TODO Complete when database is setup.
        var genericMessage = JsonSerializer.Serialize(inputModel);
        Console.WriteLine($"Message received from {nodeId} : {genericMessage}");
        
        return Result.Success;
    }

    // TODO Add a proper read model
    public Task<ErrorOr<Success>> GetGreenHouseNodeDataAsyncAsync(int nodeId,
        DateTime fromDate,
        DateTime toDate)
    {
        throw new NotImplementedException();
    }

    // TODO Add a proper read model
    public Task<ErrorOr<Success>> GetGreenHouseNodesDataAsyncAsync(DateTime fromDate,
        DateTime toDate)
    {
        throw new NotImplementedException();
    }
}