using Microsoft.EntityFrameworkCore;
using TrilhaBackendLeds.Context;
using TrilhaBackendLeds.Middlewares;
using TrilhaBackendLeds.Repositories;
using TrilhaBackendLeds.Repositories.Interfaces;
using TrilhaBackendLeds.Services.Interfaces;
using TrilhaBackendLeds.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TrilhaBackendLeds",
        Description ="API desenvolvida para o desafio da Trilha Backend do LEDS. A API tem como função principal filtrar os concursos que um candidato pode participar."
    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TrilhaBackEndLedsContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddScoped<IProfissaoRepository, ProfissaoRepository>();
builder.Services.AddScoped<IProfissaoService, ProfissaoService>();
builder.Services.AddScoped<ICandidatoRepository, CandidatoRepository>();
builder.Services.AddScoped<ICandidatoService, CandidatoService>();
builder.Services.AddScoped<IConcursoRepository, ConcursoRepository>();
builder.Services.AddScoped<IConcursoService, ConcursoService>();
builder.Services.AddScoped<IOrgaoRepository, OrgaoRepository>();
builder.Services.AddScoped<IOrgaoService, OrgaoService>();
builder.Services.AddScoped<IEditalRepository, EditalRepository>();
builder.Services.AddScoped<IEditalService, EditalService>();
builder.Services.AddScoped<IVagasRepository, VagaRepository>();
builder.Services.AddScoped<IVagasService, VagaService>();
builder.Services.AddScoped<ICandidatoProfissaoRepository, CandidatoProfissaoRepository>();
builder.Services.AddScoped<ICandidatoProfissaoService, CandidatoProfissaoService>();
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<CandidatoService>();
builder.Services.AddScoped<ConcursoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TrilhaBackEndLedsContext>();
    dbContext.Database.Migrate();
}

app.UseMiddleware<GlobalExceptionHandlerMiddlaware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
