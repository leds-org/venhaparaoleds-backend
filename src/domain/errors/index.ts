export abstract class AppError extends Error {
  public abstract readonly statusCode: number;
  public abstract readonly isOperational: boolean;

  constructor(message: string, public readonly context?: Record<string, unknown>) {
    super(message);
    Object.setPrototypeOf(this, new.target.prototype);
    
    const errorConstructor = Error as any;
    if (typeof errorConstructor.captureStackTrace === 'function') {
      errorConstructor.captureStackTrace(this, this.constructor);
    }
  }
}

export class NotFoundError extends AppError {
  public readonly statusCode = 404;
  public readonly isOperational = true;

  constructor(resource: string, identifier?: string) {
    const message = identifier 
      ? `${resource} with identifier '${identifier}' not found`
      : `${resource} not found`;
    super(message);
  }
}

export class ValidationError extends AppError {
  public readonly statusCode = 400;
  public readonly isOperational = true;

  constructor(message: string, public readonly details?: unknown) {
    super(message);
  }
}

export class InternalError extends AppError {
  public readonly statusCode = 500;
  public readonly isOperational = false;

  constructor(message: string = 'Internal server error') {
    super(message);
  }
}
