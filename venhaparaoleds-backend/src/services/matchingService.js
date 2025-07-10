const { loadCandidatos, loadConcursos } = require('../data/database');

const todosOsCandidatos = loadCandidatos();
const todosOsConcursos = loadConcursos();

const candidatosMap = new Map(todosOsCandidatos.map(candidato => [candidato.cpf, candidato]));
const concursosMap = new Map(todosOsConcursos.map(concurso => [concurso.codigo, concurso]));

const findContestsForCandidate = (cpf) => {
  const candidate = candidatosMap.get(cpf);
  if (!candidate) {
    console.warn(`Candidato com CPF ${cpf} não encontrado.`);
    return [];
  }
  const candidateProfessions = new Set(candidate.profissoes);
  const suitableContests = todosOsConcursos.filter(contest =>
    contest.vagas.some(vaga => candidateProfessions.has(vaga))
  );
  return suitableContests.map(({ orgao, codigo, edital }) => ({
    orgao,
    codigo,
    edital,
  }));
};

const findCandidatesForContest = (contestCode) => {
  const contest = concursosMap.get(contestCode);
  if (!contest) {
    console.warn(`Concurso com código ${contestCode} não encontrado.`);
    return [];
  }
  const requiredProfessions = new Set(contest.vagas);
  const suitableCandidates = todosOsCandidatos.filter(candidate =>
    candidate.profissoes.some(profissao => requiredProfessions.has(profissao))
  );
  return suitableCandidates.map(({ nome, dataNascimento, cpf }) => ({
    nome,
    dataNascimento,
    cpf,
  }));
};

module.exports = {
  findContestsForCandidate,
  findCandidatesForContest,
};