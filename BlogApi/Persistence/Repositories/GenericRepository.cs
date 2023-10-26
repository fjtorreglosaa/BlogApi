using Microsoft.EntityFrameworkCore;
using BlogApi.Domain.Models;
using BlogApi.Persistence.Context;
using BlogApi.Persistence.Repositories.Contracts;

namespace BlogApi.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Model
    {
        private readonly ApplicationDBContext _context;

        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
