using AutoMapper;
using WebWorker.Data.Entities;
using WebWorker.Models.Seeder;

namespace WebWorker.Mappers;

public class IngredientMapper : Profile
{
    public IngredientMapper()
    {
        CreateMap<SeederIngredientModel, IngredientEntity>();
    }
}
