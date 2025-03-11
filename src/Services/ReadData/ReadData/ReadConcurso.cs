using DesafioBackEnd.src.Services.ReadData.ReadData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DesafioBackEnd.src.Domain;
using DesafioBackEnd.src.Services.ReadData.Interface;

namespace DesafioBackEnd.src.Services.ReadData.ReadData
{
    public class ReadConcurso : ReadDom<Concurso>, IReadConcurso
    {
        public ReadConcurso() : base($"../../../src/Data/concursos.txt") { }

        public override (string, List<Concurso>) Process()
        {
            StreamReader arquivo = new StreamReader(caminho);
            string? line;
            Concurso concurso;
            string[] dados;
            int n = 0;
            List<string> vaga;
            List<Concurso> concursos = new List<Concurso>();

            if (ValidateArchive())
            {
                line = arquivo.ReadLine();

                while (line != null)
                {
                    dados = line.Split(';');
                    dados[3] = dados[3].Replace("[", "").Replace("]", "").Replace(", ", ",");
                    vaga = dados[3].Split(',').ToList();
                    concurso = new Concurso(n, dados[0], dados[1], dados[2], vaga);

                    concursos.Add(concurso);

                    n += 1;
                    line = arquivo.ReadLine();
                }
                return ("Arquivo de concursos lido com sucesso", concursos);
            }
            else
            {
                return ("Arquivo de concursos vazio", concursos);
            }
        }
    }
}
