using BlogApi.Domain.Models;
using BlogApi.Persistence.Context;
using BlogApi.Persistence.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationDBContext _context;
        public PostRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Post>> GetPostsByBlogId(Guid blogId)
        {
            var posts = await _context.Posts.Where(x => x.BlogId == blogId).ToListAsync();

            return posts;
        }

        public async Task<IReadOnlyList<Post>> GetPostsByUserId(Guid userId)
        {
            var posts = await _context.Posts.Where(x => x.AuthorId == userId).ToListAsync();

            return posts;
        }
    }
}
