using GameLibrary.Domain.Models;
using GameLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.API.Controllers;

public class GenreController : ControllerBase
{
    private IGenreService GenreService { get; }

    public GenreController(IGenreService genreService)
    {
        GenreService = genreService;
    }

    [HttpGet("genres")]
    public async Task<IActionResult> GetAllGenres(CancellationToken cancellationToken)
    {
        var result = await GenreService.GetAllGenres(cancellationToken);

        return Ok(result);
    }

    [HttpGet("genres/{id}")]
    public async Task<IActionResult> GetGenreById(int id, CancellationToken cancellationToken)
    {
        var result = await GenreService.GetGenreById(id, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost("genres")]
    public async Task<IActionResult> Create([FromBody] GenreDto genreDto, CancellationToken cancellationToken)
    {
        var result = await GenreService.Create(genreDto, cancellationToken);

        return Ok(result);
    }
    
    [HttpPost("genres/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GenreDto genreDto, CancellationToken cancellationToken)
    {
        var result = await GenreService.Update(id, genreDto, cancellationToken);

        return Ok(result);
    }
    
    [HttpDelete("genres/{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await GenreService.Delete(id, cancellationToken);

        return Ok(result);
    }
}