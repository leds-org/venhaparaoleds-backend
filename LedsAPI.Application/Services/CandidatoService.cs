using LedsAPI.Application.Interfaces;
using LedsAPI.Domain.Entities;
using LedsAPI.Domain.Interfaces.Repositories;
using LedsAPI.Application.DTOs;
using LedsAPI.Application.Converters;

namespace LedsAPI.Application.Services;

public class CandidatoService : ICandidatoService
{
    private readonly ICandidatoRepository _candidatoRepository;
    private readonly IProfissaoRepository _profissaoRepository;
    public CandidatoService(ICandidatoRepository candidatoRepository,
                            IProfissaoRepository profissaoRepository)
    {
        _candidatoRepository = candidatoRepository;
        _profissaoRepository = profissaoRepository;
    }
    public async Task<CandidatoDto> GetAsync(string cpf)
    {
        var candidato = await _candidatoRepository.GetByCPF(cpf);
        return candidato == null ? null : MapConverter.MapToDto(candidato);
    }

    public async Task<List<CandidatoDto>> GetAllAsync()
    {
        var candidatos = await _candidatoRepository.GetAllAsync();
        return candidatos?.Select(MapConverter.MapToDto).ToList();
    }

    public async Task InsertUpdateAsync(CandidatoDto candidatoDto)
    {
        var profissoes = new List<Profissao>();
        foreach (var profissaoDto in candidatoDto.Profissoes)
        {
            var profissao = await _profissaoRepository.GetByNome(profissaoDto.NomeProf);
            if (profissao == null)
            {
                profissao = new Profissao(profissaoDto.NomeProf);
                await _profissaoRepository.AddAsync(profissao);
            }
            profissoes.Add(profissao);
        }

        var candidato = await _candidatoRepository.GetByCPF(candidatoDto.CPF);
        if (candidato == null)
        {
            candidato = new Candidato(candidatoDto.CPF, candidatoDto.Nome, candidatoDto.Nascimento, profissoes);
            await _candidatoRepository.AddAsync(candidato);
        }
        else
        {
            candidato.Atualizar(candidatoDto.Nome, candidatoDto.Nascimento, profissoes);
            await _candidatoRepository.UpdateAsync(candidato);
        }
    }
}
