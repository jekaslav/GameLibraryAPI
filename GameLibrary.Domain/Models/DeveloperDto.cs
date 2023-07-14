namespace GameLibrary.Domain.Models;

public class DeveloperDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}