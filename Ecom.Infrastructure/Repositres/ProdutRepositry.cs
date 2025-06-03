using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Products;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositres
{
    public class ProdutRepositry : GenericRepositry<Product>, IProdutRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementServies imageManagementServies;

        public ProdutRepositry(AppDbContext context, IMapper mapper, IImageManagementServies imageManagementServies) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementServies = imageManagementServies;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllAsync(string sort)
        {
            var query = context.Products
                .Include(x => x.Category)
                .Include(x => x.Photos)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(sort))
            {
                query = sort switch
                {
                    "PriceAsc" => query.OrderBy(q => q.NewPrice),
                    "PriceDec" => query.OrderByDescending(q => q.NewPrice),
                    _ => query.OrderBy(q => q.Name),
                };
            }
            var result = mapper.Map<List<ProductDTO>>(query);
            return result;
        }


        public async Task<bool> AddAsync(AddProductDTO addProductDTO)
        {
            if (addProductDTO == null) return false;

            var productMapping = mapper.Map<Product>(addProductDTO);
            await context.Products.AddAsync(productMapping);
            await context.SaveChangesAsync();
            var ImagePath = await imageManagementServies.AddImageAsync(addProductDTO.Photo, addProductDTO.Name);
            //   pathيوجد اكثر من   
            // رح سوي mapping
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = productMapping.Id,

            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;

        }
    }
}
