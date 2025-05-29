using GnuHiveApi.Domain.Common.Enums;
using GnuHiveApi.UseCaseFacade.GreenHouse.Models;

namespace GnuHiveApi.WebApi.Models.GreenHouse;

public class GreenHouseModuleValuesInputModel : IGreenHouseModuleValuesInputModel
{
    public GreenHouseModulesEnum Module { get; set; }
    
    public ToggleStateEnum State { get; set; }
}

public class GreenHouseModuleInputModel : IGreenHouseModuleInputModel
{
    public ICollection<GreenHouseModuleValuesInputModel> ModuleValues { get; set; } = null!;
    ICollection<IGreenHouseModuleValuesInputModel> IGreenHouseModuleInputModel.ModuleValues =>
        this.ModuleValues.ToList<IGreenHouseModuleValuesInputModel>();
}