using DesafioBackEnd.src.Domain;
using DesafioBackEnd.src.Services.StartMenu.Interface;
using DesafioBackEnd.src.Services.StartMenu.OpcoesMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.src.Services.StartMenu
{
    public static class GenerateMenu
    {
        public static void Generate(List<Candidato> candidatos, List<Concurso> concursos)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<OpcaoMenu> opcoes = new List<OpcaoMenu>();
            string? opcaoSelecionada = "";
            int iteradorOpcao, opcaoIndex;

            foreach (Type type in assembly.GetTypes())
            {
                // Verifica se a classe é uma subclasse de OpcaoMenu e não é abstrata
                if (typeof(OpcaoMenu).IsAssignableFrom(type) && !type.IsAbstract)
                {
                    // Cria uma instância da classe e adiciona à lista de opções
                    OpcaoMenu? opcao = Activator.CreateInstance(type, candidatos, concursos) as OpcaoMenu;
                    if (opcao != null)
                    {
                        opcoes.Add(opcao);
                    }
                }
            }

            while (opcaoSelecionada == "" || Convert.ToInt32(opcaoSelecionada) != opcoes.Count + 1)
            {

                Console.WriteLine("\nSelecione uma opção:");
                for (iteradorOpcao = 0; iteradorOpcao < opcoes.Count; iteradorOpcao++)
                {
                    Console.WriteLine($"{iteradorOpcao + 1}. {opcoes[iteradorOpcao].nome}");
                }

                Console.WriteLine($"{iteradorOpcao + 1}. Sair");

                try
                {
                    Console.WriteLine("\n");

                    opcaoSelecionada = Console.ReadLine();

                    opcaoIndex = Convert.ToInt32(opcaoSelecionada);

                    if (opcaoSelecionada != null && opcoes.ElementAtOrDefault(opcaoIndex - 1) != null)
                    {
                        opcoes[opcaoIndex - 1].Executar();
                    }
                    else if (opcaoSelecionada != null && opcaoIndex == iteradorOpcao + 1)
                    {
                        Console.WriteLine("Encerrando aplicação");
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Não foi possível ler o dado inserido, tente novamente com outro.");
                    opcaoSelecionada = "0";

                }
            }
        }
    }
}
