using GameLibrary.Domain.Models;

namespace GameLibrary.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllGenres(CancellationToken cancellationToken);

    Task<GenreDto> GetGenreById(int id, CancellationToken cancellationToken);

    Task<bool> Create(GameDto gameDto, CancellationToken cancellationToken);
}