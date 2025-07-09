using Xunit;
using VenhaParaOLEDS.Services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using System;
using VenhaParaOLEDS.Data;
using VenhaParaOLEDS.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Linq;

public class ImportadorServiceTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void ImportarCandidatos_DeveIgnorarArquivoInexistente()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var logger = new LoggerFactory().CreateLogger<ImportadorService>();
        var service = new ImportadorService(dbContext, logger, "pasta_inexistente");

        // Act
        service.ImportarCandidatos();

        //Assert
        Assert.Empty(dbContext.Candidatos);
    }

    [Fact]
    public void ImportarCandidatos_DeveImportarCandidatosDoArquivo()
    {
        // Arrange
        var logger = new LoggerFactory().CreateLogger<ImportadorService>();
        var dbContext = GetInMemoryDbContext();

        var pastaDados = Path.Combine(Path.GetTempPath(), "dados_teste" + Guid.NewGuid());
        Directory.CreateDirectory(pastaDados);

        var arquivo = Path.Combine(pastaDados, "candidatos.txt");

        File.WriteAllText(arquivo,
            "Nome,Data de Nascimento,CPF,Profissões\n" +
            "João Silva,10/10/1990,123.456.789-00,[pintor, eletricista]");

        var importador = new ImportadorService(dbContext, logger, pastaDados);

        // Act
        importador.ImportarCandidatos();

        // Assert
        var candidatos = dbContext.Candidatos.Include(c => c.Profissoes).ToList();
        Assert.Single(candidatos);
        Assert.Equal("João Silva", candidatos[0].Nome);
        Assert.Equal("123.456.789-00", candidatos[0].CPF);
        Assert.Equal(2, candidatos[0].Profissoes.Count);

        //Cleanup
        try
        {
            if (File.Exists(arquivo))
                File.Delete(arquivo);

            if (Directory.Exists(pastaDados))
                Directory.Delete(pastaDados, recursive: true);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erro ao apagar diretório temporário: {ex.Message}");
        }

    }

    [Fact]
    public void ImportarConcursos_DeveIgnorarArquivoInexistente()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var logger = new LoggerFactory().CreateLogger<ImportadorService>();
        var service = new ImportadorService(dbContext, logger, "pasta_inexistente");

        // Act
        service.ImportarConcursos();

        // Assert
        Assert.Empty(dbContext.Concursos);
        Assert.Empty(dbContext.Vagas);
    }

    [Fact]
    public void ImportarConcursos_DeveImportarConcursosDoArquivo()
    {
        // Arrange
        var logger = new LoggerFactory().CreateLogger<ImportadorService>();
        var dbContext = GetInMemoryDbContext();

        var pastaDados = Path.Combine(Path.GetTempPath(), "dados_teste" + Guid.NewGuid());
        Directory.CreateDirectory(pastaDados);

        var arquivo = Path.Combine(pastaDados, "concursos.txt");

        File.WriteAllText(arquivo,
            "Orgão,Edital,Código do Concurso,Lista de vagas\n" +
            "PMES,17/2016,55312084049,[engenheiro civil,encanador,arquiteto]");

        var importador = new ImportadorService(dbContext, logger, pastaDados);

        // Act
        importador.ImportarConcursos();

        //Assert
        var concursos = dbContext.Concursos.Include(c => c.Vagas).ToList();
        Assert.Single(concursos);

        var concurso = concursos[0];
        Assert.Equal("PMES", concurso.Orgao);
        Assert.Equal("17/2016", concurso.Edital);
        Assert.Equal("55312084049", concurso.Codigo);
        Assert.Equal(3, concurso.Vagas.Count);

        var nomeDasVagas = concurso.Vagas.Select(v => v.Nome).ToList();
        Assert.Contains("engenheiro civil", nomeDasVagas);
        Assert.Contains("encanador", nomeDasVagas);
        Assert.Contains("arquiteto", nomeDasVagas);

        // Cleanup
        try
        {
            if (File.Exists(arquivo))
                File.Delete(arquivo);

            if (Directory.Exists(pastaDados))
                Directory.Delete(pastaDados, recursive: true);
        }
        catch(IOException ex)
        {
            Console.WriteLine($"Erro ao apagar diretório temporário: {ex.Message}");
        }
    }
}