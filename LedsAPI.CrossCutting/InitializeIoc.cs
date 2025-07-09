
using LedsAPI.Application.Interfaces;
using LedsAPI.Application.Services;
using LedsAPI.Domain.Interfaces.Repositories;
using LedsAPI.Infra.Data;
using LedsAPI.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LedsAPI.CrossCutting;

public static class InitializeIoc
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddScoped<IImportacaoService, ImportacaoService>();
        services.AddScoped<ICandidatoService, CandidatoService>();
        services.AddScoped<IConcursoService, ConcursoService>();
        services.AddScoped<IProfissaoService, ProfissaoService>();

        services.AddScoped<ICandidatoRepository, CandidatoRepository>();
        services.AddScoped<IConcursoRepository, ConcursoRepository>();
        services.AddScoped<IProfissaoRepository, ProfissaoRepository>();

        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "DataBase", "app.db");
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        var connectionString = $"DataSource={dbPath}";

        services.AddDbContext<LedsDbContext>(options => options.UseSqlite(connectionString));

        var provider = services.BuildServiceProvider();
        using var context = provider.GetRequiredService<LedsDbContext>();

        // Aplica migrations automaticamente (cria o banco se não existir)
        context.Database.Migrate();

        return services;
    }
}
