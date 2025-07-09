import { Request, Response, NextFunction } from 'express';
import { AppError } from '../../domain/errors';
import { logger } from '../../utils/logger';

/**
 * Middleware para tratamento de erros
 */
export function errorHandler(
  error: Error,
  req: Request,
  res: Response,
  next: NextFunction
): void {
  logger.error('Error occurred:', {
    message: error.message,
    stack: error.stack,
    url: req.url,
    method: req.method,
    body: req.body,
    params: req.params,
    query: req.query,
  });

  if (error instanceof AppError) {
    res.status(error.statusCode).json({
      status: 'error',
      message: error.message,
      ...(error.context && { context: error.context }),
    });
    return;
  }

  res.status(500).json({
    status: 'error',
    message: 'Internal server error',
  });
}

/**
 * Middleware para tratamento de rotas não encontradas
 */
export function notFoundHandler(req: Request, res: Response): void {
  res.status(404).json({
    status: 'error',
    message: `Route ${req.method} ${req.path} not found`,
  });
}

/**
 * Middleware de logging de requisições
 */
export function requestLogger(req: Request, res: Response, next: NextFunction): void {
  const start = Date.now();

  res.on('finish', () => {
    const duration = Date.now() - start;
    logger.info('Request completed', {
      method: req.method,
      url: req.url,
      statusCode: res.statusCode,
      duration: `${duration}ms`,
      userAgent: req.get('User-Agent'),
      ip: req.ip,
    });
  });

  next();
}
