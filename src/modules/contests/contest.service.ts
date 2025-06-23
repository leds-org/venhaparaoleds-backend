import prisma from "../../prismaClient";
import { HttpException } from "../../utils/HttpException";

// funcao auxiliar para encontrar um concurso pelo código
async function findContestByCode(code: number) {
  try {
    // Verifica se o concurso existe no banco de dados pelo código
    const contest = await prisma.contest.findUnique({
      where: { contestCode: code },
    });

    // Se não encontrar o concurso, lança uma exceção 404
    if (!contest) {
      throw new HttpException(404, "Contest not found");
    }
    // Retorna o concurso encontrado
    return contest;
  } catch (error) {
    // Se o erro for uma exceção personalizada, relança
    if (error instanceof HttpException) {
      throw error;
    } else {
      // Caso contrário, lança uma exceção 500 com mensagem de erro
      const message =
        error instanceof Error
          ? `Internal error while checking contest: ${error.message}`
          : "Unexpected error occurred while checking contest";
      throw new HttpException(500, message);
    }
  }
}

export default class contestService {
  /**
   * Método para obter candidatos por código de concurso
   * @param code - Código do concurso
   * @returns Lista de candidatos filtrados por profissões relacionadas ao concurso
   */
  async getCandidatesByContestCode(code: number): Promise<any> {
    try {
      // Busca o concurso pelo código
      const contest = await findContestByCode(code);

      // Busca todos os candidatos no banco de dados
      const allCandidates = await prisma.candidate.findMany();

      // Filtra os candidatos com base nas profissões do concurso e nos títulos de trabalho dos candidatos
      const filteredCandidates = allCandidates.filter((candidate) => {
        // Converte as profissões do candidato de JSON para array
        // Isso é necessário porque as profissões são armazenadas como uma string JSON no banco de dados pois usei o sqlite e ele não suporta arrays
        const professions = JSON.parse(candidate.professions) as string[];
        // Converte os títulos de trabalho do concurso de JSON para array
        // Isso é necessário porque os títulos de trabalho são armazenados como uma string JSON no banco de dados pois usei o sqlite e ele não suporta arrays
        const jobTitles = JSON.parse(contest!.jobTitles) as string[];
        // Verifica se alguma profissão do candidato está presente nos títulos de trabalho do concurso
        // Se sim, inclui o candidato na lista filtrada
        return professions.some((prof) => jobTitles.includes(prof));
      });
      // Mapeia os candidatos filtrados para retornar apenas os campos necessários
      // Retorna uma lista de objetos contendo nome, CPF e data de nascimento dos candidatos filtrados
      return filteredCandidates.map((candidate) => ({
        name: candidate.name,
        cpf: candidate.cpf,
        birthdate: candidate.birthdate,
      }));
    } catch (error) {
      // Se o erro for uma exceção personalizada, relança
      if (error instanceof HttpException) {
        throw error;
      }
      // Caso contrário, lança uma exceção 500 com mensagem de erro generica
      throw new HttpException(
        500,
        `Error retrieving candidates for contest: ${
          error instanceof Error ? error.message : "Unknown error"
        }`
      );
    }
  }
}
