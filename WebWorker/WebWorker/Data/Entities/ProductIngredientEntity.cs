using System.ComponentModel.DataAnnotations.Schema;

namespace WebWorker.Data.Entities;

[Table("tblProductIngredients")]
public class ProductIngredientEntity
{
    [ForeignKey(nameof(Product))]
    public long ProductId { get; set; }
    [ForeignKey(nameof(Ingredient))]
    public long IngredientId { get; set; }

    public virtual ProductEntity? Product { get; set; }
    public virtual IngredientEntity? Ingredient { get; set; }
}