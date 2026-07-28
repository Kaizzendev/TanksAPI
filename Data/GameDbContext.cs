using Microsoft.EntityFrameworkCore;
using TanksAPI.Models;

namespace TanksAPI.Data;

public class GameDbContext: DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}