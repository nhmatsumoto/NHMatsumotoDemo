using AutoMapper;
using NHMatsumotoDemo.Domain.Dtos;
using NHMatsumotoDemo.Services;
using NHMatsumotoDemo.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace NHMatsumotoDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        private IMapper _mapper;

        public AuthController(ITokenService tokenService, IAuthService authService, IMapper mapper)
        {
            _tokenService = tokenService;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestDTO dto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            //Consulta usuário na services
            var user = await _authService.GetUser(dto.Email, dto.Senha);

            if (user is not null)
            {
                // Gera o Token
                var token = _tokenService.GenerateToken(user);

                //Gera refresh token
                var refreshToken = _tokenService.GenereteRefreshToken();

                //Salva refresh token
                _tokenService.SaveRefreshToken(user.Email, refreshToken);

                // Oculta a senha
                user.Senha = "";

                return Ok(new LoginResponseDTO
                {
                    Usuario = new UsuarioDTO
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Nome,
                    },
                    AccessToken = token,
                    RefreshToken = refreshToken
                });
            }

            return BadRequest(new { message = "Usuário ou senha inválidos" });
        }

        [HttpPost]
        [Authorize]
        [Route("refresh")]
        public IActionResult Refresh(string token, string refreshToken)
        {
            //Validar se o token está expirado
            var isValidToken = _tokenService.ValidateToken(token);

            if (isValidToken)
            {
                var principal = _tokenService.GetPrincipalFromExpiredToken(token);
                var username = principal?.Identity?.Name;

                if (username is not null)
                {
                    var savedRefreshToken = _tokenService.GetRefreshToken(username);

                    if (savedRefreshToken != refreshToken)
                    {
                        throw new SecurityTokenException("Refresh Token inválido");
                    }

                    var newJwtToken = _tokenService.GenerateToken(principal.Claims);
                    var newRefreshToken = _tokenService.GenereteRefreshToken();
                    _tokenService.DeleteRefreshToken(username, refreshToken);
                    _tokenService.SaveRefreshToken(username, newRefreshToken);

                    return Ok(new
                    {
                        token = newJwtToken,
                        refreshToken = newRefreshToken
                    });
                }

                return BadRequest("Usuário não encontrado");
            }
            else
            {
                return BadRequest("Token inválido");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("validate")]
        public IActionResult Validate(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                return Ok(_tokenService.ValidateToken(token));
            }

            return BadRequest();
        }
    }
}
