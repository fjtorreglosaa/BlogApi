using BlogApi.Application.Utilities.DTOs.PostDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;

namespace BlogApi.Application.Services.Validation.Contracts
{
    public interface IPostValidationService
    {
        Task<(ValidationResultDTO Validation, Post Post)> ValidateForDelete(Guid id);
        Task<(ValidationResultDTO Validation, Post Post)> ValidateForUpdate(Guid id, UpdatePostDTO criteria);
        Task<ValidationResultDTO> ValidateForCreate(CreatePostDTO criteria);
    }
}
