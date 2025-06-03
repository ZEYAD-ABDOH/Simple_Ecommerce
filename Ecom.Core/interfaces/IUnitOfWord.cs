namespace Ecom.Core.interfaces
{
    public interface IUnitOfWord
    {
        IProdutRepositry ProdutRepositry { get; }
        ICategoryRepositry CategoryRepositry { get; }

        IPhotoRepositry PhotoRepositry { get; }
    }
}
