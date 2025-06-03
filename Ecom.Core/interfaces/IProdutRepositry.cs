using Ecom.Core.DTO;
using Ecom.Core.Entites.Products;

namespace Ecom.Core.interfaces
{
    public interface IProdutRepositry : IGenericRepositry<Product>
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync(string sort);
        Task<bool> AddAsync(AddProductDTO addProductDTO);
    }
}
