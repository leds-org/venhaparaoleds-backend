import { BuscaService } from '../../application/services/busca.service';
import { ICandidatoRepository, IConcursoRepository } from '../../domain/repositories';
import { ICandidato, IConcurso } from '../../domain/entities';
import { NotFoundError, ValidationError } from '../../domain/errors';

const mockCandidatoRepository: jest.Mocked<ICandidatoRepository> = {
  findByCpf: jest.fn(),
  findAll: jest.fn(),
  create: jest.fn(),
  createMany: jest.fn(),
};

const mockConcursoRepository: jest.Mocked<IConcursoRepository> = {
  findByCodigo: jest.fn(),
  findAll: jest.fn(),
  create: jest.fn(),
  createMany: jest.fn(),
};

describe('BuscaService', () => {
  let buscaService: BuscaService;

  beforeEach(() => {
    buscaService = new BuscaService(mockCandidatoRepository, mockConcursoRepository);
    jest.clearAllMocks();
  });

  describe('buscarConcursosPorCandidato', () => {
    const mockCandidato: ICandidato = {
      id: '1',
      nome: 'João Silva',
      dataDeNascimento: new Date('1990-01-01'),
      cpf: '123.456.789-01',
      profissoes: ['engenheiro civil', 'arquiteto'],
    };

    const mockConcursos: IConcurso[] = [
      {
        id: '1',
        orgao: 'SEDU',
        edital: '1/2023',
        codigo: '12345678901',
        vagas: ['engenheiro civil', 'professor'],
      },
      {
        id: '2',
        orgao: 'SEJUS',
        edital: '2/2023',
        codigo: '12345678902',
        vagas: ['advogado', 'contador'],
      },
    ];

    it('should return matching concursos for valid candidato', async () => {
      mockCandidatoRepository.findByCpf.mockResolvedValue(mockCandidato);
      mockConcursoRepository.findAll.mockResolvedValue(mockConcursos);

      const result = await buscaService.buscarConcursosPorCandidato('123.456.789-01');

      expect(result).toHaveLength(1);
      expect(result[0]).toEqual({
        orgao: 'SEDU',
        codigo: '12345678901',
        edital: '1/2023',
      });
    });

    it('should throw NotFoundError when candidato does not exist', async () => {
      mockCandidatoRepository.findByCpf.mockResolvedValue(null);

      await expect(
        buscaService.buscarConcursosPorCandidato('123.456.789-01')
      ).rejects.toThrow(NotFoundError);
    });

    it('should throw ValidationError for invalid CPF', async () => {
      await expect(
        buscaService.buscarConcursosPorCandidato('invalid-cpf')
      ).rejects.toThrow(ValidationError);
    });
  });

  describe('buscarCandidatosPorConcurso', () => {
    const mockConcurso: IConcurso = {
      id: '1',
      orgao: 'SEDU',
      edital: '1/2023',
      codigo: '12345678901',
      vagas: ['engenheiro civil', 'professor'],
    };

    const mockCandidatos: ICandidato[] = [
      {
        id: '1',
        nome: 'João Silva',
        dataDeNascimento: new Date('1990-01-01'),
        cpf: '123.456.789-01',
        profissoes: ['engenheiro civil', 'arquiteto'],
      },
      {
        id: '2',
        nome: 'Maria Santos',
        dataDeNascimento: new Date('1985-05-15'),
        cpf: '987.654.321-00',
        profissoes: ['advogado', 'contador'],
      },
    ];

    it('should return matching candidatos for valid concurso', async () => {
      mockConcursoRepository.findByCodigo.mockResolvedValue(mockConcurso);
      mockCandidatoRepository.findAll.mockResolvedValue(mockCandidatos);

      const result = await buscaService.buscarCandidatosPorConcurso('12345678901');

      expect(result).toHaveLength(1);
      expect(result[0]).toEqual({
        nome: 'João Silva',
        dataDeNascimento: new Date('1990-01-01'),
        cpf: '123.456.789-01',
      });
    });

    it('should throw NotFoundError when concurso does not exist', async () => {
      mockConcursoRepository.findByCodigo.mockResolvedValue(null);

      await expect(
        buscaService.buscarCandidatosPorConcurso('12345678901')
      ).rejects.toThrow(NotFoundError);
    });

    it('should throw ValidationError for invalid codigo', async () => {
      await expect(
        buscaService.buscarCandidatosPorConcurso('invalid-codigo')
      ).rejects.toThrow(ValidationError);
    });
  });
});
