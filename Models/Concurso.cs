﻿namespace DesafioLeds.Models
{
    public class Concurso
    {
        public int Id { get; set; }
        public string Orgao { get; set; }
        public string Edital { get; set; }
        public string Codigo { get; set; }
        public List<string> Vagas { get; set; } = [];

    }
}
