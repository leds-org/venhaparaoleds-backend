using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DesafioBackEnd.src.Domain;

namespace DesafioBackEnd.src.Services.ReadData.Interface
{
    internal interface IReadDom <T>
    {
        (string, List<T>) Process();
    }
}
