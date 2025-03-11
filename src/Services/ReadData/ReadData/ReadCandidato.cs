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
    public class ReadCandidato : ReadDom<Candidato>, IReadCandidato
    {
        public ReadCandidato() : base($"src/Data/candidatos.txt") { }

        public override (string, List<Candidato>) Process()
        {
            StreamReader arquivo = new(caminho);
            string? line;
            Candidato candidato;
            string[] dados;
            int n = 0;
            List<string> profissao;
            List<Candidato> candidatos = [];

            if (ValidateArchive())
            {
                line = arquivo.ReadLine();

                while (line != null)
                {
                    dados = line.Split(';');
                    dados[3] = dados[3].Replace("[", "").Replace("]", "").Replace(", ", ",");
                    profissao = [.. dados[3].Split(',')];
                    candidato = new Candidato(n, dados[0], dados[1], dados[2], profissao);

                    candidatos.Add(candidato);

                    n += 1;
                    line = arquivo.ReadLine();
                }

                return ("Arquivo de candidatos lido com sucesso", candidatos);
            }
            else
            {
                return ("Arquivo de candidatos vazio", candidatos);
            }
        }
    }
}
