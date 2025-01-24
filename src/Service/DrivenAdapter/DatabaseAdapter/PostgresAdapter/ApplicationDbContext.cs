#nullable disable warnings

using Microsoft.EntityFrameworkCore;

using Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Entity;

namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter;


public class ApplicationDbContext : DbContext
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
   {
   }

   public DbSet<FoodItemEntity> FoodItems { get; set; }
}