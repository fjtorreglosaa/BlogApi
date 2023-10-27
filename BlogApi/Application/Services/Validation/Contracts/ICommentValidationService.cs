using BlogApi.Application.Utilities.DTOs.CommentDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;

namespace BlogApi.Application.Services.Validation.Contracts
{
    public interface ICommentValidationService
    {
        Task<(ValidationResultDTO Validation, Comment Comment)> ValidateForDelete(Guid id);
        Task<(ValidationResultDTO Validation, Comment Comment)> ValidateForUpdate(Guid id, UpdateCommentDTO criteria);
        Task<ValidationResultDTO> ValidateForCreate(CreateCommentDTO criteria);
    }
}
