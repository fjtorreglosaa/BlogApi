namespace BlogApi.Domain.Models
{
    public class Comment : Model
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public string Content { get; set; }
        public bool Edited { get; set; }
        public DateTime? LastModificated { get; set; }
    }
}
