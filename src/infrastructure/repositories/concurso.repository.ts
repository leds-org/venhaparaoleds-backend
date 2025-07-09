import { IConcursoRepository } from '../../domain/repositories';
import { IConcurso } from '../../domain/entities';
import { prisma } from '../database/connection';
import { NotFoundError } from '../../domain/errors';

export class ConcursoRepository implements IConcursoRepository {
  async findByCodigo(codigo: string): Promise<IConcurso | null> {
    const concurso = await prisma.concurso.findUnique({
      where: { codigo },
    });

    if (!concurso) return null;

    return {
      ...concurso,
      vagas: JSON.parse(concurso.vagas),
    };
  }

  async findAll(): Promise<IConcurso[]> {
    const concursos = await prisma.concurso.findMany({
      orderBy: { orgao: 'asc' },
    });

    return concursos.map((concurso: any) => ({
      ...concurso,
      vagas: JSON.parse(concurso.vagas),
    }));
  }

  async create(concurso: IConcurso): Promise<IConcurso> {
    const novoConcurso = await prisma.concurso.create({
      data: {
        orgao: concurso.orgao,
        edital: concurso.edital,
        codigo: concurso.codigo,
        vagas: JSON.stringify(concurso.vagas),
      },
    });

    return {
      ...novoConcurso,
      vagas: JSON.parse(novoConcurso.vagas),
    };
  }

  async createMany(concursos: IConcurso[]): Promise<IConcurso[]> {
    const novosConcursos = await prisma.$transaction(
      concursos.map((concurso) =>
        prisma.concurso.create({
          data: {
            orgao: concurso.orgao,
            edital: concurso.edital,
            codigo: concurso.codigo,
            vagas: JSON.stringify(concurso.vagas),
          },
        })
      )
    );

    return novosConcursos.map((concurso: any) => ({
      ...concurso,
      vagas: JSON.parse(concurso.vagas),
    }));
  }
}
