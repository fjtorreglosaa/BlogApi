using BlogApi.Domain.Models;

namespace BlogApi.Persistence.Repositories.Contracts
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<IReadOnlyList<Blog>> GetBlogsByUserId(string userId);
    }
}
