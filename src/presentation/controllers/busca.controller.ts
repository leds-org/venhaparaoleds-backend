import { Request, Response } from 'express';
import { IBuscaService } from '../../domain/repositories';
import { logger } from '../../utils/logger';

/**
 * Controller para operações de busca
 */
export class BuscaController {
  constructor(private readonly buscaService: IBuscaService) {}

  async buscarConcursosPorCandidato(req: Request, res: Response): Promise<void> {
    try {
      const { cpf } = req.params;

      if (!cpf) {
        res.status(400).json({
          status: 'error',
          message: 'CPF é obrigatório',
        });
        return;
      }

      logger.info(`Request to find concursos for candidato with CPF: ${cpf}`);

      const concursos = await this.buscaService.buscarConcursosPorCandidato(cpf);

      res.status(200).json({
        status: 'success',
        data: {
          candidato: { cpf },
          concursos,
          total: concursos.length,
        },
      });
    } catch (error) {
      logger.error('Error in buscarConcursosPorCandidato:', error);
      throw error;
    }
  }

  async buscarCandidatosPorConcurso(req: Request, res: Response): Promise<void> {
    try {
      const { codigo } = req.params;

      if (!codigo) {
        res.status(400).json({
          status: 'error',
          message: 'Código do concurso é obrigatório',
        });
        return;
      }

      logger.info(`Request to find candidatos for concurso with code: ${codigo}`);

      const candidatos = await this.buscaService.buscarCandidatosPorConcurso(codigo);

      res.status(200).json({
        status: 'success',
        data: {
          concurso: { codigo },
          candidatos,
          total: candidatos.length,
        },
      });
    } catch (error) {
      logger.error('Error in buscarCandidatosPorConcurso:', error);
      throw error;
    }
  }

  async healthCheck(req: Request, res: Response): Promise<void> {
    res.status(200).json({
      status: 'success',
      message: 'API is running',
      timestamp: new Date().toISOString(),
      version: '1.0.0',
    });
  }
}
