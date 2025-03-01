using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Repositories.Interfaces
{
    public interface IConcursoRepository
    {
        IEnumerable<Concurso> FindAll();
        IEnumerable<Concurso> FindByCodigoDoConcursoWithEdital(string codigoDoConcurso);
        Concurso FindById(int concursoId);
        Concurso FindByCodigoDoConcurso(string codigoDoConcurso);
        Concurso Create(Concurso concurso);
        Concurso Update(Concurso concurso);
        void DeleteById(int concursoId);
    }
}
