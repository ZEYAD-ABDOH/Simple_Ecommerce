using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Products;

namespace Ecom.API.Mapping
{
    public class CategroyMapping : Profile
    {
        public CategroyMapping()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
