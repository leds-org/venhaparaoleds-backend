using LedsAPI.Application.DTOs;

namespace LedsAPI.Application.Interfaces;

// Esta � uma interface que define os contratos para o servi�o de Candidato.
// Ela especifica quais opera��es podem ser realizadas com os dados de candidatos.
public interface ICandidatoService
{
    // Obt�m um �nico Candidato pelo seu CPF de forma ass�ncrona.
    Task<CandidatoDto> GetAsync(string cpf);

    // Obt�m uma lista de todos os Candidatos de forma ass�ncrona.
    Task<List<CandidatoDto>> GetAllAsync();

    // Insere um novo Candidato ou atualiza um existente de forma ass�ncrona.
    Task InsertUpdateAsync(CandidatoDto candidatoDto);
}