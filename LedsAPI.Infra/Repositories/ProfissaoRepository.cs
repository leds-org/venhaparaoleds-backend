using LedsAPI.Domain.Entities;
using LedsAPI.Domain.Interfaces.Repositories;
using LedsAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace LedsAPI.Infra.Repositories;

// Implementação do repositório para a entidade Profissão, responsável pelo acesso a dados.
public class ProfissaoRepository : IProfissaoRepository
{
    private readonly LedsDbContext _context; // Contexto do banco de dados.

    // Construtor que recebe o contexto do banco de dados via injeção de dependência.
    public ProfissaoRepository(LedsDbContext context)
    {
        _context = context;
    }

    // Obtém uma profissão pelo seu nome.
    public async Task<Profissao> GetByNome(string nome)
    {
        return await _context.Profissoes.FirstOrDefaultAsync(x => x.NomeProf.Equals(nome));
    }

    // Obtém uma lista de todas as profissões.
    public async Task<List<Profissao>> GetAllAsync()
    {
        return await _context.Profissoes.ToListAsync();
    }

    // Adiciona uma nova profissão ao banco de dados.
    public async Task AddAsync(Profissao profissao)
    {
        _context.Profissoes.Add(profissao);
        await _context.SaveChangesAsync(); // Salva as mudanças.
    }
}