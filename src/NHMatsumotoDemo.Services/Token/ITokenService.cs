using NHMatsumotoDemo.Domain.Entities;
using System.Security.Claims;

namespace NHMatsumotoDemo.Services
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user);
        bool ValidateToken(string token);
        string GenerateToken(IEnumerable<Claim> claims);
        string GenereteRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        void SaveRefreshToken(string username, string refreshToken);
        string GetRefreshToken(string username);
        void DeleteRefreshToken(string username, string refreshToken);
    }
}
