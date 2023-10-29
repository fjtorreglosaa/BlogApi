using BlogApi.Application.Utilities.DTOs.CommentDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;

namespace BlogApi.Application.Services.Domain.Contracts
{
    public interface ICommentService
    {
        Task<(ValidationResultDTO Validation, bool Commited)> CreateComment(CreateCommentDTO criteria);
        Task<(ValidationResultDTO Validation, bool Commited)> RemoveComment(Guid commentId);
        Task<IReadOnlyList<CommentDTO>> GetAllCommentsAsync();
        Task<CommentDTO> GetCommentsByIdAsync(Guid commentId);
        Task<(ValidationResultDTO Validation, bool Commited)> UpdateComment(Guid commentId, UpdateCommentDTO criteria);
        Task<IReadOnlyList<CommentDTO>> GetCommentsByPostId(Guid postId);
    }
}
