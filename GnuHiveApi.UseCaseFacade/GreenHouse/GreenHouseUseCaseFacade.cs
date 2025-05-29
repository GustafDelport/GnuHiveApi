using ErrorOr;

namespace GnuHiveApi.UseCaseFacade.GreenHouse;

public class GreenHouseUseCaseFacade : IGreenHouseUseCaseFacade
{
    public Task<ErrorOr<Success>> GetSensoryDataInRangeAsync(DateTime fromDate, DateTime toDate)
    {
        throw new NotImplementedException();
    }
}