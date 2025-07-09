using LedsAPI.Domain.Entities;

namespace LedsAPI.Domain.Interfaces.Repositories;
// Interface que define os contratos para opera��es de acesso a dados (reposit�rio) da entidade Profiss�o.
public interface IProfissaoRepository
{
    Task<Profissao> GetByNome(string nome);
    Task<List<Profissao>> GetAllAsync();
    Task AddAsync(Profissao profissao);
}
