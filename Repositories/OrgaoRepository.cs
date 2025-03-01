using Microsoft.EntityFrameworkCore;
using TrilhaBackendLeds.Context;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;

namespace TrilhaBackendLeds.Repositories
{
    public class OrgaoRepository : IOrgaoRepository
    {
        private readonly TrilhaBackEndLedsContext _context;

        public OrgaoRepository(TrilhaBackEndLedsContext context)
        {
            _context = context;
        }

        public Orgao FindById(int orgaoId)
        {
            return _context.Orgaos.Find(orgaoId);
        }

        public Orgao FindByNome(string nome)
        {
            return _context.Orgaos.FirstOrDefault(orgao => orgao.Nome == nome);
        }

        public Orgao FindOrgaoWithEditais(int orgaoId)
        {
            return _context.Orgaos.Include(o => o.Editais).FirstOrDefault(o => o.Id == orgaoId);
        }

        public IEnumerable<Orgao> FindAll()
        {
            return _context.Orgaos.ToList();
        }

        public Orgao Create(Orgao orgao)
        {
            _context.Orgaos.Add(orgao);
            _context.SaveChanges();
            return orgao;
        }

        public Orgao Update(Orgao orgao)
        {
            _context.Orgaos.Entry(orgao).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return orgao;
        }

        public void DeleteById(int orgaoId)
        {
            Orgao orgao = _context.Orgaos.Find(orgaoId);

            if (orgao == null) throw new NotFoundException("Orgão não encontrado");

            _context.Orgaos.Remove(orgao);
            _context.SaveChanges();
        }
    }
}
