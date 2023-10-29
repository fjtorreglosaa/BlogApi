using BlogApi.Application.Services.Validation.Contracts;
using BlogApi.Application.Utilities.DTOs.PostDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;
using BlogApi.Persistence.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogApi.Application.Services.Validation
{
    public class PostValidationService : IPostValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(ValidationResultDTO Validation, Post Post)> ValidateForDelete(Guid id)
        {
            var validationResult = new ValidationResultDTO();

            var blog = await _unitOfWork.Posts.GetByIdAsync(id);

            if (validationResult == null)
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"The post with the id {id} does not exists.",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            return (validationResult, blog);
        }

        public async Task<(ValidationResultDTO Validation, Post Post)> ValidateForUpdate(Guid id, UpdatePostDTO criteria)
        {
            var validationResult = new ValidationResultDTO();

            var post = await _unitOfWork.Posts.GetByIdAsync(id);

            if (validationResult == null)
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"The post with the id {id} does not exists.",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            var postTitle = await _unitOfWork.Posts.GetAll().Where(x => x.Title == criteria.Title).ToListAsync();

            if (postTitle.Any())
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"There is a post with the provided title {criteria.Title}",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(criteria.Title))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The title field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(criteria.Content))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The content field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            return (validationResult, post);
        }

        public async Task<ValidationResultDTO> ValidateForCreate(CreatePostDTO criteria)
        {
            var validationResult = new ValidationResultDTO();

            var postTitle = await _unitOfWork.Posts.GetAll().Where(x => x.Title == criteria.Title).ToListAsync();

            if (postTitle.Any())
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"There is a post with the provided title {criteria.Title}",
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
            if (string.IsNullOrEmpty(criteria.Title))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The title field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }
            if (string.IsNullOrEmpty(criteria.Content))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The content field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            return validationResult;
        }
    }
}
