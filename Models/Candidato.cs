namespace DesafioLeds.Models
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string CPF { get; set; }
        public List<string> Profissoes { get; set; } = [];

    }
}
