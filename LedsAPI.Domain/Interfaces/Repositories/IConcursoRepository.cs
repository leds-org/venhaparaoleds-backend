using LedsAPI.Domain.Entities;

namespace LedsAPI.Domain.Interfaces.Repositories;
// Interface que define os contratos para opera��es de acesso a dados (reposit�rio) da entidade Concurso.
public interface IConcursoRepository
{
    Task<Concurso> GetByCdConcurso(long cdConcurso);
    Task<List<Concurso>> GetByProfissoes(IEnumerable<string> listaProfissoes);
    Task<List<Concurso>> GetAllAsync();
    Task AddAsync(Concurso concurso);
}
