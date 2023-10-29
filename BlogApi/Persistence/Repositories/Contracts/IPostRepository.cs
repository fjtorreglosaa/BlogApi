using BlogApi.Domain.Models;

namespace BlogApi.Persistence.Repositories.Contracts
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IReadOnlyList<Post>> GetPostsByUserId(Guid blogId);
        Task<IReadOnlyList<Post>> GetPostsByBlogId(Guid userId);
    }
}
