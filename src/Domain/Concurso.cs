using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace DesafioBackEnd.src.Domain
{
    public class Concurso
    {
        public int Id { get; private set; }
        public string Orgao { get; private set; }
        public string Edital { get; private set; }
        public string Codigo { get; private set; }
        public List<string> Vaga { get; private set; }

        public Concurso()
        {
            Orgao = string.Empty;
            Edital = string.Empty;
            Codigo = string.Empty;
            Vaga = [];
        }

        public Concurso(int id, string orgao, string edital, string codigo, List<string> vaga)
        {
            this.Id = id;
            this.Orgao = orgao;
            this.Edital = edital;
            this.Codigo = codigo;
            this.Vaga = vaga;
        }
    }
}
