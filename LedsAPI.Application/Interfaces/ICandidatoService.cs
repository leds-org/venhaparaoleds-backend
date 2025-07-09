using LedsAPI.Application.DTOs;

namespace LedsAPI.Application.Interfaces;

// Esta é uma interface que define os contratos para o serviço de Candidato.
// Ela especifica quais operações podem ser realizadas com os dados de candidatos.
public interface ICandidatoService
{
    // Obtém um único Candidato pelo seu CPF de forma assíncrona.
    Task<CandidatoDto> GetAsync(string cpf);

    // Obtém uma lista de todos os Candidatos de forma assíncrona.
    Task<List<CandidatoDto>> GetAllAsync();

    // Insere um novo Candidato ou atualiza um existente de forma assíncrona.
    Task InsertUpdateAsync(CandidatoDto candidatoDto);
}