using GameLibrary.Domain.Models;
using GameLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers;

public class GameController : ControllerBase
{
    private IGameService GameService { get; }

    public GameController(IGameService gameService)
    {
        GameService = gameService;
    }

    [HttpGet("games")]
    public async Task<IActionResult> GetAllGames(CancellationToken cancellationToken)
    {
        var result = await GameService.GetAllGames(cancellationToken);

        return Ok(result);
    }

    [HttpGet("games/{id}")]
    public async Task<IActionResult> GetGameById(int id, CancellationToken cancellationToken)
    {
        var result = await GameService.GetGameById(id, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost("games")]
    public async Task<IActionResult> Create([FromBody] GameDto gameDto, CancellationToken cancellationToken)
    {
        var result = await GameService.Create(gameDto, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost("games/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GameDto gameDto, CancellationToken cancellationToken)
    {
        var result = await GameService.Update(id, gameDto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("games/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await GameService.Delete(id, cancellationToken);

        return Ok(result);
    }
}