using LedsAPI.Application.DTOs;

namespace LedsAPI.Application.Interfaces;

// Interface que define as opera��es dispon�veis para o servi�o de Concurso.
public interface IConcursoService
{
    // Obt�m um concurso pelo seu c�digo.
    Task<ConcursoDto> GetAsync(long cdConcurso);
    // Obt�m uma lista de todos os concursos.
    Task<List<ConcursoDto>> GetAllAsync();
    // Insere um novo concurso.
    Task InsertAsync(ConcursoDto concursoDto);
    // Obt�m uma lista de concursos considerados compat�veis.
    Task<List<CasamentoResultadoDto>> ObterConcursosCompativeisAsync();
    // Busca concursos relacionados a um CPF espec�fico.
    Task<List<ConcursoDto>> BuscarConcursosPorCpfAsync(string cpf);
    // Busca candidatos por um c�digo de concurso.
    Task<List<CandidatoDto>> BuscarCandidatosPorConcursoAsync(long cdConcurso);
}