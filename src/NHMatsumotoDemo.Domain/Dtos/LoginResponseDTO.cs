namespace NHMatsumotoDemo.Domain.Dtos
{
    public class LoginResponseDTO : EntityBaseDto
    {
        public UsuarioDTO Usuario { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
