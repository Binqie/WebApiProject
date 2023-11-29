using WebApiProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebApiProject.Models;
using WebApiProject.Services;

namespace WebApiProject.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserChildren> UserChildren { get; set; }
    public DbSet<UserParents> UserParents { get; set; }
    public DbSet<Token> Tokens { get; set; }
 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Role)
            .WithMany(e => e.Users);

        modelBuilder.Entity<User>()
            .HasMany(e => e.UserChildren)
            .WithMany(e => e.Users);

        modelBuilder.Entity<User>()
            .HasOne(e => e.UserParents)
            .WithMany(e => e.User);
        
    }
}