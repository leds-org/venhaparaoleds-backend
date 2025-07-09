using LedsAPI.Domain.Entities;

namespace LedsAPI.Domain.Interfaces.Repositories;
// Interface que define os contratos para operações de acesso a dados (repositório) da entidade Profissão.
public interface IProfissaoRepository
{
    Task<Profissao> GetByNome(string nome);
    Task<List<Profissao>> GetAllAsync();
    Task AddAsync(Profissao profissao);
}
