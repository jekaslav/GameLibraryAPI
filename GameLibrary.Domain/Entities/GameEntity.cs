namespace GameLibrary.Domain.Entities;

public class GameEntity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public int DeveloperId { get; set; }
    
    public DeveloperEntity Developer { get; set; }
    
    public ICollection<GenreEntity> Genres { get; set; }
}