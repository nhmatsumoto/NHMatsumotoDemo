namespace NHMatsumotoDemo.Domain.Entities
{
    public class Endereco : EntityBase
    {
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Logradouro { get; set; }
        public string Cidade { get; set; }
        public EstadosEnum Esatado { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public bool EnderecoAtual { get; set; }

        //EF Core Relations
  
        public Usuario Usuario { get; set; }
    }
}
