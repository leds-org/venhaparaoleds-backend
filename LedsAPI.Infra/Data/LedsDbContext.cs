using LedsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedsAPI.Infra.Data
{
    /*
     * LedsAplicationContext é a classe de contexto do banco de dados para a aplicação.
     * Ela herda de DbContext do Entity Framework Core e é responsável por:
     * 1. Mapear suas classes de entidade para tabelas no banco de dados.
     * 2. Fornecer acesso às coleções de entidades (DbSet).
     * 3. Configurar os relacionamentos entre as entidades.
     */
    public class LedsDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração do DbContext.
        // As opções são geralmente configuradas na inicialização da aplicação (e.g., Program.cs).
        public LedsDbContext(DbContextOptions<LedsDbContext> options) : base(options) { }

        // DbSet para a entidade Candidatos, permitindo operações CRUD na tabela 'Candidatos'.
        public DbSet<Candidato> Candidatos { get; set; }
        // DbSet para a entidade Profissao, permitindo operações CRUD na tabela 'Profissoes'.
        public DbSet<Profissao> Profissoes { get; set; }
        // DbSet para a entidade Concursos, permitindo operações CRUD na tabela 'Concursos'.
        public DbSet<Concurso> Concursos { get; set; }
        // DbSet para a entidade EmpregoVaga, permitindo operações CRUD na tabela 'Vagas'.
        public DbSet<EmpregoVaga> Vagas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidato>().HasKey(c => c.CPF);

            modelBuilder.Entity<Candidato>().HasMany(c => c.Profissoes)
                                            .WithMany()
                                            .UsingEntity(x => x.ToTable("CandidatoProfissao"));

            modelBuilder.Entity<Profissao>().HasIndex(p => p.NomeProf).IsUnique();


            modelBuilder.Entity<EmpregoVaga>().HasKey(c => c.Id);

            modelBuilder.Entity<EmpregoVaga>().HasMany(c => c.ProfissoesNecessarias)
                                            .WithMany()
                                            .UsingEntity(x => x.ToTable("VagaProfissao"));


        }
    }
}