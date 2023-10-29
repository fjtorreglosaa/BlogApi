using BlogApi.Application.Utilities.DTOs.CommentDTOs;

namespace BlogApi.Application.Utilities.DTOs.PostDTOs
{
    public class PostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
