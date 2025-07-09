export interface ICandidato {
  id?: string;
  nome: string;
  dataDeNascimento: Date;
  cpf: string;
  profissoes: string[];
  createdAt?: Date;
  updatedAt?: Date;
}

export interface IConcurso {
  id?: string;
  orgao: string;
  edital: string;
  codigo: string;
  vagas: string[];
  createdAt?: Date;
  updatedAt?: Date;
}

export interface IConcursoParaCandidato {
  orgao: string;
  codigo: string;
  edital: string;
}

export interface ICandidatoParaConcurso {
  nome: string;
  dataDeNascimento: Date;
  cpf: string;
}
