using BlogApi.Application.Utilities.DTOs.BlogDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;

namespace BlogApi.Application.Services.Domain.Contracts
{
    public interface IBlogService
    {
        Task<IReadOnlyList<BlogDTO>> GetAllBlogsAsync();
        Task<BlogDTO> GetBlogByIdAsync(Guid blogId);
        Task<(ValidationResultDTO Validation, bool Commited)> RemoveBlog(Guid blogId);
        Task<(ValidationResultDTO Validation, bool Commited)> CreateBlog(CreateBlogDTO criteria);
        Task<(ValidationResultDTO Validation, bool Commited)> UpdateBlog(Guid blogId, UpdateBlogDTO criteria);
    }
}
