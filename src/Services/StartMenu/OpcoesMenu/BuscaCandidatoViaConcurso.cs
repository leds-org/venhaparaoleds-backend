using DesafioBackEnd.src.Domain;
using DesafioBackEnd.src.Services.StartMenu.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.src.Services.StartMenu.OpcoesMenu
{
    internal class BuscaCandidatoViaConcurso : OpcaoMenu, IOpcaoMenu
    {   
        private Concurso concursoAlvo { get; set; }

        public BuscaCandidatoViaConcurso(List<Candidato> candidatos, List<Concurso> concursos) : base("Buscar potenciais candidatos por código de concurso", candidatos, concursos)
        {
            this.concursoAlvo = new Concurso();
        }

        /*Método para posicionar no concurso alvo da busca.*/
        protected override bool Posicione()
        {

            string? concursoAlvo = null;

            Console.WriteLine("Digite o código do Concurso.");

            try
            {
                concursoAlvo = Console.ReadLine();

                if (concursoAlvo == null || concursoAlvo == "")
                {
                    Console.WriteLine("Por favor, digite um código.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível ler o dado inserido, tente novamente com outro.\nSegue o erro.\n" + e.Message);
                return false;
            }
            try
            {
                this.concursoAlvo = concursos.Find(x => x.codigo == concursoAlvo) ?? throw new InvalidOperationException("Concurso não encontrado.");
                return true;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        protected void BuscaCandidato()
        {

            List<Candidato> candidatosEncontrados = new List<Candidato>();
            List<string> profissaoDoCandidato = new List<string>();

            foreach (Candidato candidato in this.candidatos)
            {
                foreach (string vaga in this.concursoAlvo.vaga)
                {
                    if (candidato.profissao.Contains(vaga))
                    {
                        candidatosEncontrados.Add(candidato);
                        profissaoDoCandidato.Add(vaga);
                    }
                }
            }

            if (candidatosEncontrados.Count == 0)
            {
                Console.WriteLine("Nenhum candidato apto encontrado.");
                return;
            }
            else
            {
                Console.WriteLine("\nExiste um total de " + candidatosEncontrados.Count.ToString() + " candidato " + (candidatosEncontrados.Count > 1 ? "s aptos" : "apto") + " para concorrer no concurso " + concursoAlvo.codigo + " do órgão " + concursoAlvo.orgao + " e edital " + concursoAlvo.edital + ".");

                for (int i = 0; i < candidatosEncontrados.Count; i++)
                {
                    Console.WriteLine("Candidato " + (i + 1) + ":");
                    Console.WriteLine("Nome: " + candidatosEncontrados[i].nome);
                    Console.WriteLine("Data de Nascimento: " + candidatosEncontrados[i].dtNascimento);
                    Console.WriteLine("CPF: " + candidatosEncontrados[i].cpf);
                    Console.WriteLine("Vaga: " + profissaoDoCandidato[i] + "\n");
                }
            }
        }

        public override void Executar()
        {
            Console.Clear();

            if (this.Posicione())
            {
                this.BuscaCandidato();

            }
            else
            {
                Console.WriteLine("Não foi possível encontrar o concurso.");
            }
        }
    }
}
