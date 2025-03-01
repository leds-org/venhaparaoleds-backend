using Microsoft.EntityFrameworkCore;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Context
{
    public class TrilhaBackEndLedsContext : DbContext
    {
        public TrilhaBackEndLedsContext(DbContextOptions<TrilhaBackEndLedsContext> options) : base(options) 
        {
        }

        public DbSet<Edital> Editais { get; set; }

        public DbSet<Concurso> Concursos { get; set; }

        public DbSet<Orgao> Orgaos { get; set; }

        public DbSet<Vaga> Vagas { get; set; }

        public DbSet<Candidato> Candidatos { get; set; }

        public DbSet<Profissao> Profissoes { get; set; }

        public DbSet<CandidatoProfissao> CandidatoProfissoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidatoProfissao>()
                .HasKey(cp => new { cp.CandidatoId, cp.ProfissaoId });

            modelBuilder.Entity<CandidatoProfissao>()
                .HasOne(cp => cp.Candidato)
                .WithMany(c => c.CandidatoProfissoes)
                .HasForeignKey(cp => cp.CandidatoId);
            modelBuilder.Entity<CandidatoProfissao>()
                .HasOne(cp => cp.Profissao)
                .WithMany(p => p.CandidatoProfissoes)
                .HasForeignKey(cp => cp.ProfissaoId);
        }
    }
}
