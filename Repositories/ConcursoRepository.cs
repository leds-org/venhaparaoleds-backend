using Microsoft.EntityFrameworkCore;
using TrilhaBackendLeds.Context;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;

namespace TrilhaBackendLeds.Repositories
{
    public class ConcursoRepository : IConcursoRepository
    {
        private readonly TrilhaBackEndLedsContext _context;

        public ConcursoRepository(TrilhaBackEndLedsContext context)
        {
            _context = context;
        }

        public Concurso FindById(int concursoId)
        {
            return _context.Concursos.Find(concursoId);
        }

        public Concurso FindByCodigoDoConcurso(string codigoDoConcurso)
        {
            return _context.Concursos.FirstOrDefault(concurso => concurso.CodigoDoConcurso == codigoDoConcurso);
        }

        public IEnumerable<Concurso> FindByCodigoDoConcursoWithEdital(string codigoDoConcurso)
        {
            return _context.Concursos
                .Include(c => c.Editais)
                .ThenInclude(e => e.Vagas)
                .ThenInclude(v => v.Profissao)
                .Where(c => c.CodigoDoConcurso == codigoDoConcurso).ToList();
        }

        public IEnumerable<Concurso> FindAll()
        {
            return _context.Concursos.ToList();
        }

        public Concurso Create(Concurso concurso)
        {
            _context.Concursos.Add(concurso);
            _context.SaveChanges();
            return concurso;
        }

        public Concurso Update(Concurso concurso)
        {
            _context.Concursos.Entry(concurso).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return concurso;
        }

        public void DeleteById(int concursoId)
        {
            Concurso concurso = _context.Concursos.Find(concursoId);

            if (concurso == null) throw new NotFoundException("Concurso não encontrado");

            _context.Concursos.Remove(concurso);
            _context.SaveChanges();
        }
    }
}
