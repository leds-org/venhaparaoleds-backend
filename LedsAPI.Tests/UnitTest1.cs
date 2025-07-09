using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Hosting; // <<<<<<<< ADICIONE ESTA LINHA PARA WebHostDefaults

// Certifique-se de que estes using's referenciam suas entidades e DTOs corretos
using LedsAPI.Domain.Entities;
using LedsAPI.Application.DTOs;
using LedsAPI.Infra.Data; // Para o LedsDbContext

namespace LedsAPI.Tests
{
    public class NewApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public NewApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remover qualquer registro existente de DbContextOptions para LedsDbContext
                    // Isso evita que a configuração padrão (de appsettings.json ou Program.cs) interfira nos testes.
                    var descriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<LedsDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Remover o próprio LedsDbContext se estiver registrado diretamente
                    var contextDescriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(LedsDbContext));
                    if (contextDescriptor != null)
                    {
                        services.Remove(contextDescriptor);
                    }

                    // *** Configuração de Banco de Dados SQLite em Memória para Testes ***
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    services.AddDbContext<LedsDbContext>(options =>
                    {
                        options.UseSqlite(connection);
                    });

                    services.AddSingleton(connection);

                    var serviceProvider = services.BuildServiceProvider();
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var dbContext = scopedServices.GetRequiredService<LedsDbContext>();

                        dbContext.Database.EnsureDeleted();
                        dbContext.Database.EnsureCreated();

                        SeedData(dbContext);
                    }
                });

                // *** IMPORTANTE: Configurar o host de teste para desabilitar o redirecionamento HTTPS ***
                // Isso evita o problema de 404 quando o HttpClient de teste faz requisições HTTP
                // e a API espera HTTPS devido ao app.UseHttpsRedirection() no Program.cs.
                builder.UseSetting(WebHostDefaults.HttpsPortKey, ""); // Desabilita a porta HTTPS
                builder.UseSetting(WebHostDefaults.EnvironmentKey, "Development"); // Define o ambiente para "Development" (se o redirecionamento for condicional)

                // Opcional: Para garantir que o WebApplicationFactory use o ambiente de teste
                builder.UseEnvironment("Development");
            });

            _client = _factory.CreateClient(); // Cria um cliente HTTP para interagir com a API de teste.
        }

        private void SeedData(LedsDbContext context)
        {
            context.Candidatos.RemoveRange(context.Candidatos);
            context.Profissoes.RemoveRange(context.Profissoes);
            context.Concursos.RemoveRange(context.Concursos);
            context.Vagas.RemoveRange(context.Vagas);
            context.SaveChanges();

            var profissaoProgramador = new Profissao("Programador");
            context.Profissoes.Add(profissaoProgramador);
            context.SaveChanges();

            var candidatoTeste = new Candidato(
                "123.456.789-00",
                "Fulano de Tal",
                new DateTime(1990, 1, 1),
                new List<Profissao> { profissaoProgramador }
            );
            context.Candidatos.Add(candidatoTeste);
            context.SaveChanges();

            var vagaDev = new EmpregoVaga(
                "Desenvolvedor Backend",
                new List<Concurso>(),
                new List<Profissao> { profissaoProgramador }
            );
            context.Vagas.Add(vagaDev);
            context.SaveChanges();

            var concursoTi = new Concurso(
                101,
                "Receita Federal",
                "Edital 01/2023",
                new List<EmpregoVaga> { vagaDev }
            );
            context.Concursos.Add(concursoTi);
            context.SaveChanges();

            vagaDev.Concursos.Add(concursoTi);
            context.SaveChanges();
        }

        [Fact]
        public async Task GET_ConcursosPorCpf_DeveRetornar200eConcursoCorreto()
        {
            var cpfExistente = "123.456.789-00";
            var response = await _client.GetAsync($"/api/Importacao/concursos-por-cpf?cpf={cpfExistente}");

            // Usa EnsureSuccessStatusCode() para lançar uma exceção se o status não for 2xx
            // Isso nos ajuda a ver outros problemas caso o 404 seja resolvido.
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var concursos = JsonSerializer.Deserialize<List<ConcursoDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(concursos);
            Assert.Single(concursos);
            Assert.Equal("Receita Federal", concursos[0].Orgao);
            Assert.Equal("Edital 01/2023", concursos[0].Edital);
            Assert.Single(concursos[0].Vagas);
            Assert.Equal("Desenvolvedor Backend", concursos[0].Vagas.First().NomeVag);
        }

        [Fact]
        public async Task GET_ConcursosPorCpf_DeveRetornar404ParaCpfInexistente()
        {
            var cpfInexistente = "999.999.999-99";
            var response = await _client.GetAsync($"/api/Importacao/concursos-por-cpf?cpf={cpfInexistente}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GET_CandidatosPorConcurso_DeveRetornar200eCandidatoCorreto()
        {
            var cdConcursoExistente = 101;
            var response = await _client.GetAsync($"/api/Importacao/candidatos-por-concurso?cdConcurso={cdConcursoExistente}");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var candidatos = JsonSerializer.Deserialize<List<CandidatoDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(candidatos);
            Assert.Single(candidatos);
            Assert.Equal("Fulano de Tal", candidatos[0].Nome);
            Assert.Equal("123.456.789-00", candidatos[0].CPF);
        }

        [Fact]
        public async Task GET_CandidatosPorConcurso_DeveRetornar404ParaConcursoInexistente()
        {
            var cdConcursoInexistente = 999;
            var response = await _client.GetAsync($"/api/Importacao/candidatos-por-concurso?cdConcurso={cdConcursoInexistente}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}