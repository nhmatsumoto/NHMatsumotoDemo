namespace NHMatsumotoDemo.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }

        //Regra para desenvolvimento: Quando o nome da variável se trata de uma sigla, colocar tudo em maiúsculo.
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
    }
}
