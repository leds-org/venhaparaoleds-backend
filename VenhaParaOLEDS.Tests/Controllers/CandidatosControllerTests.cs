using Xunit;
using System.Net.Http.Json;
using VenhaParaOLEDS;
using VenhaParaOLEDS.Models;
using VenhaParaOLEDS.Data;
using VenhaParaOLEDS.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace VenhaParaOLEDS.Tests.Controllers
{
    public class CandidatosControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void GetConcursosCompativeis_RetotnarConcursosParaCandidatoExistente()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            var candidato = new Models.Candidato
            {
                CPF = "12345678900",
                Nome = "Maria",
                Profissoes = new List<Models.Profissao>
                {
                    new Models.Profissao {Nome = "engenheiro civil"},
                    new Models.Profissao {Nome = "arquiteto"}
                }

            };
            context.Candidatos.Add(candidato);


            var concurso1 = new Models.Concurso
            {
                Codigo = "ABC123",
                Orgao = "PMES",
                Edital = "17/2016",
                Vagas = new List<Models.Vaga>
                {
                    new Models.Vaga {Nome = "engenheiro civil"},
                    new Models.Vaga {Nome = "encanador"}
                }
            };

            var concurso2 = new Models.Concurso
            {
                Codigo = "XYZ789",
                Orgao = "Outro",
                Edital = "18/2017",
                Vagas = new List<Models.Vaga>
                {
                    new Models.Vaga {Nome = "advogado"}
                }
            };
            context.Concursos.AddRange(concurso1, concurso2);

            context.SaveChanges();

            var controller = new CandidatosController(context);

            //Act
            var result = controller.GetConcursosCompativeis("12345678900");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var concursos = Assert.IsAssignableFrom<IEnumerable<DTOs.ConcursoDto>>(okResult.Value);

            Assert.Single(concursos);
            Assert.Equal("ABC123", concursos.First().Codigo);
        }
         [Fact]
        public void GetConcursosCompativeis_RetornaNotFoundParaCandidatoInexistente()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new CandidatosController(context);

            // Act
            var result = controller.GetConcursosCompativeis("00000000000");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Candidato não encontrado.", notFoundResult.Value);
        }
    }
}
