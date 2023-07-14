using GameLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Domain.Contexts;

public sealed class GameLibraryDbContext : DbContext
{
    public GameLibraryDbContext(DbContextOptions<GameLibraryDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DeveloperEntity> Developers { get; set; } = null!;
    
    public DbSet<GameEntity> Games { get; set; } = null!;

    public DbSet<GenreEntity> Genres { get; set; } = null!;

    public DbSet<GameGenreEntity> GameGenres { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeveloperEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<GameEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Developer)
                .WithMany()
                .HasForeignKey(e => e.DeveloperId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.GameGenres)
                .WithOne(e => e.Game)
                .HasForeignKey(e => e.GameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GenreEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<GameGenreEntity>(entity =>
        {
            entity.HasKey(e => new { e.GameId, e.GenreId });
            entity.HasOne(e => e.Game)
                .WithMany(e => e.GameGenres)
                .HasForeignKey(e => e.GameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Genre)
                .WithMany(e => e.GameGenres)
                .HasForeignKey(e => e.GenreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
