using TrilhaBackendLeds.Dtos.CandidatoDtos;
using TrilhaBackendLeds.Dtos.MatchDtos;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Services
{
    public class MatchService
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IConcursoRepository _concursoRepository;
        private readonly IEditalRepository _editalRepository;
        private readonly CandidatoService _candidatoService;
        private readonly ConcursoService _concursoService;

        public MatchService(ICandidatoRepository candidatoRepository, IConcursoRepository concursoRepository, IEditalRepository editalRepository, CandidatoService candidatoService, ConcursoService concursoService)
        {
            _candidatoRepository = candidatoRepository;
            _concursoRepository = concursoRepository;
            _editalRepository = editalRepository;
            _candidatoService = candidatoService;
            _concursoService = concursoService;
        }

        public List<OpportunitiesResponse> FindOpportunitiesByCpf(string cpf)
        {
            _candidatoService.ValidateCPF(cpf);
            var candidato = _candidatoRepository.FindByCpf(cpf);

            if (candidato == null) throw new NotFoundException("Candidato não encontrado");

            var profissaoIds = candidato.CandidatoProfissoes.Select(cp => cp.Profissao.Id).ToList();

            var editais = _editalRepository.FindOpportunitiesByProfessions(profissaoIds);

            return editais.Select(e => new OpportunitiesResponse(
                                  e.NumeroDoEdital,
                                  e.Concurso.CodigoDoConcurso,
                                  e.Orgao.Nome )).ToList();
        }

        public List<CandidatoConcursoMatchResponse> FindCandidatosByCodigoDoConcurso(string codigoConcurso)
        {
            _concursoService.ValidateCodigoDoConcurso(codigoConcurso);

            var concurso = _concursoRepository.FindByCodigoDoConcursoWithEdital(codigoConcurso);
            if (concurso == null) throw new NotFoundException("Concurso não encontrado");

            var profissaoIds = concurso.SelectMany(c => c.Editais).SelectMany(e => e.Vagas).Select(v => v.Profissao.Id).ToList();

            var candidatos = _candidatoRepository.FindByProfissoes(profissaoIds);

            return candidatos.Select(e => new CandidatoConcursoMatchResponse(
                                  e.Nome,
                                  e.DataDeNascimento,
                                  e.Cpf)).ToList();
        }


    }
}
