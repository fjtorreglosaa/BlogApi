using BlogApi.Domain.Models;

namespace BlogApi.Application.Utilities.DTOs.CommentDTOs
{
    public class PostCommentDTO
    {
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public bool Edited { get; set; }
        public DateTime? LastModificated { get; set; }
    }
}
