using backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class TaskManagerDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<CaseworkerTask> CaseworkerTasks { get; set; }
    private readonly IConfiguration _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["ConnectionStrings:TaskManagerDb"]);
    }
}