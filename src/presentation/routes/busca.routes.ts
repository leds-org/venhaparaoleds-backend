import { Router } from 'express';
import { BuscaController } from '../controllers/busca.controller';

export function createBuscaRoutes(buscaController: BuscaController): Router {
  const router = Router();

  /**
   * @route GET /api/candidatos/:cpf/concursos
   * @desc Busca concursos compatíveis com o perfil do candidato
   * @param {string} cpf - CPF do candidato (formato: XXX.XXX.XXX-XX)
   */
  router.get('/candidatos/:cpf/concursos', async (req, res, next) => {
    try {
      await buscaController.buscarConcursosPorCandidato(req, res);
    } catch (error) {
      next(error);
    }
  });

  /**
   * @route GET /api/concursos/:codigo/candidatos
   * @desc Busca candidatos compatíveis com o perfil do concurso
   * @param {string} codigo - Código do concurso (11 dígitos)
   */
  router.get('/concursos/:codigo/candidatos', async (req, res, next) => {
    try {
      await buscaController.buscarCandidatosPorConcurso(req, res);
    } catch (error) {
      next(error);
    }
  });

  /**
   * @route GET /health
   * @desc Health check endpoint
   */
  router.get('/health', async (req, res, next) => {
    try {
      await buscaController.healthCheck(req, res);
    } catch (error) {
      next(error);
    }
  });

  return router;
}
