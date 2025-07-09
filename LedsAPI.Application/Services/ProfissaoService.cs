using LedsAPI.Application.Converters;
using LedsAPI.Application.DTOs;
using LedsAPI.Application.Interfaces;
using LedsAPI.Domain.Entities;
using LedsAPI.Domain.Interfaces.Repositories;

namespace LedsAPI.Application.Services;

public class ProfissaoService : IProfissaoService
{
    private readonly IProfissaoRepository _profissaoRepository;
    public ProfissaoService(IProfissaoRepository profissaoRepository)
    {
        _profissaoRepository = profissaoRepository;
    }
    public async Task<ProfissaoDto> GetAsync(string nomeProf)
    {
        var profissao = await _profissaoRepository.GetByNome(nomeProf);
        return profissao == null ? null : MapConverter.MapToDto(profissao);
    }

    public async Task<List<ProfissaoDto>> GetAllAsync()
    {
        var profissoes = await _profissaoRepository.GetAllAsync();
        return profissoes?.Select(MapConverter.MapToDto).ToList();
    }

    public async Task<Guid> InsertAsync(ProfissaoDto profissaoDto)
    {
        var profissao = await _profissaoRepository.GetByNome(profissaoDto.NomeProf);
        if (profissao == null)
        {
            profissao = new Profissao(profissaoDto.NomeProf);
            await _profissaoRepository.AddAsync(profissao);
        }
        else
        {
            throw new Exception($"Profissão '{profissaoDto.NomeProf}' já existe."); // Retorna 409 Conflict se j� existir.
        }
        return profissao.Id;
    }    
}
