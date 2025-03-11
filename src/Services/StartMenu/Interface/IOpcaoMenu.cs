using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.src.Services.StartMenu.Interface
{
    internal interface IOpcaoMenu
    {
        string nome { get; }
        void Executar();
    }
}
