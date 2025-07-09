using LedsAPI.Application.DTOs;

namespace LedsAPI.Application.Interfaces;

// Interface que define as operações disponíveis para o serviço de Concurso.
public interface IConcursoService
{
    // Obtém um concurso pelo seu código.
    Task<ConcursoDto> GetAsync(long cdConcurso);
    // Obtém uma lista de todos os concursos.
    Task<List<ConcursoDto>> GetAllAsync();
    // Insere um novo concurso.
    Task InsertAsync(ConcursoDto concursoDto);
    // Obtém uma lista de concursos considerados compatíveis.
    Task<List<CasamentoResultadoDto>> ObterConcursosCompativeisAsync();
    // Busca concursos relacionados a um CPF específico.
    Task<List<ConcursoDto>> BuscarConcursosPorCpfAsync(string cpf);
    // Busca candidatos por um código de concurso.
    Task<List<CandidatoDto>> BuscarCandidatosPorConcursoAsync(long cdConcurso);
}