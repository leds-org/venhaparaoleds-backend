using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace DesafioBackEnd.src.Domain
{
    public class Concurso
    {
        public int id { get; private set; }
        public string orgao { get; private set; }
        public string edital { get; private set; }
        public string codigo { get; private set; }
        public List<string> vaga { get; private set; }

        public Concurso()
        {
            orgao = string.Empty;
            edital = string.Empty;
            codigo = string.Empty;
            vaga = new List<string>();
        }

        public Concurso(int id, string orgao, string edital, string codigo, List<string> vaga)
        {
            this.id = id;
            this.orgao = orgao;
            this.edital = edital;
            this.codigo = codigo;
            this.vaga = vaga;
        }
    }
}
