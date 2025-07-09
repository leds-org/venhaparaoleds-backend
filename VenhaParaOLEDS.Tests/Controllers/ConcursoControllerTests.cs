using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VenhaParaOLEDS.Controllers;
using VenhaParaOLEDS.Data;
using VenhaParaOLEDS.Models;

namespace VenhaParaOLEDS.Tests.Controllers
{
    public class ConcursosControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void GetCandidatosCompativeis_DeveRetornarCandidatosParaConcursoExistente()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            var concurso = new Concurso
            {
                Codigo = "XYZ123",
                Orgao = "PMES",
                Edital = "10/2024",
                Vagas = new List<Vaga>
                {
                    new Vaga { Nome = "arquiteto" },
                    new Vaga { Nome = "encanador" }
                }
            };
            context.Concursos.Add(concurso);

            var candidato1 = new Candidato
            {
                CPF = "11111111111",
                Nome = "João",
                Profissoes = new List<Profissao>
                {
                    new Profissao { Nome = "arquiteto" }
                }
            };

            var candidato2 = new Candidato
            {
                CPF = "22222222222",
                Nome = "Ana",
                Profissoes = new List<Profissao>
                {
                    new Profissao { Nome = "engenheiro civil" }
                }
            };

            context.Candidatos.AddRange(candidato1, candidato2);
            context.SaveChanges();

            var controller = new ConcursosController(context);

            // Act
            var result = controller.GetCandidatosCompativeis("XYZ123");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var candidatos = Assert.IsAssignableFrom<IEnumerable<DTOs.CandidatoDto>>(okResult.Value);

            Assert.Single(candidatos);
            Assert.Equal("João", candidatos.First().Nome);
        }

        [Fact]
        public void GetCandidatosCompativeis_DeveRetornarNotFoundParaConcursoInexistente()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new ConcursosController(context);

            // Act
            var result = controller.GetCandidatosCompativeis("INEXISTENTE");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Concurso não encontrado", notFoundResult.Value);
        }
    }
}