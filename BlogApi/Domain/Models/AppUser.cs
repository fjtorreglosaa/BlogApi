using Microsoft.AspNetCore.Identity;

namespace BlogApi.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public Guid UserId { get; set; }
        public bool IsAuthor { get; set; }
    }
}
