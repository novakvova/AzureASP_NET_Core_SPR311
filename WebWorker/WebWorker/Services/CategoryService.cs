using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebWorker.Data;
using WebWorker.Data.Entities;
using WebWorker.Interfaces;
using WebWorker.Models.Category;

namespace WebWorker.Services;

public class CategoryService(IMapper mapper, 
    AppWorkerDbContext context,
    IImageService imageService) : ICategoryService
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

    public async Task DeleteAsync(long id)
    {
        var entity = await context.Categories.FindAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<CategoryItemModel>> GetAllAsync()
    {
        var items = await context.Categories
            .Where(c => !c.IsDeleted)
            .ProjectTo<CategoryItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();
        //AsQurable
        return items;
    }
}
