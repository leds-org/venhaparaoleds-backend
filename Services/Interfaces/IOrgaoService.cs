using TrilhaBackendLeds.Dtos.OrgaosDtos;

namespace TrilhaBackendLeds.Services.Interfaces
{
    public interface IOrgaoService
    {
        OrgaoResponse Create(OrgaoCreateRequest orgao);
        OrgaoResponse FindById(int orgaoId);
        OrgaoResponse FindByNome(string nomeDoOrgao);
        IEnumerable<OrgaoResponse> FindAll();
        OrgaoResponse Update(int orgaoId, OrgaoUpdateRequest orgao);
        void DeleteById(int orgaoId);
    }
}
