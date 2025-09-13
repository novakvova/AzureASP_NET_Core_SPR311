using WebWorker.Models.Category;

namespace WebWorker.Interfaces;

public interface ICategoryService
{
    Task<long> CreateAsync(CategoryCreateModel model);
}
