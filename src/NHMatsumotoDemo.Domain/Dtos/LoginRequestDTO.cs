namespace NHMatsumotoDemo.Domain.Dtos
{
    public record LoginRequestDTO 
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
