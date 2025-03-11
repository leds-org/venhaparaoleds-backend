using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesafioBackEnd.src.Domain;
using DesafioBackEnd.src.Services.GenerateDB;
using DesafioBackEnd.src.Services.StartMenu;

namespace DesafioBackEnd.src.Application
{
    internal class Application
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando aplicação...");

            (List<Candidato> candidatos, List<Concurso> concursos) = GenerateDB.GenerateData();

            GenerateMenu.Generate(candidatos, concursos);

            Console.WriteLine("Aplicação finalizada.");
        }
    }
}
