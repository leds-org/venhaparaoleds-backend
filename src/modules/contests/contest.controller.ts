import { Response, Request } from "express";
import ContestService from "./contest.service";
import { HttpException } from "../../utils/httpException";
const contestService = new ContestService();

export default class ContestController {
  // Método para obter candidatos por código de concurso
  async getCandidatesByContestCode(req: Request, res: Response): Promise<any> {
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
  }
}
