// Esse arquivo é um mock do Prisma Client.
const prisma = {
  candidate: {
    findFirst: jest.fn(),
    findMany: jest.fn(),
  },
  contest: {
    findMany: jest.fn(),
    findUnique: jest.fn(), 
  },
};

export default prisma;
