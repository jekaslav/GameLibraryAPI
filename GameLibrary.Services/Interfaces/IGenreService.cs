using GameLibrary.Domain.Models;

namespace GameLibrary.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllGenres(CancellationToken cancellationToken);

    Task<GenreDto> GetGenreById(int id, CancellationToken cancellationToken);

    Task<bool> Create(GenreDto genreDto, CancellationToken cancellationToken);

    Task<bool> Update(int id, GenreDto genreDto, CancellationToken cancellationToken);

    Task<bool> Delete(int id, CancellationToken cancellationToken);
}