// Data/AppBdContext.ca
using Microsoft.EntityFrameworkCore;
using VenhaParaOLEDS;
using VenhaParaOLEDS.Models;

namespace VenhaParaOLEDS.Data
{
    public class AppDbContext : DbContext // Herda de DbContext(classe base)
    {
        // Construtor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // Table Candidatos
        public DbSet<Candidato> Candidatos { get; set; }
        // Entidade de relacionamento
        public DbSet<Profissao> Profissoes { get; set; }
        // Table Concursos
        public DbSet<Concurso> Concursos { get; set; } 
        // Entidade de relacionamento
        public DbSet<Vaga> Vagas { get; set; }
    }
}