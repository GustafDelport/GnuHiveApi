using GnuHiveApi.Domain.Common.Enums;

namespace GnuHiveApi.UseCaseFacade.GreenHouse.Models;

public interface IGreenHouseNodeModuleInputModel
{
    public GreenHouseModulesEnum Module { get; set; }
    public ToggleStateEnum State { get; set; }
}

public interface IGreenHouseNodeDataInputModel
{
    public decimal Temperature { get; }
    public decimal Humidity { get; }
    public decimal LightLevel { get; }
    public ICollection<IGreenHouseNodeModuleInputModel> NodeModulesState { get; }
}