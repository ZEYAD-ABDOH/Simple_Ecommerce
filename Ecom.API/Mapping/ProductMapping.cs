﻿using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Products;

namespace Ecom.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>().
                ForMember(prod => prod.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<Photo, PhotoDTO>()
               .ReverseMap();
            CreateMap<AddProductDTO, Product>()
               .ForMember(x => x.Photos, op => op.Ignore())
              .ReverseMap();

        }
    }
}
