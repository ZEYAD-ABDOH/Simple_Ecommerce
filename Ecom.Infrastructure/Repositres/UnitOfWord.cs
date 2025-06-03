using AutoMapper;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositres
{
    public class UnitOfWord : IUnitOfWord
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementServies _imageManagementServies;
        public IProdutRepositry ProdutRepositry { get; }
        public ICategoryRepositry CategoryRepositry { get; }
        public IPhotoRepositry PhotoRepositry { get; }

        public UnitOfWord(AppDbContext context, IImageManagementServies imageManagementServies, IMapper mapper)
        {
            _context = context;
            _imageManagementServies = imageManagementServies;
            _mapper = mapper;
            ProdutRepositry = new ProdutRepositry(_context, _mapper, _imageManagementServies);
            CategoryRepositry = new CategoryRepositry(_context);
            PhotoRepositry = new PhotoRepositry(_context);

        }
    }
}

