using LedsAPI.Application.DTOs;
using LedsAPI.Domain.Entities;

namespace LedsAPI.Application.Converters;

// Classe estática responsável por mapear (converter) objetos
// das entidades de domínio para os DTOs correspondentes.
public static class MapConverter
{
    public static ConcursoDto MapToDto(Concurso concurso)
    {

        // Converte um objeto 'Concurso' (Entidade) para 'ConcursoDto' (DTO).
        // Inclui o mapeamento de suas vagas relacionadas.
        return new ConcursoDto()
        {
            CdConcurso = concurso.CdConcurso,
            Orgao = concurso.Orgao,
            Edital = concurso.Edital,
            Vagas = concurso.Vagas.Select(MapToDto).ToList() // Mapeia recursivamente a lista de vagas associadas.
        };
    }
    // Converte um objeto 'EmpregoVaga' (Entidade) para 'VagaDto' (DTO).
    // Inclui o mapeamento das profissões necessárias.
    public static VagaDto MapToDto(EmpregoVaga empregoVaga)
    {
        return new VagaDto()
        {
            NomeVag = empregoVaga.NomeVag,
            ProfissoesNecessarias = empregoVaga.ProfissoesNecessarias.Select(MapToDto).ToList()
        };
    }
    public static CandidatoDto MapToDto(Candidato candidato)
    {
        return new CandidatoDto()
        {
            CPF = candidato.CPF,
            Nome = candidato.Nome,
            Nascimento = candidato.Nascimento,
            Profissoes = candidato.Profissoes.Select(MapToDto).ToList()
        };
    }
    // Converte um objeto 'Profissao' (Entidade) para 'ProfissaoDto' (DTO).
    public static ProfissaoDto MapToDto(Profissao profissao)
    {
        return new ProfissaoDto()
        {
            NomeProf = profissao.NomeProf
        };
    }
}
