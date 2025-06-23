// Esse código importa o PrismaClient e cria uma instância dele, que pode ser usada para interagir com o banco de dados definido no esquema Prisma. 
import { PrismaClient } from '@prisma/client';

const prisma = new PrismaClient();

export default prisma;
