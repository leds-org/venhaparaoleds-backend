using DesafioBackEnd.src.Domain;
using DesafioBackEnd.src.Services.StartMenu.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.src.Services.StartMenu.OpcoesMenu
{
    public abstract class OpcaoMenu : IOpcaoMenu
    {
        public string nome { get; private set; }
        public List<Candidato> candidatos { get; private set; }
        public List<Concurso> concursos { get; private set; }

        protected OpcaoMenu(string nome, List<Candidato> candidatos, List<Concurso> concursos)
        {
            this.nome = nome;
            this.candidatos = candidatos;
            this.concursos = concursos;
        }
        protected abstract bool Posicione();
        public abstract void Executar();
    }

}
