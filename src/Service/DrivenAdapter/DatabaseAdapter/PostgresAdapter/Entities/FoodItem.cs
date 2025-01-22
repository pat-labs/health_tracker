using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable warnings
namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Entities;

[Table("food_item")]
public class FoodItemEntity
{
   public string FoodItemId { get; set; }
   public string Name { get; set; }
   public double CaloriesPer100g { get; set; }
   public double ProteinPer100g { get; set; }
   public double CarbsPer100g { get; set; }
   public double FatPer100g { get; set; }
}