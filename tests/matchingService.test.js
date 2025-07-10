//dados de teste unit:
const mockCandidatos = [
  { nome: 'Ana Sophia Araújo', dataNascimento: '18/12/2004', cpf: '438.150.926-98', profissoes: ['marceneiro', 'carpinteiro', 'eletricista'] },
  { nome: 'Antônio Carvalho', dataNascimento: '19/05/1969', cpf: '380.416.975-93', profissoes: ['engenheiro civil'] },
  { nome: 'Sabrina Farias', dataNascimento: '15/08/1965', cpf: '764.589.123-82', profissoes: ['arquiteto'] },
];

const mockConcursos = [
  { orgao: 'PMES', edital: '17/2016', codigo: '55312084049', vagas: ['engenheiro civil', 'arquiteto'] },
  { orgao: 'SECONT', edital: '16/2015', codigo: '35233564365', vagas: ['pintor', 'arquiteto'] },
  { orgao: 'SEDURB', edital: '16/2019', codigo: '19750532858', vagas: ['engenheiro civil', 'eletricista'] },
  { orgao: 'SECONT', edital: '6/2019', codigo: '61603548745', vagas: ['marceneiro'] },
];

// jeck + mock
jest.mock('../src/data/database', () => ({
  loadCandidatos: jest.fn(() => mockCandidatos),
  loadConcursos: jest.fn(() => mockConcursos),
}));

//importando as funcoes
const { findContestsForCandidate, findCandidatesForContest } = require('../src/services/matchingService');

describe('Matching Service', () => {
  
  describe('findContestsForCandidate (Por CPF)', () => {
    it('deve listar os concursos corretos para um candidato com múltiplas competências', () => {
      const concursos = findContestsForCandidate('438.150.926-98');
      expect(concursos).toHaveLength(2);
      const codigos = concursos.map(c => c.codigo);
      expect(codigos).toEqual(expect.arrayContaining(['19750532858', '61603548745']));
    });

    it('deve listar concursos para um candidato com uma única competência', () => {
      const concursos = findContestsForCandidate('380.416.975-93');
      expect(concursos).toHaveLength(2);
      const codigos = concursos.map(c => c.codigo);
      expect(codigos).toEqual(expect.arrayContaining(['55312084049', '19750532858']));
    });

    it('deve retornar uma lista vazia para um CPF não cadastrado', () => {
      const concursos = findContestsForCandidate('000.000.000-00');
      expect(concursos).toHaveLength(0);
    });
  });

  describe('findCandidatesForContest (Por Código do Concurso)', () => {
    it('deve listar os candidatos corretos para um concurso que exige múltiplas vagas', () => {
      const candidatos = findCandidatesForContest('55312084049');
      expect(candidatos).toHaveLength(2);
      const cpfs = candidatos.map(c => c.cpf);
      expect(cpfs).toEqual(expect.arrayContaining(['380.416.975-93', '764.589.123-82']));
    });

    it('deve listar o candidato correto para um concurso com uma única vaga', () => {
      const candidatos = findCandidatesForContest('61603548745');
      expect(candidatos).toHaveLength(1);
      expect(candidatos[0].cpf).toBe('438.150.926-98');
    });

    it('deve retornar uma lista vazia para um código de concurso não existente', () => {
      const candidatos = findCandidatesForContest('00000000000');
      expect(candidatos).toHaveLength(0);
    });
  });
});