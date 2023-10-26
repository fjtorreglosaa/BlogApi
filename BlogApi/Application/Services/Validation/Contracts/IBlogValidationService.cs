using BlogApi.Application.Utilities.DTOs.BlogDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;

namespace BlogApi.Application.Services.Validation.Contracts
{
    public interface IBlogValidationService
    {
        Task<(ValidationResultDTO Validation, Blog Blog)> ValidateForDelete(Guid id);
        Task<(ValidationResultDTO Validation, Blog Blog)> ValidateForUpdate(Guid id, UpdateBlogDTO criteria);
        Task<ValidationResultDTO> ValidateForCreate(CreateBlogDTO criteria);
    }
}
