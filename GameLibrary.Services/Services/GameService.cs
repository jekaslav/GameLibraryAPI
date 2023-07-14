using GameLibrary.Domain.Contexts;
using GameLibrary.Domain.Entities;
using GameLibrary.Domain.Models;
using GameLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Services.Services;

public class GameService : IGameService
{
    private GameLibraryDbContext GameLibraryDbContext { get; }

    public GameService(GameLibraryDbContext context)
    {
        GameLibraryDbContext = context;
    }
    
    public async Task<IEnumerable<GameDto>> GetAllGames(CancellationToken cancellationToken)
    {
        var games = await GameLibraryDbContext.Games
            .AsNoTracking()
            .Include(g => g.GameGenres)
            .ThenInclude(gg => gg.Genre)
            .Select(x => new GameDto
            {
                Id = x.Id,
                Title = x.Title,
                DeveloperId = x.DeveloperId,
                GenreIds = x.GameGenres.Select(gg => gg.GenreId).ToList()
            })
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
            .Include(g => g.GameGenres)
            .ThenInclude(gg => gg.Genre)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        var result = new GameDto
        {
            Id = game!.Id,
            Title = game.Title,
            DeveloperId = game.DeveloperId,
            GenreIds = game.GameGenres.Select(gg => gg.GenreId).ToList()
        };

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
            GameGenres = gameDto.GenreIds.Select(genreId => new GameGenreEntity { GenreId = genreId }).ToList()
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
            .Include(g => g.GameGenres)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (gameToUpdate is null)
        {
            throw new NullReferenceException();
        }

        gameToUpdate.Title = gameDto.Title ?? gameToUpdate.Title;
        
        GameLibraryDbContext.GameGenres.RemoveRange(gameToUpdate.GameGenres);
        
        gameToUpdate.GameGenres = gameDto.GenreIds.Select(genreId => new GameGenreEntity { GenreId = genreId }).ToList();

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid ID");
            }

            var gameToDelete = await GameLibraryDbContext.Games
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (gameToDelete is null)
            {
                throw new NullReferenceException();
            }

            GameLibraryDbContext.Games.Remove(gameToDelete);

            await GameLibraryDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
