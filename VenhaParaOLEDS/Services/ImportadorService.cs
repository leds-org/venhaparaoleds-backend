// Services/ImportadorService.cs
using VenhaParaOLEDS.Data;
using VenhaParaOLEDS.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage;

namespace VenhaParaOLEDS.Services
{
    public class ImportadorService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ImportadorService> _logger;
        private readonly string _basePath = "/app/dados";

        public ImportadorService(AppDbContext context, ILogger<ImportadorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void ImportarCandidatos()
        {
            var path = Path.Combine(_basePath, "candidatos.txt");

            if (!File.Exists(path))
            {
                _logger.LogWarning("Arquivo candidatos.txt não encontrado em {Path}", path);
                return;
            }

            _logger.LogInformation("Iniciado importação de candidatos...");

            var linhas = File.ReadAllLines(path).Skip(1); // Pula o cabeçalho
            int count = 0;

            foreach (var linha in linhas)
            {
                try
                {
                    var partes = linha.Split(',');
                    var nome = partes[0].Trim();
                    var dataNascimento = DateTime.ParseExact(partes[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var cpf = partes[2].Trim();
                    var profissoes = partes[3].Trim('[', ']', '"').Split(',').Select(p => new Profissao { Nome = p.Trim() }).ToList();

                    var candidato = new Candidato
                    {
                        Nome = nome,
                        DataNascimento = dataNascimento,
                        CPF = cpf,
                        Profissoes = profissoes
                    };

                    _context.Candidatos.Add(candidato);
                    count++;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao importar candidato na linha: {Linha}", linha);
                }

            }
            _context.SaveChanges();
            _logger.LogInformation("{Count} candidatos importados com sucesso.", count);
        }

        public void ImportarConcursos()
        {
            var path = Path.Combine(_basePath, "concursos.txt");

            if (!File.Exists(path))
            {
                _logger.LogWarning("Arquivo concursos.txt não encontrado em {Path}", path);
                return;
            }

            _logger.LogInformation("Iniciando importação de concursos...");

            var linhas = File.ReadAllLines(path).Skip(1); // Pula o cabeçalho
            int count = 0;

            foreach (var linha in linhas)
            {
                try
                {
                    var partes = linha.Split(',');

                    if (partes.Length < 4) continue;

                    var orgao = partes[0].Trim();
                    var edital = partes[1].Trim();
                    var codigo = partes[2].Trim();
                    var vagas = partes[3].Trim('[', ']', '"').Split(',').Select(v => new Vaga { Nome = v.Trim() }).ToList();

                    var concurso = new Concurso
                    {
                        Orgao = orgao,
                        Edital = edital,
                        Codigo = codigo,
                        Vagas = vagas
                    };

                    _context.Concursos.Add(concurso);
                    count++;

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao importar concurso na linha: {Linha}", linha);
                }
            }
            _context.SaveChanges();
            _logger.LogInformation("{Count} concursos importados com sucesso.", count);
        }
    }
}