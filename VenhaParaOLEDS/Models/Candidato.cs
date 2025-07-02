// Models/Candidato.cs
using System;
using System.Collections.Generic;

namespace VenhaParaOLEDS.Models
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public List<string> Profissoes { get; set; }
    }
}