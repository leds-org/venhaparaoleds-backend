using Microsoft.EntityFrameworkCore;
using TrilhaBackendLeds.Context;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;

namespace TrilhaBackendLeds.Repositories
{
    public class EditalRepository : IEditalRepository
    {
        private readonly TrilhaBackEndLedsContext _context;

        public EditalRepository(TrilhaBackEndLedsContext context)
        {
            this._context = context;
        }

        public Edital FindById(int editalId)
        {
            return _context.Editais.Include(e => e.Concurso).Include(e => e.Orgao).FirstOrDefault(e => e.Id == editalId);
        }

        public Edital FindByOrgaoIdAndNumeroDoEdital(int orgaoId, string numeroDoEdital)
        {
            return _context.Editais.Include(e => e.Concurso).Include(e => e.Orgao).FirstOrDefault(edital => edital.OrgaoId == orgaoId && edital.NumeroDoEdital == numeroDoEdital);
        }

        public IEnumerable<Edital> FindAll()
        {
            return _context.Editais.Include(e => e.Concurso).Include(e => e.Orgao).ToList();
        }

        public IEnumerable<Edital> FindOpportunitiesByProfessions(List<int> profissaoIds)
        {
            return _context.Editais
                .Include(e => e.Vagas)
                .ThenInclude(v => v.Profissao)
                .Include(e => e.Concurso)
                .Include(e => e.Orgao)
                .Where(e => e.Vagas.Any(v => profissaoIds.Contains(v.Profissao.Id))).ToList();
        }

        public Edital Create(Edital edital)
        {
            _context.Editais.Add(edital);
            _context.SaveChanges();
            return edital;
        }

        public Edital Update(Edital edital)
        {
            _context.Editais.Entry(edital).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return edital;
        }

        public void DeleteById(int editalId)
        {
            Edital edital = _context.Editais.Find(editalId);

            if (edital == null) throw new NotFoundException("Edital não encontrado");

            _context.Editais.Remove(edital);
            _context.SaveChanges();
        }

    }
}
