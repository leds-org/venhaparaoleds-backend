using DesafioLeds.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioLeds.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Concurso> Concursos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=projeto.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Candidato>()
                .Property(c => c.Profissoes)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            modelBuilder.Entity<Concurso>()
                .Property(c => c.Vagas)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        }
    }
}
