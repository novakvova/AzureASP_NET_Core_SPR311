using System.ComponentModel.DataAnnotations;

namespace WebWorker.Data.Entities;

public class CategoryEntity : BaseEntity<long>
{
    [Required, StringLength(250)]
    public string Name { get; set; } = String.Empty;

    [Required, StringLength(250)]
    public string Slug { get; set; } = String.Empty;

    [StringLength(250)]
    public string? Image { get; set; } = null;

    public ICollection<ProductEntity>? Products { get; set; }
}
