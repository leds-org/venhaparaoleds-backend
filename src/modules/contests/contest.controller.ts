import { Response, Request } from "express";
import ContestService from "./contest.service";
import { HttpException } from "../../utils/HttpException";
const contestService = new ContestService();

export default class ContestController {
  // Método para obter candidatos por código de concurso
  async getCandidatesByContestCode(req: Request, res: Response): Promise<any> {
    try {
      // Extrai o código do concurso dos parâmetros da rota
      const { code } = req.params;

      // Verifica se o código é um número válido
      if (!code || isNaN(Number(code))) {
        throw new HttpException(400, "Código do concurso inválido ou ausente.");
      }

      // Chama o serviço para buscar candidatos relacionados ao código do concurso
      // O código é convertido para inteiro antes de ser passado para o serviço
      const candidates = await contestService.getCandidatesByContestCode(
        parseInt(code)
      );
      // Retorna a lista de candidatos com status 200 OK
      return res.status(200).json(candidates);
    } catch (e) {
      // Em caso de erro customizado, retorna o status e mensagem
      if (e instanceof HttpException) {
        return res.status(e.status).json({ message: e.message });
      }
      // Em caso de erro inesperado, retorna status 500 e mensagem genérica
      return res
        .status(500)
        .json({ message: "Erro interno inesperado no servidor." });
    }
  }
}
