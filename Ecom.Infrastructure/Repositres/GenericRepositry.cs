using Ecom.Core.interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecom.Infrastructure.Repositres
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepositry(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public async Task AddAsync(T entitiy)
        {
            await _context.AddAsync(entitiy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbset.FindAsync(id);
            _dbset.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
             => await _dbset.AsNoTracking().ToListAsync();


        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbset.AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {

            var entity = await _dbset.FindAsync(id);
            return entity;

        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbset;
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);

            return entity;
        }

        public async Task UpdateAsync(T entitiy)
        {
            _dbset.Entry(entitiy).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
