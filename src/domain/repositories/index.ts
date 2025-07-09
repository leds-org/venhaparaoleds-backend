import { ICandidato, IConcurso, ICandidatoParaConcurso, IConcursoParaCandidato } from '../entities';

export interface ICandidatoRepository {
  findByCpf(cpf: string): Promise<ICandidato | null>;
  findAll(): Promise<ICandidato[]>;
  create(candidato: ICandidato): Promise<ICandidato>;
  createMany(candidatos: ICandidato[]): Promise<ICandidato[]>;
}

export interface IConcursoRepository {
  findByCodigo(codigo: string): Promise<IConcurso | null>;
  findAll(): Promise<IConcurso[]>;
  create(concurso: IConcurso): Promise<IConcurso>;
  createMany(concursos: IConcurso[]): Promise<IConcurso[]>;
}

export interface IBuscaService {
  buscarConcursosPorCandidato(cpf: string): Promise<IConcursoParaCandidato[]>;
  buscarCandidatosPorConcurso(codigo: string): Promise<ICandidatoParaConcurso[]>;
}
