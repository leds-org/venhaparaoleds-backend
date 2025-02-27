using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using venhaparaoleds_backend.src.Services.ReadData.Interface;
using venhaparaoleds_backend.src.Services.ReadData.ReadData;

namespace venhaparaoleds_backend.src.Services.GenerateDB
{
    public static class GenerateDB
    {
        public static void GenerateData()
        {
            string result;

            Console.WriteLine("Gerando banco de dados...");

            IReadCandidato readCandidato = new ReadCandidato();
            result = readCandidato.Process();
            Console.WriteLine(result);

            IReadConcurso readConcurso = new ReadConcurso();
            result = readConcurso.Process();
            Console.WriteLine(result);
        }
    }
}
