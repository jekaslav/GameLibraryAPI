using GameLibrary.Domain.Models;
using GameLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers;

public class DeveloperController : ControllerBase
{
    private IDeveloperService DeveloperService { get; }

    public DeveloperController(IDeveloperService developerService)
    {
        DeveloperService = developerService;
    }

    [HttpGet("developers")]
    public async Task<IActionResult> GetAllDevelopers(CancellationToken cancellationToken)
    {
        var result = await DeveloperService.GetAllDevelopers(cancellationToken);

        return Ok(result);
    }

    [HttpGet("developers/{id}")]
    public async Task<IActionResult> GetDeveloperById(int id, CancellationToken cancellationToken)
    {
        var result = await DeveloperService.GetDeveloperById(id, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost("developers")]
    public async Task<IActionResult> Create([FromBody] DeveloperDto developerDto, CancellationToken cancellationToken)
    {
        var result = await DeveloperService.Create(developerDto, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost("developers/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DeveloperDto developerDto, CancellationToken cancellationToken)
    {
        var result = await DeveloperService.Update(id, developerDto, cancellationToken);

        return Ok(result);
    }
    
    [HttpDelete("developers/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await DeveloperService.Delete(id, cancellationToken);

        return Ok(result);
    }
}