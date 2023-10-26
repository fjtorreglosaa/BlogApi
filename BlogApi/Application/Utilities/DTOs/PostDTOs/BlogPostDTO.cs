﻿using BlogApi.Application.Utilities.DTOs.CommentDTOs;

namespace BlogApi.Application.Utilities.DTOs.PostDTOs
{
    public class BlogPostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<PostCommentDTO> Comments { get; set; } = new List<PostCommentDTO>();
    }
}