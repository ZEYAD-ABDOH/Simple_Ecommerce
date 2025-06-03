using Ecom.Core.Entites.Products;
using Ecom.Core.interfaces;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositres
{
    public class CategoryRepositry : GenericRepositry<Category>, ICategoryRepositry
    {
        public CategoryRepositry(AppDbContext context) : base(context)
        {
        }
    }
}
