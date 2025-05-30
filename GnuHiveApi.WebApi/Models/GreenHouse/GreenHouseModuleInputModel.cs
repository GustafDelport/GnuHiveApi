using GnuHiveApi.Domain.Common.Enums;
using GnuHiveApi.UseCaseFacade.GreenHouse.Models;

namespace GnuHiveApi.WebApi.Models.GreenHouse;

public class GreenHouseModuleInputModel : IGreenHouseModuleInputModel
{
    public GreenHouseModulesEnum Module { get; set; }
    
    public ToggleStateEnum State { get; set; }
}