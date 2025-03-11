using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesafioBackEnd.src.Domain;
using DesafioBackEnd.src.Services.ReadData.Interface;
using DesafioBackEnd.src.Services.ReadData.ReadData;

namespace DesafioBackEnd.src.Services.GenerateDB
{
    public static class GenerateDB
    {
        public static (List<Candidato>, List<Concurso>) GenerateData()
        {
            string result;
            List<Candidato> candidatos;
            List<Concurso> concursos;

            Console.WriteLine("Gerando banco de dados...");

            ReadCandidato readCandidato = new();
            (result, candidatos) = readCandidato.Process();
            Console.WriteLine(result);

            ReadConcurso readConcurso = new();
            (result, concursos) = readConcurso.Process();
            Console.WriteLine(result);

            return (candidatos, concursos);
        }
    }
}
