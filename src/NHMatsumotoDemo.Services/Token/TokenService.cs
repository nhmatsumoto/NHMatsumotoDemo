using NHMatsumotoDemo.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NHMatsumotoDemo.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public List<(string, string)> _refreshTokens = new();

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_config.GetRequiredSection("JwtPrivateToken").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_config.GetRequiredSection("JwtPrivateToken").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_config.GetRequiredSection("JwtPrivateToken").Value);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GenereteRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetRequiredSection("JwtPrivateToken").Value)),
                ValidateLifetime = false
            };

            var tokenHandle = new JwtSecurityTokenHandler();
            var principal = tokenHandle.ValidateToken(token, tokenValidationParameter, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken 
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token Invalido");

            return principal;
        }

        public void SaveRefreshToken(string username, string refreshToken) 
            => _refreshTokens.Add(new(username, refreshToken));
        
        public string GetRefreshToken(string username)
            => _refreshTokens.FirstOrDefault(x => x.Item1 == username).Item2;

        public void DeleteRefreshToken(string username, string refreshToken)
        {
            var token = _refreshTokens.FirstOrDefault(x => x.Item1 == username && x.Item2 == refreshToken);
            _refreshTokens.Remove(token);
        } 
    }
}

