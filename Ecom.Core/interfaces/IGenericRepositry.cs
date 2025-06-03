using System.Linq.Expressions;

namespace Ecom.Core.interfaces
{
    public interface IGenericRepositry<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entitiy);
        Task UpdateAsync(T entitiy);
        Task DeleteAsync(int id);


    }
}
