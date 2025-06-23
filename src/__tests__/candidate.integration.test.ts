import request from "supertest";
import app from "../app";

// Testes de integração para a API de candidatos
// Esses testes verificam se a API retorna os concursos associados a um candidato específico, dado seu CPF.
describe("Candidate API Integration Tests", () => {
  // Teste para obter concursos relacionados ao CPF do candidato
  it("GET /api/candidates/:cpf - should return contests for candidate", async () => {
    const cpf = "182.845.084-34";

    // Faz uma requisição GET para a rota de candidatos com o CPF codificado
    const res = await request(app).get(
      `/api/candidates/${encodeURIComponent(cpf)}`
    );

    // Verifica se o status da resposta é 200 OK e se o corpo da resposta é um array
    expect(res.status).toBe(200);
    expect(Array.isArray(res.body)).toBe(true);

    // Se houver resultados, verifica se cada objeto contém as propriedades esperadas
    if (res.body.length > 0) {
      expect(res.body[0]).toHaveProperty("agency");
      expect(res.body[0]).toHaveProperty("publicNotice");
      expect(res.body[0]).toHaveProperty("contestCode");
    }
  });

  // Teste para verificar se a API retorna 404 quando o candidato não é encontrado
  it("GET /api/candidates/:cpf - should return 404 if candidate not found", async () => {
    const cpf = "000.000.000-00";

    // Faz uma requisição GET para a rota de candidatos com um CPF que não existe
    const res = await request(app).get(
      `/api/candidates/${encodeURIComponent(cpf)}`
    );

    // Verifica se o status da resposta é 404 Not Found e se a mensagem de erro é a esperada
    expect(res.status).toBe(404);
    expect(res.body).toHaveProperty("message", "Candidate not found");
  });
});
