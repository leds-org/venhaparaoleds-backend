import { HttpException } from "../../utils/httpException";
import prisma from "../../prismaClient";

// Função auxiliar para encontrar um candidato pelo CPF
async function findCandidateByCPF(cpf: string) {
  try {
    // Verifica se o usuário existe no banco de dados pelo CPF
    const existingCandidate = await prisma.candidate.findFirst({
      where: { cpf },
    });

    // Se não encontrar o candidato, lança uma exceção 404
    if (!existingCandidate) {
      throw new HttpException(404, "Candidate not found");
    }

    // Retorna o candidato encontrado
    return existingCandidate;
  } catch (error) {
    // Se o erro for uma exceção personalizada, relança
    if (error instanceof HttpException) {
      throw error;
    }
  }
}

export default class CandidateService {
  /**
   * Método para obter concursos relacionados ao CPF do candidato
   * @param cpf - CPF do candidato
   * @returns Lista de concursos filtrados por profissões relacionadas ao candidato
   */
  async getContestsByCPF(cpf: string) {
    try {
      // Busca o candidato pelo CPF
      const candidate = await findCandidateByCPF(cpf);

      // Garante que o candidato foi encontrado antes de acessar suas propriedades
      if (!candidate) {
        throw new HttpException(404, "Candidate not found");
      }

      // Pega e Converte as profissões do candidato de JSON para array
      // Isso é necessário porque as profissões são armazenadas como uma string JSON no banco de dados pois usei o sqlite e ele não suporta arrays
      const professions = JSON.parse(candidate.professions) as string[];

      // Busca todos os concursos no banco de dados
      const allContests = await prisma.contest.findMany();

      // Filtra os concursos com base nas profissões do candidato e nos títulos de trabalho dos concursos
      const filteredContests = allContests.filter((contest) => {
        // Converte os títulos de trabalho do concurso de JSON para array
        // Isso é necessário porque os títulos de trabalho são armazenados como uma string JSON no banco de dados pois usei o sqlite e ele não suporta arrays
        const jobTitles = JSON.parse(contest.jobTitles) as string[];
        // Verifica se alguma profissão do candidato está presente nos títulos de trabalho do concurso
        // Se sim, inclui o concurso na lista filtrada
        return professions.some((prof) => jobTitles.includes(prof));
      });

      // Mapeia os concursos filtrados para o formato desejado
      // Retorna apenas os campos necessários: agency, publicNotice e contestCode
      return filteredContests.map((contest) => ({
        agency: contest.agency,
        publicNotice: contest.publicNotice,
        // Converte o código do concurso de BigInt para string
        // Isso é necessário porque o código do concurso é armazenado como BigInt no banco de dados e o JSON não suporta BigInt.
        contestCode: contest.contestCode.toString(),
      }));
    } catch (error) {
      // Se o erro for uma exceção personalizada, relança
      if (error instanceof HttpException) {
        throw error;
      }
    }
  }
}
