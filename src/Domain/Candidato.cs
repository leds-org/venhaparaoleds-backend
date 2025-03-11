using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioBackEnd.src.Domain
{
    public class Candidato
    {
        public int id { get; private set; }
        public string nome { get; private set; }
        public string dtNascimento { get; private set; }
        public string cpf { get; private set; }
        public List<string> profissao { get; private set; }

        public Candidato()
        {   
            nome = string.Empty;
            dtNascimento = string.Empty;
            cpf = string.Empty;
            profissao = new List<string>();
        }

        public Candidato(int id, string nome, string dtNascimento, string cpf, List<string> profissao)
        {
            this.id = id;
            this.nome = nome;
            this.dtNascimento = dtNascimento;
            this.cpf = cpf;
            this.profissao = profissao;
        }
    }
}
