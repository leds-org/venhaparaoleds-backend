using LedsAPI.Application.Interfaces;
using LedsAPI.Application.DTOs;
using LedsAPI.Domain.Interfaces.Repositories;
using LedsAPI.Application.Converters;
using LedsAPI.Domain.Entities; // Importante: Adicione esta linha para usar os DTOs

namespace LedsAPI.Application.Services;

// Este é um serviço para fazer consultas (buscas) no banco de dados.
public class ConcursoService : IConcursoService
{
    private readonly IConcursoRepository _concursoRepository;
    private readonly ICandidatoRepository _candidatoRepository;
    private readonly IProfissaoRepository _profissaoRepository;

    // Construtor: prepara o serviço com acesso ao banco de dados.
    public ConcursoService(IConcursoRepository concursoRepository,
                           ICandidatoRepository candidatoRepository,
                           IProfissaoRepository profissaoRepository)
    {
        _concursoRepository = concursoRepository;
        _candidatoRepository = candidatoRepository;
        _profissaoRepository = profissaoRepository;
    }

    public async Task<ConcursoDto> GetAsync(long cdConcurso)
    {
        var concurso = await _concursoRepository.GetByCdConcurso(cdConcurso);
        return concurso == null ? null : MapConverter.MapToDto(concurso);
    }

    public async Task<List<ConcursoDto>> GetAllAsync()
    {
        var concursos = await _concursoRepository.GetAllAsync();
        return concursos?.Select(MapConverter.MapToDto).ToList();
    }

    public async Task InsertAsync(ConcursoDto concursoDto)
    {
        var concurso = await _concursoRepository.GetByCdConcurso(concursoDto.CdConcurso);

        if (concurso != null)
        {
            throw new Exception($"Concurso com código '{concursoDto.CdConcurso}' já existe.");
        }

        // Processa as vagas do concurso.
        var listaEmpregoVaga = new List<EmpregoVaga>();
        foreach (var vagaDto in concursoDto.Vagas)
        {
            var profsNecessarias = new List<Profissao>();
            if (vagaDto.ProfissoesNecessarias != null && vagaDto.ProfissoesNecessarias.Any())
            {
                // Processa as profissões necessárias para cada vaga.
                foreach (var profDto in vagaDto.ProfissoesNecessarias)
                {
                    profDto.NomeProf = profDto.NomeProf.Trim().ToLower();
                    // Busca a profissão existente ou cria uma nova.
                    var profissao = await _profissaoRepository.GetByNome(profDto.NomeProf);

                    if (profissao == null)
                    {
                        profissao = new Profissao(profDto.NomeProf);
                        await _profissaoRepository.AddAsync(profissao);// Adiciona a nova profissão ao contexto.
                    }
                    profsNecessarias.Add(profissao); // Adiciona a profissão à lista de profissões necessárias para esta vaga.
                }
            }

            // Cria a vaga e associa as profissões necessárias.
            listaEmpregoVaga.Add(new EmpregoVaga(vagaDto.NomeVag.Trim().ToLower(), null, profsNecessarias));
        }

        // Cria o novo objeto Concursos.
        concurso = new Concurso(concursoDto.CdConcurso, concursoDto.Orgao, concursoDto.Edital, listaEmpregoVaga);

        await _concursoRepository.AddAsync(concurso);
    }

