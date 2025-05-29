using ErrorOr;
using GnuHiveApi.UseCaseFacade.GreenHouse.Models;

namespace GnuHiveApi.UseCaseFacade.GreenHouse;

public interface IGreenHouseUseCaseFacade
{
    Task<ErrorOr<Success>> GetSensoryDataInRangeAsync(DateTime fromDate, DateTime toDate);
    Task<ErrorOr<Success>> SetGreenHouseModuleStateAsync(IGreenHouseModuleValuesInputModel inputModel);
    Task<ErrorOr<Success>> SetGreenHouseModulesStateAsync(IGreenHouseModuleInputModel inputModel);
}