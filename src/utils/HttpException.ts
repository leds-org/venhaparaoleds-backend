// Esse código define uma classe de exceção personalizada para erros HTTP.
// Ela estende a classe Error e adiciona um campo para o status HTTP, permitindo que erros específicos sejam lançados e tratados de forma consistente em toda a aplicação.
export class HttpException extends Error {
  status: number;
  
  constructor(status: number, message: string) {
    super(message);
    this.status = status;
    this.name = "HttpException";
  }
}