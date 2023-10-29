using BlogApi.Application.Utilities.DTOs.PostDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;

namespace BlogApi.Application.Services.Domain.Contracts
{
    public interface IPostService
    {
        Task<(ValidationResultDTO Validation, bool Commited)> CreateComment(CreatePostDTO criteria);
        Task<(ValidationResultDTO Validation, bool Commited)> RemovePost(Guid commentId);
        Task<IReadOnlyList<PostDTO>> GetAllPostsAsync();
        Task<IReadOnlyList<PostDTO>> GetPostsByUserId(Guid userId);
        Task<IReadOnlyList<PostDTO>> GetPostsByBlogId(Guid blogId);
    }
}
