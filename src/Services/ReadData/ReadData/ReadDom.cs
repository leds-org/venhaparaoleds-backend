using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioBackEnd.src.Services.ReadData.Interface;

namespace DesafioBackEnd.src.Services.ReadData.ReadData
{
    public abstract class ReadDom<T> : IReadDom<T>
    {
        protected string caminho { get; }

        protected ReadDom(string caminho)
        {
            this.caminho = caminho;
        }
        protected bool ValidateArchive()
        {

            using StreamReader arquivo = new StreamReader(caminho);
            string? line = arquivo.ReadLine();

            if (line == null)
            {
                return false;
            }

            return true;
        }
        public abstract (string, List<T>) Process();
    }
}
