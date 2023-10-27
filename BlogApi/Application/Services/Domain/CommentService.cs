using AutoMapper;
using BlogApi.Application.Services.Domain.Contracts;
using BlogApi.Application.Services.Validation.Contracts;
using BlogApi.Application.Utilities.DTOs.CommentDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;
using BlogApi.Persistence.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;

namespace SmallBlog.Application.Services.Domain
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentValidationService _commentValidationService;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentService> _logger;

        public CommentService(IUnitOfWork unitOfWork, ICommentValidationService commentValidationService, IMapper mapper, ILogger<CommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _commentValidationService = commentValidationService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<(ValidationResultDTO Validation, bool Commited)> CreateComment(CreateCommentDTO criteria)
        {
            var validation = await _commentValidationService.ValidateForCreate(criteria);

            var entity = new Comment
            {
                Id = new Guid(),
                UserId = criteria.UserId,
                PostId = criteria.PostId,
                Content = criteria.Content,
                Edited = false
            };

            if (validation.Conditions.Any())
            {
                foreach (var condition in validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validation, false);
            }

            await _unitOfWork.Comments.AddAsync(entity);

            _unitOfWork.Commit();

            return (validation, true);
        }

        public async Task<(ValidationResultDTO Validation, bool Commited)> RemoveComment(Guid commentId)
        {
            var validationResult = await _commentValidationService.ValidateForDelete(commentId);

            if (validationResult.Validation.Conditions.Any())
            {
                foreach (var condition in validationResult.Validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validationResult.Validation, false);
            }

            _unitOfWork.Comments.Delete(validationResult.Comment);

            _unitOfWork.Commit();

            return (validationResult.Validation, true);
        }

        public async Task<IReadOnlyList<CommentDTO>> GetAllBlogsAsync()
        {
            var data = await _unitOfWork.Comments.GetAllAsync();

            var entity = _mapper.Map<List<CommentDTO>>(data);

            return entity;
        }

        public async Task<CommentDTO> GetBlogByIdAsync(Guid commentId)
        {
            var entity = await _unitOfWork.Comments.GetAll().Where(x => x.Id == commentId).FirstOrDefaultAsync();

            var result = _mapper.Map<CommentDTO>(entity);

            return result;
        }

        public async Task<(ValidationResultDTO Validation, bool Commited)> UpdateBlog(Guid commentId, UpdateCommentDTO criteria)
        {
            var validationResult = await _commentValidationService.ValidateForUpdate(commentId, criteria);

            if (validationResult.Validation.Conditions.Any())
            {
                foreach (var condition in validationResult.Validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validationResult.Validation, false);
            }

            var entity = validationResult.Comment;

            entity.Content = criteria.Content;
            entity.Edited = true;
            entity.LastModificated = DateTime.Now;

            _unitOfWork.Commit();

            return (validationResult.Validation, true);
        }
    }
}
