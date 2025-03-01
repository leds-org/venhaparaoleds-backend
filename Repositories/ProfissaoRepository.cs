using TrilhaBackendLeds.Context;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Repositories.Interfaces;

namespace TrilhaBackendLeds.Repositories
{
    public class ProfissaoRepository : IProfissaoRepository
    {
        private readonly TrilhaBackEndLedsContext _context;

        public ProfissaoRepository(TrilhaBackEndLedsContext context)
        {
            _context = context;
        }

        public IEnumerable<Profissao> FindAll()
        {
            return _context.Profissoes.ToList();
        }

        public Profissao FindById(int profissaoId)
        {
            return _context.Profissoes.Find(profissaoId);
        }

        public Profissao FindByNome(string nomeDaProfissao)
        {
            return _context.Profissoes.FirstOrDefault(p => p.Nome == nomeDaProfissao);
        }

        public Profissao Create(Profissao profissao)
        {
            _context.Profissoes.Add(profissao);
            _context.SaveChanges();
            return profissao;
        }

        public Profissao Update(Profissao profissao)
        {
            _context.Profissoes.Entry(profissao).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return profissao;
        }

        public void DeleteById(int profissaoId)
        {
            Profissao profissao = _context.Profissoes.Find(profissaoId);
            if (profissao == null) throw new NotFoundException("Profissão não encontrada");
            _context.Profissoes.Remove(profissao);
            _context.SaveChanges();
        }
    }

}
