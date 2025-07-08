import { Request, Response, NextFunction } from "express";
import { HttpException } from "../utils/httpException";

// Essa função é um middleware de tratamento de erros para a aplicação Express.
// Ela captura erros lançados em qualquer parte da aplicação e formata a resposta de erro de forma consistente.
export default function errorHandler(
  err: unknown,
  req: Request,
  res: Response,
  _next: NextFunction
) {
  // Verifica se o erro é uma instância de HttpException, que é uma exceção personalizada para erros HTTP.
  const statusCode = err instanceof HttpException ? err.status : 500;
  // Define o código de status HTTP da resposta com base no tipo de erro.
  const message =
    err instanceof HttpException
      ? err.message
      : "Erro interno. Tente novamente mais tarde.";

  // Retorna a resposta JSON com o código de status e a mensagem de erro.
  res.status(statusCode).json({ message });
}
