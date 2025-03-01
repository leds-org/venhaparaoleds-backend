using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Repositories.Interfaces
{
    public interface ICandidatoProfissaoRepository
    {
        public CandidatoProfissao findCandidatoProfissao(int profissaoId, int candidatoId);
        Candidato AddProfissaoToCandidato(int profissaoId, int candidatoId);
        void RemoveProfissaoFromCandidato(CandidatoProfissao candidatoProfissao);
    }
}
