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
    

    public async Task<bool> Create(GenreDto genreDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(genreDto.Name))
        {
            throw new ArgumentException();
        }

        var newGenre = new GenreEntity
        {
            Name = genreDto.Name,
            Description = genreDto.Description
        };

        GameLibraryDbContext.Genres.Add(newGenre);

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Update(int id, GenreDto genreDto, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var genreToUpdate = await GameLibraryDbContext.Genres
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (genreToUpdate is null)
        {
            throw new NullReferenceException();
        }

        genreToUpdate.Name = genreDto.Name ?? genreToUpdate.Name;
        genreToUpdate.Description = genreDto.Description ?? genreToUpdate.Description;

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
    
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            throw new Exception("Invalid ID");
        }
            
        var genreToDelete = await GameLibraryDbContext.Genres
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
            
        if (genreToDelete is null)
        {
            throw new NullReferenceException();
        }

        GameLibraryDbContext.Genres.Remove(genreToDelete);

        await GameLibraryDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}