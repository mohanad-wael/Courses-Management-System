using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task.Data;

namespace Task.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class 
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _entities.ToListAsync();
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }       
        public void Update(T obj)
        {
            _entities.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

     

        public async void DeleteByIdAsync(int id)
        {
            try
            {
                var result = await _entities.FindAsync(id);
                _entities.Remove(result);
            }
            catch (Exception ex)
            {

                throw;
            }



        }
    }
}
