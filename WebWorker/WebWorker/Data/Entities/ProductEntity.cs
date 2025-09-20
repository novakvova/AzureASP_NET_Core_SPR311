using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebWorker.Data.Entities;

[Table("tblProducts")]
public class ProductEntity : BaseEntity<long>
{
    [StringLength(250)]
    public string Name { get; set; } = String.Empty;

    [StringLength(250)]
    public string Slug { get; set; } = String.Empty;

    public decimal Price { get; set; }

    public int Weight { get; set; }

    [ForeignKey(nameof(Category))]
    public long CategoryId { get; set; }

    public CategoryEntity? Category { get; set; }
}
