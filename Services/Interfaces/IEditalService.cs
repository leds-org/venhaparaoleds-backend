using TrilhaBackendLeds.Dtos.EditaisDtos;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Services.Interfaces
{
    public interface IEditalService
    {
        EditalResponse Create(EditalCreateRequest edital);
        EditalResponse FindById(int editalId);
        EditalResponse FindByOrgaoIdAndNumeroDoEdital(int orgaoId, string numeroDoEdital);
        IEnumerable<EditalResponse> FindAll();
        EditalResponse Update(int editalId, EditalUpdateRequest edital);
        void DeleteById(int editalId);
    }
}
