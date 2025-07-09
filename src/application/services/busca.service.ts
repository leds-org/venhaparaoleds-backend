import { IBuscaService, ICandidatoRepository, IConcursoRepository } from '../../domain/repositories';
import { ICandidatoParaConcurso, IConcursoParaCandidato } from '../../domain/entities';
import { NotFoundError } from '../../domain/errors';
import { validateCpf, validateCodigoConcurso } from '../../utils/validators';
import { logger } from '../../utils/logger';

export class BuscaService implements IBuscaService {
  constructor(
    private readonly candidatoRepository: ICandidatoRepository,
    private readonly concursoRepository: IConcursoRepository
  ) {}

  async buscarConcursosPorCandidato(cpf: string): Promise<IConcursoParaCandidato[]> {
    // Validar CPF
    validateCpf(cpf);
    
    logger.info(`Buscando concursos para candidato com CPF: ${cpf}`);

    // Buscar candidato
    const candidato = await this.candidatoRepository.findByCpf(cpf);
    if (!candidato) {
      throw new NotFoundError('Candidato', cpf);
    }

    // Buscar todos os concursos
    const concursos = await this.concursoRepository.findAll();
    
    // Filtrar concursos que têm vagas compatíveis com as profissões do candidato
    const concursosCompativeis = concursos.filter((concurso) => {
      return concurso.vagas.some((vaga) =>
        candidato.profissoes.includes(vaga)
      );
    });

    logger.info(`Encontrados ${concursosCompativeis.length} concursos compatíveis para o candidato`);

    // Mapear para o formato de resposta
    return concursosCompativeis.map((concurso) => ({
      orgao: concurso.orgao,
      codigo: concurso.codigo,
      edital: concurso.edital,
    }));
  }

  async buscarCandidatosPorConcurso(codigo: string): Promise<ICandidatoParaConcurso[]> {
    // Validar código do concurso
    validateCodigoConcurso(codigo);
    
    logger.info(`Buscando candidatos para concurso com código: ${codigo}`);

    // Buscar concurso
    const concurso = await this.concursoRepository.findByCodigo(codigo);
    if (!concurso) {
      throw new NotFoundError('Concurso', codigo);
    }

    // Buscar todos os candidatos
    const candidatos = await this.candidatoRepository.findAll();
    
    // Filtrar candidatos que têm profissões compatíveis com as vagas do concurso
    const candidatosCompativeis = candidatos.filter((candidato) => {
      return candidato.profissoes.some((profissao) =>
        concurso.vagas.includes(profissao)
      );
    });

    logger.info(`Encontrados ${candidatosCompativeis.length} candidatos compatíveis para o concurso`);

    // Mapear para o formato de resposta
    return candidatosCompativeis.map((candidato) => ({
      nome: candidato.nome,
      dataDeNascimento: candidato.dataDeNascimento,
      cpf: candidato.cpf,
    }));
  }
}
