using BlogApi.Application.Services.Domain.Contracts;
using BlogApi.Persistence.UnitOfWork.Contracts;

namespace SmallBlog.Application.Services.Domain
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
