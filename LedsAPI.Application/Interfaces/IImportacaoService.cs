namespace LedsAPI.Application.Interfaces;

// Interface que define os m�todos para o servi�o de importa��o de dados.
public interface IImportacaoService
{
    // M�todo para importar dados de candidatos a partir de um arquivo.
    Task ImportarArquivos_CandidatosAsync(string ArquivoPath);
    // M�todo para importar dados de concursos a partir de um arquivo.
    Task ImportarArquivos_ConcursosAsync(string ArquivoPath);
}