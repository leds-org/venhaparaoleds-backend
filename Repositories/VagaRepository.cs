using Microsoft.EntityFrameworkCore;
using TrilhaBackendLeds.Context;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;

namespace TrilhaBackendLeds.Repositories
{
    public class VagaRepository : IVagasRepository
    {
        private readonly TrilhaBackEndLedsContext _context;

        public VagaRepository(TrilhaBackEndLedsContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Vaga> FindAll()
        {
            return _context.Vagas
                .Include(v => v.Profissao)
                .Include(v => v.Edital)
                    .ThenInclude(edital => edital.Orgao)
                .Include(v => v.Edital)
                    .ThenInclude(edital => edital.Concurso)
                .OrderBy(v => v.Id)
                .ToList();
        }

        public Vaga FindById(int vagaId)
        {
            return _context.Vagas
                .Include(v => v.Profissao)
                .Include(v => v.Edital)
                    .ThenInclude(edital => edital.Orgao)
                .Include(v => v.Edital)
                    .ThenInclude(edital => edital.Concurso)
                .FirstOrDefault(v => v.Id == vagaId);

        }

        public Vaga FindByProfissaoIdAndEditalId(int profissaoId, int editalId)
        {
            return _context.Vagas
                .Include(v => v.Profissao)
                .Include(v => v.Edital)
                    .ThenInclude(edital => edital.Orgao)
                .Include(v => v.Edital)
                    .ThenInclude(edital => edital.Concurso)
                .FirstOrDefault(v => v.EditalId == editalId && v.ProfissaoId == profissaoId);
        }

        public Vaga Create(Vaga vaga)
        {
            _context.Vagas.Add(vaga);
            _context.SaveChanges();
            return vaga;
        }

        public Vaga Update(Vaga vaga)
        {
            _context.Vagas.Entry(vaga).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return vaga;
        }

        public void DeleteById(int vagaId)
        {
            Vaga vaga = _context.Vagas.Find(vagaId);

            if (vaga == null) throw new NotFoundException("Vaga não encontrada");

            _context.Vagas.Remove(vaga);
            _context.SaveChanges();
        }

    }
}
