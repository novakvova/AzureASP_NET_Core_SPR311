using AutoMapper;
using WebWorker.Data;
using WebWorker.Data.Entities;
using WebWorker.Interfaces;
using WebWorker.Models.Category;

namespace WebWorker.Services;

public class CategoryService(IMapper mapper, 
    AppWorkerDbContext context,
    ImageService imageService) : ICategoryService
{
    public async Task<long> CreateAsync(CategoryCreateModel model)
    {
        var entity = mapper.Map<CategoryEntity>(model);
        if (model.Image != null)
        {
            entity.Image = await imageService.SaveAsync(model.Image);
        }
        context.Categories.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }
}
