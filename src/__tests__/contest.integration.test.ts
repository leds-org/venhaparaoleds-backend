import request from "supertest";
import app from "../app";

// Testes de integração para a API de concursos
// Esses testes verificam se a API retorna os candidatos associados a um concurso específico, dado seu código.
describe("Contest API Integration Tests", () => {
  // Teste para obter candidatos por código de concurso
  it("GET /api/contests/:code - should return candidates for contest", async () => {
    const code = "61828450843";

    // Faz uma requisição GET para a rota de concursos com o código codificado
    const res = await request(app).get(
      `/api/contests/${encodeURIComponent(code)}`
    );

    // Verifica se o status da resposta é 200 OK e se o corpo da resposta é um array
    expect(res.status).toBe(200);
    expect(Array.isArray(res.body)).toBe(true);

    // Se houver resultados, verifica se cada objeto contém as propriedades esperadas
    if (res.body.length > 0) {
      expect(res.body[0]).toHaveProperty("name");
      expect(res.body[0]).toHaveProperty("cpf");
      expect(res.body[0]).toHaveProperty("birthdate");
    }
  });

  // Teste para verificar se a API retorna 404 quando o concurso não é encontrado
  it("GET /api/contests/:cpf - should return 404 if contest not found", async () => {
    const code = "0000000000";

    // Faz uma requisição GET para a rota de concursos com um código que não existe
    const res = await request(app).get(
      `/api/contests/${encodeURIComponent(code)}`
    );

    // Verifica se o status da resposta é 404 Not Found e se a mensagem de erro é a esperada
    expect(res.status).toBe(404);
    expect(res.body).toHaveProperty("message", "Contest not found");
  });
});
