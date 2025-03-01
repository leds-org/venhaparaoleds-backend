using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Repositories.Interfaces
{
    public interface ICandidatoRepository
    {
        IEnumerable<Candidato> FindAll();
        IEnumerable<Candidato> FindByProfissoes(List<int> profissoesIds);
        Candidato FindById(int candidatoId);
        Candidato FindByCpf(string cpf);
        Candidato FindByCpfLight(string cpf);
        Candidato Create(Candidato candidato);
        Candidato Update(Candidato candidato);
        void DeleteById(int candidatoId);

    }
}
