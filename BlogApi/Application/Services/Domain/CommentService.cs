using BlogApi.Application.Services.Domain.Contracts;
using BlogApi.Persistence.UnitOfWork.Contracts;

namespace SmallBlog.Application.Services.Domain
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
