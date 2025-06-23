import { Response, Request } from "express";
import CandidateService from "./candidate.service";
import { HttpException } from "../../utils/HttpException";

const candidateService = new CandidateService();

export default class CandidateController {
  async getContestsByCPF(req: Request, res: Response): Promise<any> {
    try {
      // Extrai o CPF dos parâmetros da rota
      const { cpf } = req.params;

      // Verifica se o CPF é válido
      if (!cpf || cpf.length !== 14) {
        throw new HttpException(400, "CPF inválido ou ausente.");
      }

      // Chama o serviço para buscar concursos relacionados ao CPF
      const contests = await candidateService.getContestsByCPF(cpf);

      // Retorna lista de concursos com status 200 OK
      return res.status(200).json(contests);
    } catch (e) {
      // Em caso de erro customizado, retorna o status e mensagem
      if (e instanceof HttpException) {
        return res.status(e.status).json({ message: e.message });
      }

      // Em caso de erro inesperado, retorna status 500 e mensagem genérica
      return res
        .status(500)
        .json({ message: "Erro interno inesperado no servidor." + e });
    }
  }
}
