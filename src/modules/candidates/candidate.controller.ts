import { Response, Request } from "express";
import CandidateService from "./candidate.service";
import { HttpException } from "../../utils/httpException";

const candidateService = new CandidateService();

export default class CandidateController {
  async getContestsByCPF(req: Request, res: Response): Promise<any> {
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
  }
}
