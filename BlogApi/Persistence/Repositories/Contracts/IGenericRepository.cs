using BlogApi.Domain.Models;

namespace BlogApi.Persistence.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : Model
    {
        IQueryable<T> GetAll();
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity); 
        void Delete(T entity);
    }
}
