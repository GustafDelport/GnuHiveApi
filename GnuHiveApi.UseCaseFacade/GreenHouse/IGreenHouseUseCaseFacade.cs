using ErrorOr;

namespace GnuHiveApi.UseCaseFacade.GreenHouse;

public interface IGreenHouseUseCaseFacade
{
    Task<ErrorOr<Success>> GetSensoryDataInRangeAsync(DateTime fromDate, DateTime toDate);
}