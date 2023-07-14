namespace GameLibrary.Domain.Entities;

public class GenreEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public ICollection<GameGenreEntity> GameGenres { get; set; }
}