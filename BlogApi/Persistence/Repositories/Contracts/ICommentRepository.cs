using BlogApi.Domain.Models;

namespace BlogApi.Persistence.Repositories.Contracts
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IReadOnlyList<Comment>> GetCommentsByPostId(Guid postId);
    }
}
