using LedsAPI.Domain.Entities;
using LedsAPI.Domain.Interfaces.Repositories;
using LedsAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace LedsAPI.Infra.Repositories;

// Implementa��o do reposit�rio para a entidade Profiss�o, respons�vel pelo acesso a dados.
public class ProfissaoRepository : IProfissaoRepository
{
    private readonly LedsDbContext _context; // Contexto do banco de dados.

    // Construtor que recebe o contexto do banco de dados via inje��o de depend�ncia.
    public ProfissaoRepository(LedsDbContext context)
    {
        _context = context;
    }

    // Obt�m uma profiss�o pelo seu nome.
    public async Task<Profissao> GetByNome(string nome)
    {
        return await _context.Profissoes.FirstOrDefaultAsync(x => x.NomeProf.Equals(nome));
    }

    // Obt�m uma lista de todas as profiss�es.
    public async Task<List<Profissao>> GetAllAsync()
    {
        return await _context.Profissoes.ToListAsync();
    }

    // Adiciona uma nova profiss�o ao banco de dados.
    public async Task AddAsync(Profissao profissao)
    {
        _context.Profissoes.Add(profissao);
        await _context.SaveChangesAsync(); // Salva as mudan�as.
    }
}