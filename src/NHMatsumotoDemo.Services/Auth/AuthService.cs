using NHMatsumotoDemo.Domain.Entities;
using NHMatsumotoDemo.Domain.Interfaces;

namespace NHMatsumotoDemo.Services.Auth
{
    public class AuthService : IAuthService
    {
        protected readonly IUsuarioRepository _repository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _repository = usuarioRepository;
        }

        public async Task<Usuario> GetUser(string email, string password)
        {
            try
            {
                var result = await _repository.GetByExpression(e => e.Email == email && e.Senha == password);
                var user = result.FirstOrDefault();

                if (user is not null)
                {
                    return user;
                }
            }catch(Exception ex)
            {
                throw new Exception("ERRO GETUSER");
            }

            return null;
        }
    }
}
