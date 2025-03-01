
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Repositories.Interfaces
{
    public interface IProfissaoRepository
    {
        IEnumerable<Profissao> FindAll();
        Profissao FindById(int profissaoId);
        Profissao FindByNome(string nomeDaProfissao);
        Profissao Create(Profissao profissao);
        Profissao Update(Profissao profissao);
        void DeleteById(int profissaoId);
    }
}
