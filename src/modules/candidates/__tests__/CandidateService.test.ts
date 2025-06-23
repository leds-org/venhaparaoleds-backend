import CandidateService from "../candidate.service";
import prisma from "../../../prismaClient";
import { jest } from '@jest/globals';

// Mock do prisma client
jest.mock("../../../__mocks__/prismaClient"); 

// Descreve o grupo de testes para o serviço de candidatos
describe("CandidateService", () => {
  let service: CandidateService;

  beforeEach(() => {
    // Limpa todos os mocks antes de cada teste
    jest.clearAllMocks();
    // Cria uma nova instância do serviço antes de cada teste
    service = new CandidateService();
  });

  // Teste para verificar se o serviço retorna concursos filtrados por CPF
  it("should return contests filtered by cpf", async () => {
    // Mock do método findFirst do prisma para retornar um candidato
    jest.spyOn(prisma.candidate, "findFirst").mockResolvedValue({
      cpf: "123.234.567-89",
      name: "João da Silva",
      birthdate: '08/08/1990',
      professions: JSON.stringify(["carpinteiro"]),
    } as any); 

    // Mock do método findMany do prisma para retornar concursos
    jest.spyOn(prisma.contest, "findMany").mockResolvedValue([
      {
        agency: "SEDU",
        publicNotice: "9/2016",
        contestCode: BigInt(123456),
        jobTitles: JSON.stringify(["carpinteiro", "marceneiro"]),
      },
      {
        agency: "SEJUS",
        publicNotice: "15/2017",
        contestCode: BigInt(987654),
        jobTitles: JSON.stringify(["professor"]),
      },
    ] as any); 

    // Chamada ao serviço para obter concursos filtrados pelo CPF
    const contests = await service.getContestsByCPF("123.234.567-89");

    // Verifica se o resultado contém apenas o concurso com a profissão correspondente
    expect(contests).toHaveLength(1);
    expect(contests[0].agency).toBe("SEDU");
  });
});

