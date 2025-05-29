using Asp.Versioning;
using GnuHiveApi.UseCaseFacade.GreenHouse;
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

    [HttpPost("light/{state}")]
    public async Task<IActionResult> ToggleLed(string state)
    {
        return this.Ok();
    }
    
    [HttpPost("fan/{state}")]
    public async Task<IActionResult> ToggleFan(string state)
    {
        return this.Ok();
    }
    
    [HttpPost("pump/{state}")]
    public async Task<IActionResult> TogglePump(string state)
    {
        return this.Ok();
    }
    
    [HttpGet("sensory-data")]
    public IActionResult GetTemperature()
    {
        return this.Ok();
    }
}