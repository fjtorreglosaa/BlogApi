using BlogApi.Domain.Models;
using BlogApi.Persistence.Context;
using BlogApi.Persistence.Repositories.Contracts;

namespace BlogApi.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
