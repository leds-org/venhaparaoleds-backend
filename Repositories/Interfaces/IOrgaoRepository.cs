using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Repositories.Interfaces
{
    public interface IOrgaoRepository
    {
        IEnumerable<Orgao> FindAll();
        Orgao FindById(int orgaoId);
        Orgao FindByNome(string nome);
        Orgao FindOrgaoWithEditais(int orgaoId);
        Orgao Create(Orgao orgao);
        Orgao Update(Orgao orgao);
        void DeleteById(int orgaoId);
    }
}
