using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LedsAPI.Infra.Data;

// Esta classe ajuda as ferramentas do Entity Framework Core (como migrações)
// a criar uma instância do seu contexto de banco de dados (LedsDbContext).
public class LedsDbContextFactory : IDesignTimeDbContextFactory<LedsDbContext>
{
    // Método para criar e configurar o contexto do banco de dados.
    public LedsDbContext CreateDbContext(string[] args)
    {
        // Cria um construtor para as opções do DbContext.
        var optionsBuilder = new DbContextOptionsBuilder<LedsDbContext>();

        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "DataBase", "app.db");
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        var connectionString = $"DataSource={dbPath}";

        // Configura o DbContext para usar MySQL com a string de conexão.
        // Tenta detectar a versão do MySQL automaticamente.
        optionsBuilder.UseSqlite(connectionString);

        // Retorna uma nova instância do LedsDbContext configurada.
        return new LedsDbContext(optionsBuilder.Options);
    }
}