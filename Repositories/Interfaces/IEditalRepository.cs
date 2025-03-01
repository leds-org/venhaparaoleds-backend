using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Repositories.Interfaces
{
    public interface IEditalRepository
    {
        IEnumerable<Edital> FindAll();
        IEnumerable<Edital> FindOpportunitiesByProfessions(List<int> professionsId);
        Edital FindById(int editalId);
        Edital FindByOrgaoIdAndNumeroDoEdital(int orgaoId, string numeroDoEdital);
        Edital Create(Edital edital);
        Edital Update(Edital edital);
        void DeleteById(int editalId);
    }
}
