using Microsoft.EntityFrameworkCore;
using TanksAPI.Models;

namespace TanksAPI.Data;

public class GameDbContext: DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<SaveGame> SaveGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.SaveGame)
            .WithOne(s => s.User)
            .HasForeignKey<SaveGame>(s => s.UserId);
    }
}