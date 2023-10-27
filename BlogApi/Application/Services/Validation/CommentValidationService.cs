using BlogApi.Application.Services.Validation.Contracts;
using BlogApi.Application.Utilities.DTOs.CommentDTOs;
using BlogApi.Application.Utilities.DTOs.PostDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;
using BlogApi.Persistence.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogApi.Application.Services.Validation
{
    public class CommentValidationService : ICommentValidationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(ValidationResultDTO Validation, Comment Comment)> ValidateForDelete(Guid id)
        {
            var validationResult = new ValidationResultDTO();

            var comment = await _unitOfWork.Comments.GetByIdAsync(id);

            if (validationResult == null)
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"The comment with the id {id} does not exists.",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            return (validationResult, comment);
        }

        public async Task<(ValidationResultDTO Validation, Comment Post)> ValidateForUpdate(Guid id, UpdateCommentDTO criteria)
        {
            var validationResult = new ValidationResultDTO();

            var comment = await _unitOfWork.Comments.GetByIdAsync(id);

            if (validationResult == null)
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = $"The comment with the id {id} does not exists.",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            var commentContent = await _unitOfWork.Comments.GetAll().Where(x => x.Content == criteria.Content).ToListAsync();


            if (string.IsNullOrEmpty(criteria.Content))
            {
                validationResult.Conditions.Add(new ValidationConditionDTO
                {
                    ErrorMessage = "The content field is required",
                    Severity = (int)HttpStatusCode.BadRequest
                });
            }

            return (validationResult, comment);
        }

        public async Task<ValidationResultDTO> ValidateForCreate(CreateCommentDTO criteria)
        {
            var validationResult = new ValidationResultDTO();

            var postTitle = await _unitOfWork.Comments.GetAll().Where(x => x.Content == criteria.Content).ToListAsync();

            if (criteria.Content == null)
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
