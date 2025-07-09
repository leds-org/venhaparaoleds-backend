namespace LedsAPI.Application.Interfaces;

// Interface que define os métodos para o serviço de importação de dados.
public interface IImportacaoService
{
    // Método para importar dados de candidatos a partir de um arquivo.
    Task ImportarArquivos_CandidatosAsync(string ArquivoPath);
    // Método para importar dados de concursos a partir de um arquivo.
    Task ImportarArquivos_ConcursosAsync(string ArquivoPath);
}