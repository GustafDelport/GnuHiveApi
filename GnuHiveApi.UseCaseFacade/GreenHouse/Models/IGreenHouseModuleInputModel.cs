using GnuHiveApi.Domain.Common.Enums;

namespace GnuHiveApi.UseCaseFacade.GreenHouse.Models;

public interface IGreenHouseModuleValuesInputModel
{
    public GreenHouseModulesEnum Module { get; protected set; }
    
    public ToggleStateEnum State { get; protected set; }
}

public interface IGreenHouseModuleInputModel
{
    public ICollection<IGreenHouseModuleValuesInputModel> ModuleValues { get; }
}