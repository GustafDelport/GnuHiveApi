using GnuHiveApi.Domain.Common.Enums;

namespace GnuHiveApi.UseCaseFacade.GreenHouse.Models;

public interface IGreenHouseModuleInputModel
{
    public GreenHouseModulesEnum Module { get; protected set; }
    public ToggleStateEnum State { get; protected set; }
}