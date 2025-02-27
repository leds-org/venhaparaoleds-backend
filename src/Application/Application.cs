using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using venhaparaoleds_backend.src.Services.GenerateDB;

namespace venhaparaoleds_backend.src.Application
{
    internal class Application
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando aplicação...");

            GenerateDB.GenerateData();
        }
    }
}
