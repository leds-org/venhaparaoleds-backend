using TrilhaBackendLeds.Dtos.CandidatoDtos;

namespace TrilhaBackendLeds.Services.Interfaces
{
    public interface ICandidatoProfissaoService
    {
        CandidatoResponse AddProfissaoToCandidato(int profissaoId, int candidatoId);
        void RemoveProfissaoFromCandidato(int profissaoId, int candidatoId);
        void ValidateCandidatoProfissaoWhenAdding(int profissaoId, int candidatoId);
    }
}
