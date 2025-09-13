using AutoMapper;
using WebWorker.Data.Entities;
using WebWorker.Models.Category;

namespace WebWorker.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CategoryCreateModel, CategoryEntity>()
            .ForMember(x=>x.Image, opt=>opt.Ignore());

        CreateMap<CategoryEntity, CategoryItemModel>()
            .ForMember(x=>x.ImagePath, 
                opt=>opt.MapFrom(x=>$"/images/400_{x.Image}"));
    }
}
