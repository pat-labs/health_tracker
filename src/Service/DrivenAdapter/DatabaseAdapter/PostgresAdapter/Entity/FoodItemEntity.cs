#nullable disable warnings

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.DrivenAdapter.DatabaseAdapter.PostgresAdapter.Entity;

[Table("food_item")]
public class FoodItemEntity
{
    [Key]
    public string food_item_id { get; set; }

    public string name { get; set; }
    public double calories_per_100g { get; set; }
    public double protein_per_100g { get; set; }
    public double carbs_per_100g { get; set; }
    public double fat_per_100g { get; set; }
}