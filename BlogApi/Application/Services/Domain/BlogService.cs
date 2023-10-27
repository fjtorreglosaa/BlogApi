using AutoMapper;
using BlogApi.Application.Services.Domain.Contracts;
using BlogApi.Application.Services.Validation.Contracts;
using BlogApi.Application.Utilities.DTOs.BlogDTOs;
using BlogApi.Application.Utilities.DTOs.ValidationDTOs;
using BlogApi.Domain.Models;
using BlogApi.Persistence.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;

namespace SmallBlog.Application.Services.Domain
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlogValidationService _blogValidationService;
        private readonly IMapper _mapper;
        private readonly ILogger<BlogService> _logger;

        public BlogService(IUnitOfWork unitOfWork, IBlogValidationService blogValidationService, IMapper mapper, ILogger<BlogService> logger)
        {
            _unitOfWork = unitOfWork;
            _blogValidationService = blogValidationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(ValidationResultDTO Validation, bool Commited)> CreateBlog(CreateBlogDTO criteria)
        {
            var validation = await _blogValidationService.ValidateForCreate(criteria);

            var entity = new Blog
            {
                Id = new Guid(),
                AuthorId = criteria.AuthorId,
                Title = criteria.BlogName,
                Description = criteria.Description
            };

            if (validation.Conditions.Any())
            {
                foreach (var condition in validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validation, false);
            }

            await _unitOfWork.Blogs.AddAsync(entity);

            _unitOfWork.Commit();

            return (validation, true);
        }

        public async Task<(ValidationResultDTO Validation, bool Commited)> RemoveBlog(Guid blogId)
        {
            var validationResult = await _blogValidationService.ValidateForDelete(blogId);

            if (validationResult.Validation.Conditions.Any())
            {
                foreach (var condition in validationResult.Validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validationResult.Validation, false);
            }

            _unitOfWork.Blogs.Delete(validationResult.Blog);

            _unitOfWork.Commit();   

            return (validationResult.Validation, true);
        }

        public async Task<IReadOnlyList<BlogDTO>> GetAllBlogsAsync()
        {
            var data = await _unitOfWork.Blogs.GetAllAsync();

            var entity = _mapper.Map<List<BlogDTO>>(data);

            return entity;
        }

        public async Task<BlogDTO> GetBlogByIdAsync(Guid blogId)
        {
            var entity = await _unitOfWork.Blogs.GetAll().Where(x => x.Id == blogId).FirstOrDefaultAsync();

            var result = _mapper.Map<BlogDTO>(entity);

            return result;
        }

        public async Task<(ValidationResultDTO Validation, bool Commited)> UpdateBlog(Guid blogId, UpdateBlogDTO criteria)
        {
            var validationResult = await _blogValidationService.ValidateForUpdate(blogId, criteria);

            if (validationResult.Validation.Conditions.Any())
            {
                foreach (var condition in validationResult.Validation.Conditions)
                {
                    _logger.LogError($"Error: {condition.ErrorMessage}");
                }

                return (validationResult.Validation, false);
            }

            var entity = validationResult.Blog;

            entity.Title = criteria.Title;
            entity.Description = criteria.Description;

            _unitOfWork.Commit();

            return (validationResult.Validation, true);
        }
    }
}
