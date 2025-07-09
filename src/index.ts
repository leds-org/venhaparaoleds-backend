import express from 'express';
import cors from 'cors';
import helmet from 'helmet';
import rateLimit from 'express-rate-limit';
import dotenv from 'dotenv';

import { createBuscaRoutes } from './presentation/routes/busca.routes';
import { BuscaController } from './presentation/controllers/busca.controller';
import { BuscaService } from './application/services/busca.service';
import { CandidatoRepository } from './infrastructure/repositories/candidato.repository';
import { ConcursoRepository } from './infrastructure/repositories/concurso.repository';
import { connectDatabase } from './infrastructure/database/connection';
import { errorHandler, notFoundHandler, requestLogger } from './presentation/middlewares/error.middleware';
import { logger } from './utils/logger';

// Carregar variáveis de ambiente
dotenv.config();

export function createApp(): express.Application {
  const app = express();

  app.use(helmet());
  app.use(cors());

  const limiter = rateLimit({
    windowMs: parseInt(process.env.RATE_LIMIT_WINDOW_MS || '900000'),
    max: parseInt(process.env.RATE_LIMIT_MAX_REQUESTS || '100'), 
    message: {
      status: 'error',
      message: 'Too many requests, please try again later.',
    },
    standardHeaders: true,
    legacyHeaders: false,
  });
  app.use(limiter);

  app.use(express.json({ limit: '10mb' }));
  app.use(express.urlencoded({ extended: true }));

  app.use(requestLogger);

  const candidatoRepository = new CandidatoRepository();
  const concursoRepository = new ConcursoRepository();
  const buscaService = new BuscaService(candidatoRepository, concursoRepository);
  const buscaController = new BuscaController(buscaService);

  // Rotas
  app.use('/api', createBuscaRoutes(buscaController));

  // Middleware de tratamento de erros
  app.use(notFoundHandler);
  app.use(errorHandler);

  return app;
}

async function startServer(): Promise<void> {
  try {
    // Conectar ao banco de dados
    await connectDatabase();

    // Criar aplicação
    const app = createApp();
    const port = process.env.PORT || 3000;

    // Iniciar servidor
    const server = app.listen(port, () => {
      logger.info(`Server running on port ${port}`);
      logger.info(`Environment: ${process.env.NODE_ENV || 'development'}`);
    });

    // Graceful shutdown
    process.on('SIGTERM', () => {
      logger.info('SIGTERM received, shutting down gracefully');
      server.close(() => {
        logger.info('Process terminated');
        process.exit(0);
      });
    });

    process.on('SIGINT', () => {
      logger.info('SIGINT received, shutting down gracefully');
      server.close(() => {
        logger.info('Process terminated');
        process.exit(0);
      });
    });
  } catch (error) {
    logger.error('Failed to start server:', error);
    process.exit(1);
  }
}

// Iniciar servidor se este arquivo for executado diretamente
if (require.main === module) {
  startServer();
}

export { startServer };
