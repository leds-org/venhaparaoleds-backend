using System;
using LedsAPI.Application.DTOs;

namespace LedsAPI.Application.Interfaces;

// Interface que define as operações disponíveis para o serviço de Profissão.
public interface IProfissaoService
{
    // Obtém uma profissão por CPF (provavelmente deveria ser por nome ou ID da profissão,
    // mas está definida com CPF na interface).
    Task<ProfissaoDto> GetAsync(string cpf);
    // Obtém uma lista de todas as profissões.
    Task<List<ProfissaoDto>> GetAllAsync();
    // Insere uma nova profissão e retorna seu GUID.
    Task<Guid> InsertAsync(ProfissaoDto profissaoDto);
}