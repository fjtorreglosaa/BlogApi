using AutoMapper;
using BlogApi.Application.Services.Domain.Contracts;
using BlogApi.Application.Services.Validation;
using BlogApi.Application.Services.Validation.Contracts;
using BlogApi.Application.Utilities.DTOs.CommentDTOs;
using BlogApi.Application.Utilities.DTOs.PostDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;
using BlogApi.Persistence.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SmallBlog.Application.Services.Domain
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostValidationService _postValidationService;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IUnitOfWork unitOfWork, IPostValidationService postValidationService, IMapper mapper, ILogger<PostService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _postValidationService = postValidationService;
            _mapper = mapper;
        }

        public async Task<(ValidationResultDTO Validation, bool Commited)> CreateComment(CreatePostDTO criteria)
        {
            var validation = await _postValidationService.ValidateForCreate(criteria);

            var entity = new Post
            {
                Id = new Guid(),
                AuthorId = criteria.AuthorId,
                BlogId = criteria.BlogId,
                Title = criteria.Title,
                Content = criteria.Content
            };

            if (validation.Conditions.Any())
            {
                foreach (var condition in validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validation, false);
            }

            await _unitOfWork.Posts.AddAsync(entity);

            _unitOfWork.Commit();

            return (validation, true);
        }

        public async Task<(ValidationResultDTO Validation, bool Commited)> RemovePost(Guid commentId)
        {
            var validationResult = await _postValidationService.ValidateForDelete(commentId);

            if (validationResult.Validation.Conditions.Any())
            {
                foreach (var condition in validationResult.Validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validationResult.Validation, false);
            }

            _unitOfWork.Posts.Delete(validationResult.Post);

            _unitOfWork.Commit();

            return (validationResult.Validation, true);
        }

        public async Task<IReadOnlyList<PostDTO>> GetAllPostsAsync()
        {
            var data = await _unitOfWork.Posts.GetAllAsync();

            var entity = _mapper.Map<List<PostDTO>>(data);

            return entity;
        }

        public async Task<PostDTO> GetPostByIdAsync(Guid commentId)
        {
            var entity = await _unitOfWork.Posts.GetAll().Where(x => x.Id == commentId).FirstOrDefaultAsync();

            var result = _mapper.Map<PostDTO>(entity);

            return result;
        }

        public async Task<IReadOnlyList<PostDTO>> GetPostsByUserId(Guid userId)
        {
            var entities = await _unitOfWork.Posts.GetPostsByUserId(userId);

            var result = _mapper.Map<List<PostDTO>>(entities);

            return result;
        }

        public async Task<IReadOnlyList<PostDTO>> GetPostsByBlogId(Guid blogId)
        {

            var entities = await _unitOfWork.Posts.GetPostsByBlogId(blogId);

            var result = _mapper.Map<List<PostDTO>>(entities);

            return result;
        }

        public async Task<(ValidationResultDTO Validation, bool Commited)> UpdatePost(Guid commentId, UpdatePostDTO criteria)
        {
            var validationResult = await _postValidationService.ValidateForUpdate(commentId, criteria);

            if (validationResult.Validation.Conditions.Any())
            {
                foreach (var condition in validationResult.Validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validationResult.Validation, false);
            }

            var entity = validationResult.Post;

            entity.Content = criteria.Content;
            entity.Title = criteria.Title;

            _unitOfWork.Commit();

            return (validationResult.Validation, true);
        }
    }
}
