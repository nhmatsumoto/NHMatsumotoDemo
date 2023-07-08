using NHMatsumotoDemo.Domain.Entities;

namespace NHMatsumotoDemo.Services.Auth
{
    public interface IAuthService
    {
        Task<Usuario> GetUser(string email, string password);
    }
}