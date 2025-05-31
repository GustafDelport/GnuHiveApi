using ErrorOr;
using GnuHiveApi.UseCaseFacade.GreenHouse.Models;

namespace GnuHiveApi.UseCaseFacade.GreenHouse;

public interface IGreenHouseUseCaseFacade
{
    Task<ErrorOr<Success>> SetGreenHouseModuleStateAsync(int nodeId, IGreenHouseModuleInputModel inputModel);
    Task<ErrorOr<Success>> ReceiveGreenHouseDataAsync(int nodeId, IGreenHouseNodeDataInputModel inputModel);
    
    // TODO Add a proper read model
    Task<ErrorOr<Success>> GetGreenHouseNodeDataAsyncAsync(int nodeId, DateTime fromDate, DateTime toDate);
    
    // TODO Add a proper read model
    Task<ErrorOr<Success>> GetGreenHouseNodesDataAsyncAsync(DateTime fromDate, DateTime toDate);
}