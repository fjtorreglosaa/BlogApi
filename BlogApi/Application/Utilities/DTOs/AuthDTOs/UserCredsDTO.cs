using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Utilities.DTOs.AuthDTOs
{
    public class UserCredsDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
