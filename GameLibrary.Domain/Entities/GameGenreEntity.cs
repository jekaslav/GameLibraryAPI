namespace GameLibrary.Domain.Entities;

public class GameGenreEntity
{
    public int GameId { get; set; }
    
    public int GenreId { get; set; }    
    
    public GameEntity Game { get; set; }

    public GenreEntity Genre { get; set; }
}