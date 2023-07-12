using AutoMapper;
using GameLibrary.Domain.Contexts;
using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Models;
using GameLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Services.Services;

public class DeveloperService : IDeveloperService
{
    private GameLibraryDbContext GameLibraryDbContext { get; }
    
    private IMapper Mapper { get; }

    public DeveloperService(GameLibraryDbContext context, IMapper mapper)
    {
        GameLibraryDbContext = context;
        Mapper = mapper;
    }

    public async Task<IEnumerable<DeveloperDto>> GetAllDevelopers(CancellationToken cancellationToken)
    {
        var developers = await GameLibraryDbContext.Developers
            .AsNoTracking()
            .Select(x => Mapper.Map<DeveloperDto>(x))
            .ToListAsync(cancellationToken);

        return developers;
    }

    public async Task<DeveloperDto> GetDeveloperById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
            
        }

        var developer = await GameLibraryDbContext.Developers
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        var result = Mapper.Map<DeveloperDto>(developer);

        return result;
    }

    public async Task<bool> Create(DeveloperDto developerDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(developerDto.Name))
        {
            throw new ArgumentException();
        }

        if (string.IsNullOrWhiteSpace(developerDto.Email))
        {
            throw new ArgumentException();
        }

        var newDeveloper = new DeveloperEntity
        {
            Name = developerDto.Name,
            Email = developerDto.Email
        };

        GameLibraryDbContext.Developers.Add(newDeveloper);

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Update(int id, DeveloperDto developerDto, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var developerToUpdate = await GameLibraryDbContext.Developers
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (developerToUpdate is null)
        {
            throw new NullReferenceException();
        }

        developerToUpdate.Name = developerDto.Name ?? developerToUpdate.Name;
        developerToUpdate.Email = developerDto.Email ?? developerToUpdate.Email;

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var developerToDelete = await GameLibraryDbContext.Developers
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (developerToDelete is null)
        {
            throw new NullReferenceException();
        }

        GameLibraryDbContext.Developers.Remove(developerToDelete);

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}