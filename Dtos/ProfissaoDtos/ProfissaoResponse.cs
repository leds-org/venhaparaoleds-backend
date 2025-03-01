namespace TrilhaBackendLeds.Dtos.ProfissaoDtos;

using TrilhaBackendLeds.Models;

public class ProfissaoResponse
{
    
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public ProfissaoResponse()
    {
    }

    public ProfissaoResponse(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

}
