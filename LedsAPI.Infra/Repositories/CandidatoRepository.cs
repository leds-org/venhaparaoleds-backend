using LedsAPI.Domain.Entities;
using LedsAPI.Domain.Interfaces.Repositories;
using LedsAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace LedsAPI.Infra.Repositories;

// Implementa��o do reposit�rio para a entidade Candidato, interagindo com o banco de dados.
public class CandidatoRepository : ICandidatoRepository
{
    private readonly LedsDbContext _context; // Contexto do banco de dados para acesso aos dados.

    // Construtor que recebe o contexto do banco de dados via inje��o de depend�ncia.
    public CandidatoRepository(LedsDbContext context)
    {
        _context = context;
    }

    // Obt�m um candidato pelo seu CPF, incluindo suas profiss�es relacionadas.
    public async Task<Candidato> GetByCPF(string cpf)
    {
        return await _context.Candidatos.Include(c => c.Profissoes).FirstOrDefaultAsync(x => x.CPF.Equals(cpf));
    }

    // Obt�m uma lista de candidatos que possuem alguma das profiss�es especificadas.
    public async Task<List<Candidato>> GetByProfissoes(IEnumerable<string> listaProfissoes)
    {
        return await _context.Candidatos
            .Include(c => c.Profissoes)
            .Where(c => c.Profissoes.Any(p => listaProfissoes.Contains(p.NomeProf)))
            .ToListAsync();
    }

    // Obt�m todos os candidatos, incluindo suas profiss�es.
    public async Task<List<Candidato>> GetAllAsync()
    {
        return await _context.Candidatos.Include(c => c.Profissoes).ToListAsync();
    }

    // Adiciona um novo candidato ao banco de dados.
    public async Task AddAsync(Candidato candidato)
    {
        _context.Candidatos.Add(candidato);
        await _context.SaveChangesAsync(); // Salva as mudan�as no banco.
    }

    // Atualiza um candidato existente no banco de dados.
    public async Task UpdateAsync(Candidato candidato)
    {
        _context.Candidatos.Update(candidato);
        await _context.SaveChangesAsync(); // Salva as mudan�as no banco.
    }
}