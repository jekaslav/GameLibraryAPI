namespace GameLibrary.Domain.Models;

public class GameDto
{
    public int Id { get; set; }
    
    public int DeveloperId { get; set; }    
    
    public string Title { get; set; } = null!;

    public ICollection<int> GenreIds { get; set; } = null!;
}