using Asp.Versioning;
using GnuHiveApi.Domain.Common.Enums;
using GnuHiveApi.UseCaseFacade.GreenHouse;
using GnuHiveApi.WebApi.Extensions;
using GnuHiveApi.WebApi.Models.GreenHouse;
using Microsoft.AspNetCore.Mvc;

namespace GnuHiveApi.WebApi.Controllers;

[Route("api/v1/green-house/")]
[ApiController]
[ApiVersion("1.0")]
public class GreenHouseController : ControllerBase
{
    private readonly IGreenHouseUseCaseFacade _greenHouseUseCaseFacade;

    public GreenHouseController(IGreenHouseUseCaseFacade greenHouseUseCaseFacade)
    {
        this._greenHouseUseCaseFacade = greenHouseUseCaseFacade;
    }
    
    [HttpPost("module")]
    public async Task<IActionResult> SetModule(
        [FromBody] GreenHouseModuleInputModel inputModel)
    {
        var result = await this._greenHouseUseCaseFacade.SetGreenHouseModuleStateAsync(inputModel);
        
        return result.MatchFirst<IActionResult>(
            _ => this.Ok(result.Value),
            this.FromError);
    }
    
    [HttpPost("node/{nodeId}/data")]
    public async Task<IActionResult> GetTemperature()
    {
        var today = DateTime.Today;
        var result = await this._greenHouseUseCaseFacade.GetSensoryDataInRangeAsync(today.AddDays(-5), today);

        return result.MatchFirst<IActionResult>(
            _ => this.Ok(result.Value),
            this.FromError);
    }
    
    [HttpGet("sensory-data")]
    public async Task<IActionResult> GetSensorDatas()
    {
        var today = DateTime.Today;
        var result = await this._greenHouseUseCaseFacade.GetSensoryDataInRangeAsync(today.AddDays(-5), today);

        return result.MatchFirst<IActionResult>(
            _ => this.Ok(result.Value),
            this.FromError);
    }
    
    [HttpGet("sensory-data")]
    public async Task<IActionResult> GetSensorData()
    {
        var today = DateTime.Today;
        var result = await this._greenHouseUseCaseFacade.GetSensoryDataInRangeAsync(today.AddDays(-5), today);

        return result.MatchFirst<IActionResult>(
            _ => this.Ok(result.Value),
            this.FromError);
    }
}