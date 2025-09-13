namespace WebWorker.Models.Category;

public class CategoryItemModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? ImagePath { get; set; }
}
