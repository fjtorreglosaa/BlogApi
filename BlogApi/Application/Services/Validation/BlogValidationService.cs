using BlogApi.Application.Services.Validation.Contracts;
using BlogApi.Application.Utilities.DTOs.BlogDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;
using BlogApi.Persistence.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogApi.Application.Services.Validation
{
    public class BlogValidationService : IBlogValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(ValidationResultDTO Validation, Blog Blog)> ValidateForDelete(Guid id)
        {
            var validationResult = new ValidationResultDTO();

            var blog = await _unitOfWork.Blogs.GetByIdAsync(id);

            if (validationResult == null)
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"The blog with the id {id} does not exists.",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            return (validationResult, blog);
        }

        public async Task<(ValidationResultDTO Validation, Blog Blog)> ValidateForUpdate(Guid id, UpdateBlogDTO criteria)
        {
            var validationResult = new ValidationResultDTO();

            var blog = await _unitOfWork.Blogs.GetByIdAsync(id);

            if (validationResult == null)
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"The blog with the id {id} does not exists.",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            var blogName = await _unitOfWork.Blogs.GetAll().Where(x => x.Title == criteria.Title).ToListAsync();

            if (blogName.Any())
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"There is a blog with the provided title {criteria.Title}",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(criteria.Title))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The blogname field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(criteria.Description))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The description field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            return (validationResult, blog);
        }

        public async Task<ValidationResultDTO> ValidateForCreate(CreateBlogDTO criteria)
        {
            var validationResult = new ValidationResultDTO();

            var blogName = await _unitOfWork.Blogs.GetAll().Where(x => x.Title == criteria.BlogName).ToListAsync();

            if (blogName.Any())
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"There is a blog with the provided title {criteria.BlogName}",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }
            if (criteria.AuthorId == null)
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The authorid field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(criteria.BlogName))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The blogname field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(criteria.Description))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The description field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            return validationResult;
        }
    }
}
