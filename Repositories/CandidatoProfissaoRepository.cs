using Microsoft.EntityFrameworkCore;
using TrilhaBackendLeds.Context;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;

namespace TrilhaBackendLeds.Repositories
{
    public class CandidatoProfissaoRepository : ICandidatoProfissaoRepository
    {

        private readonly TrilhaBackEndLedsContext _context;

        public CandidatoProfissaoRepository(TrilhaBackEndLedsContext context)
        {
            _context = context;
        }

        public CandidatoProfissao findCandidatoProfissao(int profissaoId, int candidatoId)
        {
            var candidatoProfissao = _context.CandidatoProfissoes
                .FirstOrDefault(cp => cp.ProfissaoId == profissaoId && cp.CandidatoId == candidatoId);

            return candidatoProfissao;
        }

        public Candidato AddProfissaoToCandidato(int profissaoId, int candidatoId)
        {
            var candidatoProfissao = new CandidatoProfissao
            {
                ProfissaoId = profissaoId,
                CandidatoId = candidatoId
            };

            _context.CandidatoProfissoes.Add(candidatoProfissao);
            _context.SaveChanges();
            return _context.Candidatos.Include(c => c.CandidatoProfissoes).ThenInclude(cp => cp.Profissao).FirstOrDefault(c => c.Id == candidatoId);
        }

        public void RemoveProfissaoFromCandidato(CandidatoProfissao candidatoProfissao) 
        {
            
            _context.CandidatoProfissoes.Remove(candidatoProfissao);
            _context.SaveChanges();
        }
    }
}
