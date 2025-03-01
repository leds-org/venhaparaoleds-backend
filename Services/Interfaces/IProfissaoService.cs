using TrilhaBackendLeds.Dtos.ProfissaoDtos;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Services.Interfaces
{
    public interface IProfissaoService
    {
        ProfissaoResponse Create(ProfissaoCreateRequest profissao);
        ProfissaoResponse FindById(int profissaoId);
        IEnumerable<ProfissaoResponse> FindAll();
        ProfissaoResponse Update(int profissaoId, ProfissaoUpdateRequest profissao);
        void DeleteById(int profissaoId);
    }
}
