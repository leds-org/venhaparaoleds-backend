import contestService from "../contest.service";
import prisma from "../../../prismaClient";
import { jest } from "@jest/globals";

// mock do prisma client
jest.mock("../../../__mocks__/prismaClient");

// Descreve o grupo de testes para o serviço de concursos
describe("ContestService", () => {
  let service: contestService;

  beforeEach(() => {
    // Limpa todos os mocks antes de cada teste
    jest.clearAllMocks();
    // Cria uma nova instância do serviço antes de cada teste
    service = new contestService();
  });

  // Teste para verificar se o serviço retorna candidatos corretamente
  it("should return candidates filtered by contest code", async () => {
    // Mock do método findMany do prisma para retornar candidatos
    jest.spyOn(prisma.candidate, "findMany").mockResolvedValue([
      {
        cpf: "123.234.567-89",
        name: "João da Silva",
        birthdate: "08/08/1990",
        professions: JSON.stringify(["carpinteiro"]),
      } as any,
      {
        cpf: "987.654.321-00",
        name: "Maria Oliveira",
        birthdate: "15/05/1985",
        professions: JSON.stringify(["professor"]),
      } as any,
    ]);

    // Mock do método findUnique do prisma para retornar um concurso com profissões
    jest.spyOn(prisma.contest, "findUnique").mockResolvedValue({
      agency: "SEDU",
      publicNotice: "9/2016",
      contestCode: 123456,
      jobTitles: JSON.stringify(["carpinteiro", "marceneiro"]),
    } as any);

    // Chamada ao serviço para obter candidatos filtrados pelo código do concurso
    const candidates = await service.getCandidatesByContestCode(123456);

    // Verifica se o resultado contém apenas o candidato com a profissão correspondente
    expect(candidates).toHaveLength(1);
    expect(candidates[0].cpf).toBe("123.234.567-89");
  });
});
