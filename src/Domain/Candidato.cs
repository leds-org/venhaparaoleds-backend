using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioBackEnd.src.Domain
{
    public class Candidato
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string DtNascimento { get; private set; }
        public string Cpf { get; private set; }
        public List<string> Profissao { get; private set; }

        public Candidato()
        {   
            Nome = string.Empty;
            DtNascimento = string.Empty;
            Cpf = string.Empty;
            Profissao = [];
        }

        public Candidato(int id, string nome, string dtNascimento, string cpf, List<string> profissao)
        {
            this.Id = id;
            this.Nome = nome;
            this.DtNascimento = dtNascimento;
            this.Cpf = cpf;
            this.Profissao = profissao;
        }
    }
}
