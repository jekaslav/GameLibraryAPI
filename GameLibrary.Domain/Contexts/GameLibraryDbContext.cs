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
        });

        modelBuilder.Entity<GenreEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}