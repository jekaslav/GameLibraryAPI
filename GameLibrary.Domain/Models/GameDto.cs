using GameLibrary.Domain.Entities;

namespace GameLibrary.Domain.Models;

public class GameDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public int DeveloperId { get; set; }

    public ICollection<int> Genres { get; set; }
}