/* Esta classe é um controlador ASP.NET Core que lida com o cadastro de entidades
 * como candidatos, vagas e concursos diretamente via API.
 */
using Microsoft.AspNetCore.Mvc;
using LedsAPI.Application.DTOs;
using LedsAPI.Application.Interfaces;

// Indica que esta classe é um controlador de API.
[ApiController]
// Define a rota base para os endpoints deste controlador (ex: /api/Cadastro).
[Route("api/[controller]")]
public class CadastroController : ControllerBase
{
    private readonly ICandidatoService _candidatoService;
    private readonly IConcursoService _concursoService;
    // Construtor: O ASP.NET Core injetará a instância do LedsAplicationContext aqui.
    public CadastroController(ICandidatoService candidatoService,
                              IConcursoService concursoService)
    {
        _candidatoService = candidatoService;
        _concursoService = concursoService;
    }

    // Endpoint POST para cadastrar um novo candidato.
    // Responde a requisições POST para /api/Cadastro/candidato.
    [HttpPost("candidato")]
    public async Task<IActionResult> CadastrarCandidato([FromBody] CandidatoDto dto)
    {
        // Validação inicial dos dados recebidos.
        if (dto == null)
        {
            return BadRequest("Dados inválidos para o candidato");
        }
        if (string.IsNullOrWhiteSpace(dto.CPF))
        {
            return BadRequest("CPF inválido para o candidato.");
        }
        if (string.IsNullOrWhiteSpace(dto.Nome))
        {
            return BadRequest("Nome inválido para o candidato.");
        }
        if (dto.Profissoes == null || !dto.Profissoes.Any() || dto.Profissoes.Any(x => string.IsNullOrWhiteSpace(x.NomeProf)))
        {
            return BadRequest("Profissões inválidas para o candidato.");
        }

        // Normaliza o CPF para garantir consistência.
        dto.CPF = dto.CPF.Trim().Replace(".", "").Replace("-", "");

        // Processa as profissões do DTO.
        foreach (var profDto in dto.Profissoes)
        {
            profDto.NomeProf = profDto.NomeProf.Trim().ToLower();
        }

        await _candidatoService.InsertUpdateAsync(dto);

        // Retorna uma resposta de sucesso (201 Created) com o candidato cadastrado.
        return CreatedAtAction(nameof(CadastrarCandidato), new { cpf = dto.CPF }, dto);
    }

    // Endpoint POST para cadastrar um novo concurso, incluindo suas vagas e profissões necessárias.
    // Responde a requisições POST para /api/Cadastro/concurso.
    [HttpPost("concurso")]
    public async Task<IActionResult> CadastrarConcurso([FromBody] ConcursoDto dto)
    {
        // Validação inicial dos dados do concurso.
        if (dto == null || string.IsNullOrWhiteSpace(dto.Orgao) || string.IsNullOrWhiteSpace(dto.Edital) || dto.Vagas == null || !dto.Vagas.Any())
        {
            return BadRequest("Dados inválidos para o concurso. Órgão, Edital e Vagas são obrigatórios.");
        }
        if (dto.Vagas.Any(x => string.IsNullOrWhiteSpace(x.NomeVag)))
        {
            return BadRequest("O nome da vaga não pode ser vazio.");
        }
        if (dto.Vagas.Any(x => x.ProfissoesNecessarias.Any(p => string.IsNullOrWhiteSpace(p.NomeProf))))
        {
            return BadRequest("O nome da profissão necessária para a vaga não pode ser vazio.");
        }

        // Normaliza o órgão e o edital.
        dto.Orgao = dto.Orgao.Trim();
        dto.Edital = dto.Edital.Trim();

        await _concursoService.InsertAsync(dto);

        // Retorna uma resposta de sucesso (201 Created) com o concurso cadastrado.
        return CreatedAtAction(nameof(CadastrarConcurso), new { cdConcurso = dto.CdConcurso }, dto);
    }
}