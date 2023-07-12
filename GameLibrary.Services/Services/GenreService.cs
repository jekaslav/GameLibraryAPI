using AutoMapper;
using GameLibrary.Domain.Contexts;
using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Models;
using GameLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Services.Services;

public class GenreService : IGenreService
{
    private GameLibraryDbContext GameLibraryDbContext { get; }
    
    private IMapper Mapper { get; }

    public GenreService(GameLibraryDbContext context, IMapper mapper)
    {
        GameLibraryDbContext = context;
        Mapper = mapper;
    }
    
    public async Task<IEnumerable<GenreDto>> GetAllGenres(CancellationToken cancellationToken)
    {
        var genres = await GameLibraryDbContext.Genres
            .AsNoTracking()
            .Select(x => Mapper.Map<GenreDto>(x))
            .ToListAsync(cancellationToken);

        return genres;
    }

    public async Task<GenreDto> GetGenreById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
            
        }

        var genre = await GameLibraryDbContext.Genres
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        var result = Mapper.Map<GenreDto>(genre);

        return result;
    }
    

    public async Task<bool> Create(GameDto gameDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(gameDto.Title))
        {
            throw new ArgumentException();
        }

        var newGame = new GameEntity
        {
            Title = gameDto.Title,
            
        };

        GameLibraryDbContext.Games.Add(newGame);

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    
}