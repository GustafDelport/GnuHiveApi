using ErrorOr;
using GnuHiveApi.UseCaseFacade.GreenHouse.Models;

namespace GnuHiveApi.UseCaseFacade.GreenHouse;

public class GreenHouseUseCaseFacade : IGreenHouseUseCaseFacade
{
    public async Task<ErrorOr<Success>> GetSensoryDataInRangeAsync(DateTime fromDate, DateTime toDate)
    {
        return Error.Unexpected(description: "Something went wrong");
    }

    public async Task<ErrorOr<Success>> SetGreenHouseModuleStateAsync(IGreenHouseModuleValuesInputModel inputModel)
    {
        return Error.Unexpected(description: "Something went wrong");
    }

    public async Task<ErrorOr<Success>> SetGreenHouseModulesStateAsync(IGreenHouseModuleInputModel inputModel)
    {
        return Error.Unexpected(description: "Something went wrong");
    }
}