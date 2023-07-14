using GameLibrary.Domain.Models;

namespace GameLibrary.Services.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameDto>> GetAllGames(CancellationToken cancellationToken);

    Task<GameDto> GetGameById(int id, CancellationToken cancellationToken);

    Task<bool> Create(GameDto gameDto, CancellationToken cancellationToken);

    Task<bool> Update(int id, GameDto gameDto, CancellationToken cancellationToken);

    Task<bool> Delete(int id, CancellationToken cancellationToken);
}