using System;
using LedsAPI.Domain.Entities;
using LedsAPI.Domain.Interfaces.Repositories;
using LedsAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace LedsAPI.Infra.Repositories;

// Implementa��o do reposit�rio para a entidade Concurso, lidando com o acesso a dados.
public class ConcursoRepository : IConcursoRepository
{
    private readonly LedsDbContext _context; // Contexto do banco de dados para interagir com as entidades.

    // Construtor que injeta o contexto do banco de dados.
    public ConcursoRepository(LedsDbContext context)
    {
        _context = context;
    }

    // Obt�m um concurso pelo seu c�digo, incluindo vagas e profiss�es necess�rias.
    public async Task<Concurso> GetByCdConcurso(long cdConcurso)
    {
        return await _context.Concursos
            .Include(c => c.Vagas) // Inclui as vagas relacionadas ao concurso.
            .ThenInclude(v => v.ProfissoesNecessarias) // Em seguida, inclui as profiss�es necess�rias de cada vaga.
            .FirstOrDefaultAsync(x => x.CdConcurso.Equals(cdConcurso)); // Busca o primeiro concurso com o c�digo correspondente.
    }

    // Obt�m uma lista de concursos que possuem vagas com as profiss�es especificadas.
    public async Task<List<Concurso>> GetByProfissoes(IEnumerable<string> listaProfissoes)
    {
        return await _context.Concursos
            .Include(c => c.Vagas)
            .ThenInclude(v => v.ProfissoesNecessarias)
            .Where(c => c.Vagas.Any(v => v.ProfissoesNecessarias.Any(p => listaProfissoes.Contains(p.NomeProf)))) // Filtra concursos onde qualquer vaga tenha qualquer uma das profiss�es.
            .ToListAsync();
    }

    // Obt�m todos os concursos, incluindo suas vagas e as profiss�es necess�rias.
    public async Task<List<Concurso>> GetAllAsync()
    {
        return await _context.Concursos
            .Include(c => c.Vagas)
            .ThenInclude(v => v.ProfissoesNecessarias)
            .ToListAsync();
    }

    // Adiciona um novo concurso ao banco de dados.
    public async Task AddAsync(Concurso Concurso)
    {
        _context.Concursos.Add(Concurso);
        await _context.SaveChangesAsync(); // Salva as altera��es de forma ass�ncrona.
    }

    // Atualiza um concurso existente no banco de dados.
    public async Task UpdateAsync(Concurso Concurso)
    {
        _context.Concursos.Update(Concurso);
        await _context.SaveChangesAsync(); // Salva as altera��es de forma ass�ncrona.
    }
}