using BlogApi.Domain.Models;

namespace BlogApi.Application.Utilities.DTOs.CommentDTOs
{
    public class UpdateCommentDTO
    {
        public string Content { get; set; }
        public bool Edited { get; set; }
        public DateTime? LastModificated { get; set; }
    }
}
