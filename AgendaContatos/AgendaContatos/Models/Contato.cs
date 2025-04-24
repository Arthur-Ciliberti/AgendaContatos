namespace AgendaContatos.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataUltimaAlteracao { get; set; } = DateTime.Now;
    }
}
