using TrilhaBackendLeds.Dtos.CandidatoDtos;

namespace TrilhaBackendLeds.Services.Interfaces
{
    public interface ICandidatoService
    {
        CandidatoResponse Create(CandidatoCreateRequest candidato);
        CandidatoResponse FindById(int candidatoId);
        CandidatoResponse FindByCpf(string cpf);
        void IsCpfUnique(string cpf);
        IEnumerable<CandidatoResponse> FindAll();
        CandidatoResponse Update(int candidatoId, CandidatoUpdateRequest candidato);
        void DeleteById(int candidatoId);
    }
}
