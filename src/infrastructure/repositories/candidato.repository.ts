import { ICandidatoRepository } from '../../domain/repositories';
import { ICandidato } from '../../domain/entities';
import { prisma } from '../database/connection';
import { NotFoundError } from '../../domain/errors';

export class CandidatoRepository implements ICandidatoRepository {
  async findByCpf(cpf: string): Promise<ICandidato | null> {
    const candidato = await prisma.candidato.findUnique({
      where: { cpf },
    });

    if (!candidato) return null;

    return {
      ...candidato,
      profissoes: JSON.parse(candidato.profissoes),
    };
  }

  async findAll(): Promise<ICandidato[]> {
    const candidatos = await prisma.candidato.findMany({
      orderBy: { nome: 'asc' },
    });

    return candidatos.map((candidato: any) => ({
      ...candidato,
      profissoes: JSON.parse(candidato.profissoes),
    }));
  }

  async create(candidato: ICandidato): Promise<ICandidato> {
    const novoCandidato = await prisma.candidato.create({
      data: {
        nome: candidato.nome,
        dataDeNascimento: candidato.dataDeNascimento,
        cpf: candidato.cpf,
        profissoes: JSON.stringify(candidato.profissoes),
      },
    });

    return {
      ...novoCandidato,
      profissoes: JSON.parse(novoCandidato.profissoes),
    };
  }

  async createMany(candidatos: ICandidato[]): Promise<ICandidato[]> {
    const novosCandidatos = await prisma.$transaction(
      candidatos.map((candidato) =>
        prisma.candidato.create({
          data: {
            nome: candidato.nome,
            dataDeNascimento: candidato.dataDeNascimento,
            cpf: candidato.cpf,
            profissoes: JSON.stringify(candidato.profissoes),
          },
        })
      )
    );

    return novosCandidatos.map((candidato: any) => ({
      ...candidato,
      profissoes: JSON.parse(candidato.profissoes),
    }));
  }
}
