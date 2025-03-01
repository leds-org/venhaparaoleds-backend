using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Repositories.Interfaces
{
    public interface IVagasRepository
    {
        IEnumerable<Vaga> FindAll();
        Vaga FindById(int vagaId);
        Vaga FindByProfissaoIdAndEditalId(int profissaoId, int editalId);
        Vaga Create(Vaga vaga);
        Vaga Update(Vaga vaga);
        void DeleteById(int vagaId);
    }
}
