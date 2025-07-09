using System;
using LedsAPI.Application.DTOs;

namespace LedsAPI.Application.Interfaces;

// Interface que define as opera��es dispon�veis para o servi�o de Profiss�o.
public interface IProfissaoService
{
    // Obt�m uma profiss�o por CPF (provavelmente deveria ser por nome ou ID da profiss�o,
    // mas est� definida com CPF na interface).
    Task<ProfissaoDto> GetAsync(string cpf);
    // Obt�m uma lista de todas as profiss�es.
    Task<List<ProfissaoDto>> GetAllAsync();
    // Insere uma nova profiss�o e retorna seu GUID.
    Task<Guid> InsertAsync(ProfissaoDto profissaoDto);
}