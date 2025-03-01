using TrilhaBackendLeds.Context;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Exceptions;
using TrilhaBackendLeds.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TrilhaBackendLeds.Repositories
{
    public class CandidatoRepository : ICandidatoRepository
    {
        private readonly TrilhaBackEndLedsContext _context;

        public CandidatoRepository(TrilhaBackEndLedsContext context)
        {
            _context = context;
        }

        public Candidato FindById(int candidatoId)
        {
            return _context.Candidatos.Include(c => c.CandidatoProfissoes).ThenInclude(cp => cp.Profissao).FirstOrDefault(c => c.Id == candidatoId);
        }

        public Candidato FindByCpf(string cpf)
        {
            return _context.Candidatos.Include(c => c.CandidatoProfissoes).ThenInclude(cp => cp.Profissao).FirstOrDefault(c => c.Cpf == cpf);
        }

        public Candidato FindByCpfLight(string cpf)
        {
            return _context.Candidatos.FirstOrDefault(c => c.Cpf == cpf);
        }

        public IEnumerable<Candidato> FindByProfissoes(List<int> profissoesIds)
        {
                return _context.Candidatos
                .Include(c => c.CandidatoProfissoes)
                .Where(c => c.CandidatoProfissoes.Any(cp => profissoesIds.Contains(cp.ProfissaoId)))
                .ToList();

        }

        public IEnumerable<Candidato> FindAll()
        {
            return _context.Candidatos
                .Include(c => c.CandidatoProfissoes)
                    .ThenInclude(cp => cp.Profissao)
                        .Select(c => new Candidato
                                    {
                                        Id = c.Id,
                                        Nome = c.Nome,
                                        Cpf = c.Cpf,
                                        DataDeNascimento = c.DataDeNascimento,
                                        CandidatoProfissoes = c.CandidatoProfissoes.Select(cp => new CandidatoProfissao
                                        {
                                            CandidatoId = cp.CandidatoId,
                                            ProfissaoId = cp.ProfissaoId,
                                            Profissao = new Profissao
                                            {
                                                Id = cp.Profissao.Id,
                                                Nome = cp.Profissao.Nome
                                            }
                                        }).ToList()
                                    })
                                    .ToList();
        }

        public Candidato Create(Candidato candidato)
        {
            _context.Candidatos.Add(candidato);
            _context.SaveChanges();
            return candidato;
        }

        public Candidato Update(Candidato candidato)
        {
            _context.Candidatos.FirstOrDefault(c => c.Id == candidato.Id);

            _context.Candidatos.Entry(candidato).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return candidato;
        }

        public void DeleteById(int candidatoId)
        {
            Candidato candidato =_context.Candidatos.Find(candidatoId);

            if (candidato == null) throw new NotFoundException("Candidato não encontrado");

            _context.Candidatos.Remove(candidato);
            _context.SaveChanges();
        }

        
    }
}
