using Microsoft.EntityFrameworkCore;
using Service.DrivenAdapter.DatabaseAdapter.Entities;

#nullable disable warnings
namespace Service.DrivenAdapter.DatabaseAdapter;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<FoodItemEntity> FoodItems { get; set; }
}