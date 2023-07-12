using GameLibrary.Domain.Models;

namespace GameLibrary.Services.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameDto>> GetAllGames(CancellationToken cancellationToken);

    Task<GameDto> GetGameById(int id, CancellationToken cancellationToken);
    
    
}