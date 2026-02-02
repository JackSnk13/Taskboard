using Microsoft.EntityFrameworkCore;
using TaskBoardAPI.Models;
namespace TaskBoardAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
}
