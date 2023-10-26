using BlogApi.Persistence.Context;
using BlogApi.Persistence.Repositories;
using BlogApi.Persistence.Repositories.Contracts;
using BlogApi.Persistence.UnitOfWork.Contracts;

namespace BlogApi.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public IBlogRepository Blogs { get; private set; }
        public IPostRepository Posts { get; private set; }
        public ICommentRepository Comments { get; private set; }

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            Blogs = new BlogRepository(_context);
            Posts = new PostRepository(_context);
            Comments = new CommentRepository(_context);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
