using LedsAPI.Domain.Entities;

namespace LedsAPI.Domain.Interfaces.Repositories;

// Esta interface define o contrato para opera��es de acesso a dados (reposit�rio) da entidade Candidato.
public interface ICandidatoRepository
{
    Task<Candidato> GetByCPF(string cpf);
    Task<List<Candidato>> GetByProfissoes(IEnumerable<string> listaProfissoes);
    Task<List<Candidato>> GetAllAsync();
    Task AddAsync(Candidato candidato);
    Task UpdateAsync(Candidato candidato);
}