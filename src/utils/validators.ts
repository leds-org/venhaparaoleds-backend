import { ValidationError } from '../domain/errors';

export function validateCpf(cpf: string): void {
  const cpfRegex = /^\d{3}\.\d{3}\.\d{3}-\d{2}$/;
  if (!cpfRegex.test(cpf)) {
    throw new ValidationError('CPF deve estar no formato XXX.XXX.XXX-XX');
  }
}

export function validateCodigoConcurso(codigo: string): void {
  const codigoRegex = /^\d{11}$/;
  if (!codigoRegex.test(codigo)) {
    throw new ValidationError('Código do concurso deve conter exatamente 11 dígitos');
  }
}

export function validateDate(dateString: string): Date {
  const dateRegex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
  const match = dateString.match(dateRegex);
  
  if (!match || match.length < 4) {
    throw new ValidationError('Data deve estar no formato DD/MM/YYYY');
  }

  const day = match[1];
  const month = match[2];
  const year = match[3];
  
  if (!day || !month || !year) {
    throw new ValidationError('Data deve estar no formato DD/MM/YYYY');
  }

  const date = new Date(parseInt(year), parseInt(month) - 1, parseInt(day));
  
  if (
    date.getDate() !== parseInt(day) ||
    date.getMonth() !== parseInt(month) - 1 ||
    date.getFullYear() !== parseInt(year)
  ) {
    throw new ValidationError('Data inválida');
  }

  return date;
}
