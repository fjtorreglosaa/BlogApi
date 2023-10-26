using BlogApi.Application.Utilities.DTOs.AuthDTOs;

namespace BlogApi.Application.Services.Auth.Contracts
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> BuildToken(UserCredsDTO credentials);
    }
}
