using BlogApi.Persistence.Repositories.Contracts;

namespace BlogApi.Persistence.UnitOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IBlogRepository Blogs { get; }
        ICommentRepository Comments { get; }
        int Commit();
    }
}
