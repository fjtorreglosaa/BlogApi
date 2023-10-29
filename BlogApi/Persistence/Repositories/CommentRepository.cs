using BlogApi.Domain.Models;
using BlogApi.Persistence.Context;
using BlogApi.Persistence.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        
        public CommentRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsByPostId(Guid postId)
        {
            var comments = await _context.Comments.Where(x => x.PostId == postId).ToListAsync();

            return comments;
        }
    }
}
