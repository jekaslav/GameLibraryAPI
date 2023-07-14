using GameLibrary.Domain.Models;

namespace GameLibrary.Services.Interfaces;

public interface IDeveloperService
{
    Task<IEnumerable<DeveloperDto>> GetAllDevelopers(CancellationToken cancellationToken);

    Task<DeveloperDto> GetDeveloperById(int id, CancellationToken cancellationToken);

    Task<bool> Create(DeveloperDto developerDto, CancellationToken cancellationToken);

    Task<bool> Update(int id, DeveloperDto developerDto, CancellationToken cancellationToken);

    Task<bool> Delete(int id, CancellationToken cancellationToken);
}