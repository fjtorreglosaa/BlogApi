namespace BlogApi.Application.Utilities.DTOs.AuthDTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
