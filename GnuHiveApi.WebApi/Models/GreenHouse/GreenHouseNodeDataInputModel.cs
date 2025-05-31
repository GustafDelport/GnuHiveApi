using GnuHiveApi.Domain.Common.Enums;
using GnuHiveApi.UseCaseFacade.GreenHouse.Models;

namespace GnuHiveApi.WebApi.Models.GreenHouse;

public class GreenHouseNodeModuleInputModel : IGreenHouseNodeModuleInputModel
{
    public GreenHouseModulesEnum Module { get; set; }
    public ToggleStateEnum State { get; set; }
}

public class GreenHouseNodeDataInputModel : IGreenHouseNodeDataInputModel
{
    public decimal Temperature { get; set; }
    public decimal Humidity { get; set; }
    public decimal LightLevel { get; set; }
    public ICollection<GreenHouseNodeModuleInputModel> NodeModulesState { get; set; } = null!;
    
    ICollection<IGreenHouseNodeModuleInputModel> IGreenHouseNodeDataInputModel.NodeModulesState =>
        this.NodeModulesState.ToList<IGreenHouseNodeModuleInputModel>();
}