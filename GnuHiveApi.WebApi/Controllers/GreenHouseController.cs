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
    
    [HttpPost("nodes/{nodeId}/module")]
    public async Task<IActionResult> SetNodeModule(
        int nodeId,
        [FromBody] GreenHouseModuleInputModel inputModel)
    {
        var result = await this._greenHouseUseCaseFacade.SetGreenHouseModuleStateAsync(nodeId, inputModel);
        
        return result.MatchFirst<IActionResult>(
            _ => this.Ok(result.Value),
            this.FromError);
    }
    
    [HttpPost("nodes/{nodeId}/data")]
    public async Task<IActionResult> ReceiveNodeData(
        int nodeId,
        [FromBody] GreenHouseNodeDataInputModel inputModel
        )
    {
        var today = DateTime.Today;
        var result = await this._greenHouseUseCaseFacade.ReceiveGreenHouseDataAsync(nodeId, inputModel); 

        return result.MatchFirst<IActionResult>(
            _ => this.Ok(result.Value),
            this.FromError);
    }
    
    [HttpGet("nodes/{nodeId}/data")]
    public async Task<IActionResult> GetNodeData(int nodeId)
    {
        var today = DateTime.Today;
        var result = await this._greenHouseUseCaseFacade.GetGreenHouseNodeDataAsyncAsync(nodeId,
            today.AddDays(-5), today);

        return result.MatchFirst<IActionResult>(
            _ => this.Ok(result.Value),
            this.FromError);
    }
    
    [HttpGet("nodes/data")]
    public async Task<IActionResult> GetAllNodesData()
    {
        var today = DateTime.Today;
        var result = await this._greenHouseUseCaseFacade.GetGreenHouseNodesDataAsyncAsync(today.AddDays(-5), today);

        return result.MatchFirst<IActionResult>(
            _ => this.Ok(result.Value),
            this.FromError);
    }
}