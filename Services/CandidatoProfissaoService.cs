using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Services.Interfaces;
using TrilhaBackendLeds.Dtos.CandidatoDtos;
using TrilhaBackendLeds.Dtos.ProfissaoDtos;

namespace TrilhaBackendLeds.Services
{
    public class CandidatoProfissaoService : ICandidatoProfissaoService
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IProfissaoRepository _profissaoRepository;
        private readonly ICandidatoProfissaoRepository _candidatoProfissaoRepository;

        public CandidatoProfissaoService(ICandidatoRepository candidatoRepository, IProfissaoRepository profissaoRepository, ICandidatoProfissaoRepository candidatoProfissaoRepository)
        {
            _candidatoRepository = candidatoRepository;
            _profissaoRepository = profissaoRepository;
            _candidatoProfissaoRepository = candidatoProfissaoRepository;
        }

        public CandidatoResponse AddProfissaoToCandidato(int profissaoId, int candidatoId)
        {
            ValidateCandidatoProfissaoWhenAdding(profissaoId, candidatoId);
            var candidato = _candidatoProfissaoRepository.AddProfissaoToCandidato(profissaoId, candidatoId);
            return new CandidatoResponse(
                    candidato.Id,
                    candidato.Nome,
                    candidato.Cpf,
                    candidato.DataDeNascimento,
                    candidato.CandidatoProfissoes.Select(cp => new ProfissaoResponse { Id = cp.ProfissaoId, Nome = cp.Profissao.Nome }).ToList());
        }

        public void RemoveProfissaoFromCandidato(int profissaoId, int candidatoId)
        {
            var candidatoProfissao = _candidatoProfissaoRepository.findCandidatoProfissao(profissaoId, candidatoId);

            if (candidatoProfissao == null) throw new NotFoundException("Esse par candidato profissão não existe");

            _candidatoProfissaoRepository.RemoveProfissaoFromCandidato(candidatoProfissao);
     
        }
        public void ValidateCandidatoProfissaoWhenAdding(int profissaoId, int candidatoId)
        {
            var profissao = _profissaoRepository.FindById(profissaoId);
            if (profissao == null) throw new NotFoundException("A profissão não existe");

            var candidato = _candidatoRepository.FindById(candidatoId);
            if (candidato == null) throw new NotFoundException("O candidato não existe");

            var listaDeProfissoes = candidato.CandidatoProfissoes.Select(cp => cp.Profissao).Where(p => p.Id == profissaoId).ToList();

            if (listaDeProfissoes.Any()) throw new BadRequestException("Esse candidato já possui essa profissão");
        }
    }
}
