using Microsoft.AspNetCore.Identity;

namespace BlogApi.Domain.Models
{
    public class Blog : Model
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Post> Posts { get; set; }
        public IdentityUser Author { get; set; }
    }
}
