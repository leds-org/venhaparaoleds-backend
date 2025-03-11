using DesafioBackEnd.src.Domain;
using DesafioBackEnd.src.Services.StartMenu.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.src.Services.StartMenu.OpcoesMenu
{
    public class BuscaConcursoViaCPF : OpcaoMenu, IOpcaoMenu
    {
        private Candidato candidatoAlvo { get; set; }
        public BuscaConcursoViaCPF(List<Candidato> candidatos, List<Concurso> concursos) : base("Buscar concursos que se encaixam no perfil do candidato através do seu CPF", candidatos, concursos)
        {
            this.candidatoAlvo = new Candidato();
        }

        /*Método para posicionar no candidato alvo da busca.*/
        protected override bool Posicione() {

            string? cpfAlvo = null;

            Console.WriteLine("Digite o CPF do candidato.");

            try
            {
                cpfAlvo = Console.ReadLine();

                if (cpfAlvo == null || cpfAlvo == "")
                {
                    Console.WriteLine("Por favor, digite um CPF.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Não foi possível ler o dado inserido, tente novamente com outro.\nSegue o erro.\n" + e.Message);
                return false;
            }
            try{
                this.candidatoAlvo = candidatos.Find(x => x.cpf == cpfAlvo) ?? throw new InvalidOperationException("Candidato não encontrado.");
                return true; 
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        protected void BuscaConcursos() { 
            
            List<Concurso> concursosEncontrados = new List<Concurso>();
            List<string> profissaoDoConcucurso = new List<string>();

            foreach (Concurso concurso in this.concursos)
            {
                foreach (string profissao in this.candidatoAlvo.profissao)
                {
                    if (concurso.vaga.Contains(profissao))
                    {
                        concursosEncontrados.Add(concurso);
                        profissaoDoConcucurso.Add(profissao);
                    }
                }
            }

            if (concursosEncontrados.Count == 0)
            {
                Console.WriteLine("Nenhum concurso encontrado.");
                return;
            }
            else
            {
                Console.WriteLine("\nExiste um total de " + concursosEncontrados.Count.ToString() + " concurso" + (concursosEncontrados.Count > 1 ? "s" : "") + " em que " + candidatoAlvo.nome + " pode se inscrever.\n");

                for (int i = 0; i < concursosEncontrados.Count; i++)
                {
                    Console.WriteLine("Concurso " + (i + 1) + ":");
                    Console.WriteLine("Órgão: " + concursosEncontrados[i].orgao);
                    Console.WriteLine("Edital: " + concursosEncontrados[i].edital);
                    Console.WriteLine("Código: " + concursosEncontrados[i].codigo);
                    Console.WriteLine("Vaga: " + profissaoDoConcucurso[i] + "\n");
                }
            }
        }

        public override void Executar()
        {

            Console.Clear();

            if (this.Posicione())
            {
                this.BuscaConcursos();
            }
            else
            {
                Console.WriteLine("Não foi possível encontrar o candidato.");
            }

        }
    }
}
