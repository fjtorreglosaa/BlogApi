using BlogApi.Domain.Models;
using BlogApi.Persistence.Context;
using BlogApi.Persistence.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Persistence.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly ApplicationDBContext _context;

        public BlogRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Blog>> GetBlogsByUserId(Guid userId)
        {
            var blogs = await _context.Blogs.Where(x => x.AuthorId == userId).ToListAsync();

            return blogs;
        }
    }
}
