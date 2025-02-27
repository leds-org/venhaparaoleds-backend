using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using venhaparaoleds_backend.src.Services.ReadData.Interface;

namespace venhaparaoleds_backend.src.Services.ReadData.ReadData
{
    public class ReadConcurso : IReadConcurso
    {
        private string caminho = $"../../src/Data/concursos.txt";

        private bool ValidateArchive()
        {

            string line;

            StreamReader sr = new StreamReader(this.caminho);

            line = sr.ReadLine();

            if (line == null)
            {
                return false;
            }

            return true;
        }
        public string Process()
        {
            if (!ValidateArchive())
            {
                return "Arquivo de concursos vazio";
            }
            else
            {
                return "Arquivo de concursos lido";
            }
        }
    }
}
