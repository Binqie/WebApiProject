using WebApiProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebApiProject.Models;
using WebApiProject.Services;

namespace WebApiProject.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<UserChildren> UserChildren { get; set; }
    public DbSet<UserParents> UserParents { get; set; }
 
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
}