    /// 
    /// Obt�m uma lista de concursos compat�veis para cada candidato no banco de dados.
    /// 
    /// <returns>Uma lista de CasamentoResultadoDto, onde cada item cont�m um candidato e seus concursos compat�veis.</returns>
    public async Task<List<CasamentoResultadoDto>> ObterConcursosCompativeisAsync()
    {
        // Carrega todos os candidatos do banco de dados, incluindo suas profiss�es relacionadas.
        // O .Include() � usado para o  "carregamento adiantado" dos relacionamentos, evitando problemas de N+1.
        var candidatos = await _candidatoRepository.GetAllAsync();

        // Carrega todos os concursos do banco de dados, incluindo suas vagas e as profiss�es necess�rias para cada vaga.
        // O .ThenInclude() � usado para incluir um relacionamento de um relacionamento.
        var concursos = await _concursoRepository.GetAllAsync();

        // Inicializa uma lista para armazenar os resultados do "casamento" entre candidatos e concursos.
        var resultado = new List<CasamentoResultadoDto>();

        // Itera sobre cada candidato para encontrar concursos compat�veis.
        foreach (var candidato in candidatos)
        {
            // Filtra os concursos que possuem vagas cujas profiss�es necess�rias
            // coincidam com as profiss�es do candidato.
            // A normaliza��o (Trim().ToLower()) � aplicada para garantir compara��es case-insensitive e ignorar espa�os.
            var profissoes = candidato.Profissoes.Select(x => x.NomeProf);
            var concursosCompativeisEntidades = await _concursoRepository.GetByProfissoes(profissoes);

            var candidatoDto = MapConverter.MapToDto(candidato);
            var concursosCompativeisDto = concursosCompativeisEntidades.Select(MapConverter.MapToDto).ToList();

            // Adiciona o resultado do casamento (candidato e seus concursos compat�veis) � lista final.
            resultado.Add(new CasamentoResultadoDto
            {
                Candidato = candidatoDto, // Atribui o DTO do candidato
                ConcursosCompativeis = concursosCompativeisDto // Atribui a lista de DTOs de concursos compat�veis
            });
        }

        // Retorna a lista completa de resultados de casamento.
        // J�ia
        return resultado;
    }

    /// <summary>
    /// Encontra concursos que combinam com as profissões de um candidato.
    /// </summary>
    /// <param name="cpf">O CPF do candidato.</param>
    /// <returns>Uma lista de concursos relacionados.</returns>
    public async Task<List<ConcursoDto>> BuscarConcursosPorCpfAsync(string cpf) // Tipo de retorno alterado para List<ConcursoDto>
    {
        // Busca o candidato pelo CPF e suas profissões.
        var candidato = await _candidatoRepository.GetByCPF(cpf);

        // Se o candidato não for encontrado, retorna uma lista vazia de ConcursoDto.
        if (candidato == null) return new List<ConcursoDto>();

        // Pega as profissões do candidato e as organiza (minúsculas, sem espaços).
        var profissoesCandidato = candidato.Profissoes
            .Select(p => p.NomeProf.Trim().ToLower())
            .ToList();

        var concursosCompativeis = await _concursoRepository.GetByProfissoes(profissoesCandidato);

        return concursosCompativeis.Select(MapConverter.MapToDto).ToList();
    }

    /// <summary>
    /// Encontra candidatos que combinam com as profissões exigidas por um concurso.
    /// </summary>
    /// <param name="cdConcurso">O código do concurso.</param>
    /// <returns>Uma lista de candidatos relacionados.</returns>
    public async Task<List<CandidatoDto>> BuscarCandidatosPorConcursoAsync(long cdConcurso) // Tipo de retorno alterado para List<CandidatoDto>
    {
        // Busca o concurso pelo código, incluindo suas vagas e as profissões necessárias para cada vaga.
        var concurso = await _concursoRepository.GetByCdConcurso(cdConcurso);

        // Se o concurso não for encontrado, retorna uma lista vazia de CandidatoDto.
        if (concurso == null) return new List<CandidatoDto>();

        // Pega todas as profissões exigidas pelas vagas do concurso e as organiza.
        var profissoesRequeridas = concurso.Vagas.SelectMany(v => v.ProfissoesNecessarias // Pega todas as profissões de todas as vagas.
                .Select(p => p.NomeProf.Trim().ToLower()))
            .Distinct() // Remove profissões duplicadas.
            .ToList();

        // Busca candidatos que possuem alguma das profissões exigidas pelo concurso.
        var candidatos = await _candidatoRepository.GetByProfissoes(profissoesRequeridas);

        // Retorna os candidatos encontrados.
        return candidatos.Select(MapConverter.MapToDto).ToList();
    }
}