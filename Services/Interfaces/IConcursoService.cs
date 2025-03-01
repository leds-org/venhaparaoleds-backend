using TrilhaBackendLeds.Dtos.CandidatoDtos;
using TrilhaBackendLeds.Dtos.ConcursoDtos;

namespace TrilhaBackendLeds.Services.Interfaces
{
    public interface IConcursoService
    {
        ConcursoResponse Create(ConcursoCreateRequest concurso);
        ConcursoResponse FindById(int concursoId);
        ConcursoResponse FindByCodigoDoConcurso(string codigoDoConcurso);
        IEnumerable<ConcursoResponse> FindAll();
        ConcursoResponse Update(int concursoId, ConcursoUpdateRequest concurso);
        void DeleteById(int concursoId);
    }
}
