// Models/Concurso.cs
using System;
using System.Collections.Generic;

namespace VenhaParaOLEDS.Models
{
    public class Concurso
    {
        public int Id { get; set; }
        public required string Orgao { get; set; }
        public required string Edital { get; set; }
        public required string Codigo { get; set; }

        // Relação 1:N com Vagas
        public List<Vaga> Vagas { get; set; } = new();
    }
}