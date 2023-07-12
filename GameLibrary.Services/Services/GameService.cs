using AutoMapper;
using GameLibrary.Domain.Contexts;
using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Models;
using GameLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Services.Services;

public class GameService : IGameService
{
    private GameLibraryDbContext GameLibraryDbContext { get; }
    
    private IMapper Mapper { get; }

    public GameService(GameLibraryDbContext context, IMapper mapper)
    {
        GameLibraryDbContext = context;
        Mapper = mapper;
    }
    
    public async Task<IEnumerable<GameDto>> GetAllGames(CancellationToken cancellationToken)
    {
        var games = await GameLibraryDbContext.Games
            .AsNoTracking()
            .Select(x => Mapper.Map<GameDto>(x))
            .ToListAsync(cancellationToken);

        return games;
    }

    public async Task<GameDto> GetGameById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
            
        }

        var game = await GameLibraryDbContext.Games
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        var result = Mapper.Map<GameDto>(game);

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
            DeveloperId = gameDto.DeveloperId,
            Genres = gameDto.Genres.Select(genreId => new GenreEntity { Id = genreId }).ToList()
        };

        GameLibraryDbContext.Games.Add(newGame);

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    
    public async Task<bool> Update(int id, GameDto gameDto, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var gameToUpdate = await GameLibraryDbContext.Games
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (gameToUpdate is null)
        {
            throw new NullReferenceException();
        }

        gameToUpdate.Title = gameDto.Title ?? gameToUpdate.Title;

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}