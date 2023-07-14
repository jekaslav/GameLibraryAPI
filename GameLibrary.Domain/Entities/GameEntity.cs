namespace GameLibrary.Domain.Entities;

public class GameEntity
{
    public int Id { get; set; } 
    
    public int DeveloperId { get; set; }   
    
    public string Title { get; set; } = null!;

    public DeveloperEntity Developer { get; set; } = null!;

    public ICollection<GameGenreEntity> GameGenres { get; set; } = null!;
}