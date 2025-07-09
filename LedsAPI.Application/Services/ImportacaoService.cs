using LedsAPI.Application.Interfaces;
using LedsAPI.Domain.Entities;
using LedsAPI.Domain.Interfaces.Repositories;

namespace LedsAPI.Application.Services;

public class ImportacaoService : IImportacaoService
{
    private readonly ICandidatoRepository _candidatoRepository;
    private readonly IConcursoRepository _concursoRepository;
    private readonly IProfissaoRepository _profissaoRepository;
    public ImportacaoService(ICandidatoRepository candidatoRepository,
                             IConcursoRepository concursoRepository,
                             IProfissaoRepository profissaoRepository)
    {
        _candidatoRepository = candidatoRepository;
        _concursoRepository = concursoRepository;
        _profissaoRepository = profissaoRepository;
    }
    public async Task ImportarArquivos_CandidatosAsync(string ArquivoPath)
    {
        // Lê todas as linhas do arquivo de forma assíncrona.
        var linhas = await File.ReadAllLinesAsync(ArquivoPath);

        // Percorre cada linha do arquivo, pulando a primeira (que geralmente é o cabeçalho).
        foreach (var linha in linhas.Skip(1))
        {
            // Divide a linha em partes usando o tab (\t) como separador.
            var partes = linha.Split('\t');

            // Se a linha não tiver 4 partes (colunas), ela é inválida e é ignorada.
            if (partes.Length < 4)
                continue; // Pula para a próxima linha.

            // Atribui cada parte da linha a variáveis com nomes mais claros.
            var nome = partes[0];
            // Converte a string de nascimento para um tipo DateTime.
            var nascimento = DateTime.Parse(partes[1]);
            var cpf = partes[2];
            // Remove colchetes extras e separa as profissões por vírgula.
            var profissoesRaw = partes[3].Trim('[', ']');
            var profissoesNomes = profissoesRaw.Split(',').Select(p => p.Trim()).ToList();

            // Para cada nome de profissão lido do arquivo.
            var listaProfissoes = new List<Profissao>();
            foreach (var nomeProf in profissoesNomes)
            {
                // Busca a profissão no banco de dados, normalizando o nome para comparação.
                // Se não encontrar, cria uma nova profissão.
                var prof = await _profissaoRepository.GetByNome(nomeProf.ToLower().Trim());

                // Se a profissão não existir, cria uma nova.
                if (prof == null)
                {
                    prof = new Profissao(nomeProf.Trim().ToLower());
                    // Adiciona a nova profissão ao contexto do banco de dados.
                    await _profissaoRepository.AddAsync(prof);
                }
                // Associa a profissão (existente ou nova) ao candidato.
                listaProfissoes.Add(prof);
            }

            // Procura por um candidato existente no banco de dados com o CPF lido.
            // O .Include(c => c.Profissoes) garante que as profissões do candidato também sejam carregadas.
            var candidato = await _candidatoRepository.GetByCPF(cpf);

            // Se o candidato não for encontrado (ou seja, é um novo candidato).
            if (candidato == null)
            {
                // Cria uma nova instância de Candidato com os dados do arquivo.
                candidato = new Candidato(cpf, nome, nascimento, listaProfissoes);
                // Adiciona o novo candidato ao contexto do banco de dados (ainda não salvo).
                await _candidatoRepository.AddAsync(candidato);

                continue;
            }

            // Se o candidato já existe no banco de dados.
            // Atualiza o nome e a data de nascimento do candidato existente.            
            candidato.Profissoes.Clear();

            candidato.Atualizar(nome, nascimento, listaProfissoes);
            // As alterações serão salvas no banco de dados
            await _candidatoRepository.UpdateAsync(candidato);
        }
    }

    public async Task ImportarArquivos_ConcursosAsync(string ArquivoPath)
    {
        // Lê todas as linhas do arquivo.
        var linhas = await File.ReadAllLinesAsync(ArquivoPath);

        // Percorre cada linha do arquivo, ignorando a primeira (o cabeçalho).
        foreach (var linha in linhas.Skip(1))
        {
            // Divide a linha em partes usando o tab como separador.
            var partes = linha.Split('\t');

            // Verifica se a linha tem o número esperado de partes. Se não, exibe um aviso e pula.
            if (partes.Length < 4)
            {
                Console.WriteLine($"[DEBUG] Linha inválida (faltando partes): {linha}");
                continue;
            }

            // Pega as informações do concurso das partes da linha.
            var orgao = partes[0];
            var edital = partes[1].Trim();

            // Tenta converter o código do concurso para um número. Se falhar, exibe um aviso e pula.
            if (!long.TryParse(partes[2], out long CdConcurso))
            {
                Console.WriteLine($"[DEBUG] Falha ao ler o Código do Concurso '{partes[2]}' na linha: {linha}");
                continue;
            }

            Console.WriteLine($"[DEBUG] Começando a processar o Concurso: {orgao} - {CdConcurso}");

            // Verifica se um concurso com o mesmo código já existe no banco de dados.
            var existingConcurso = await _concursoRepository.GetByCdConcurso(CdConcurso);

            // Se o concurso já existe, exibe um aviso e pula para a próxima linha.
            if (existingConcurso != null)
            {
                Console.WriteLine($"[DEBUG] Concurso com código {CdConcurso} já existe, não será importado novamente.");
                continue;
            }

            // Cria um novo objeto Concurso com as informações lidas.
            var concurso = new Concurso(CdConcurso, orgao, edital, new List<EmpregoVaga>()); // Inicializa a lista de vagas.

            // Pega as profissões necessárias para as vagas do concurso.
            var vagasRaw = partes[3].Trim('[', ']');
            var ProfissoesNecessariassDasVagas = vagasRaw.Split(',')
                .Select(p => p.Trim().ToLower()) // Limpa e coloca em minúsculas.
                .Where(p => !string.IsNullOrWhiteSpace(p)) // Remove entradas vazias.
                .Distinct() // Remove profissões duplicadas.
                .ToList();

            // Para cada profissão necessária lida.
            foreach (var nomeProf in ProfissoesNecessariassDasVagas)
            {
                // Busca a profissão no banco de dados.
                var prof = await _profissaoRepository.GetByNome(nomeProf);
                
                // Se a profissão não existir, cria uma nova.
                if (prof == null)
                {
                    prof = new Profissao(nomeProf);
                    await _profissaoRepository.AddAsync(prof); // Adiciona a nova profissão ao contexto.
                }

                // Cria uma nova vaga de emprego para o concurso.
                var vaga = new EmpregoVaga(nomeProf, null, new List<Profissao> { prof }); // Associa a profissão necessária à vaga.
                
                // Adiciona a vaga ao concurso.
                concurso.Vagas.Add(vaga);
            }

            // Tenta salvar o novo concurso no banco de dados.
            try
            {
                await _concursoRepository.AddAsync(concurso); // Adiciona o concurso ao contexto.
                Console.WriteLine($"[INFO] Concurso {CdConcurso} salvo com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Erro inesperado ao processar ou salvar o concurso {CdConcurso}: {ex.Message}");
            }
        }
    }
}
