using TrilhaBackendLeds.Dtos.VagasDtos;

namespace TrilhaBackendLeds.Services.Interfaces
{
    public interface IVagasService
    {
        VagaResponse Create(VagaCreateRequest vaga);
        VagaResponse FindById(int vagaId);
        IEnumerable<VagaResponse> FindAll();
        VagaResponse Update(int vagaId, VagaUpdateRequest vaga);
        void DeleteById(int vagaId);
    }
}
