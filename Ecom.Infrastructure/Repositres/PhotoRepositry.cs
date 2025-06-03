using Ecom.Core.Entites.Products;
using Ecom.Core.interfaces;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositres
{
    public class PhotoRepositry : GenericRepositry<Photo>, IPhotoRepositry
    {
        public PhotoRepositry(AppDbContext context) : base(context)
        {
        }
    }
}
