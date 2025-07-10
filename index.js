const {
  findContestsForCandidate,
  findCandidatesForContest,
} = require('./src/services/matchingService');

function main() {
  console.log('Bem-vindo ao Sistema de Matching de Concursos!');
  console.log('================================================\n');

  const cpfCandidato = '940.538.716-20';
  console.log(`Buscando concursos para o candidato com CPF: ${cpfCandidato}`);
  
  const concursosEncontrados = findContestsForCandidate(cpfCandidato);

  if (concursosEncontrados.length > 0) {
    console.log('Concursos compatíveis encontrados:');
    console.table(concursosEncontrados);
  } else {
    console.log('Nenhum concurso compatível encontrado para este candidato.');
  }

  console.log('\n------------------------------------------------\n');

  const codigoConcurso = '55312084049';
  console.log(`Buscando candidatos para o concurso de código: ${codigoConcurso}`);
  
  const candidatosEncontrados = findCandidatesForContest(codigoConcurso);

  if (candidatosEncontrados.length > 0) {
    console.log('Candidatos compatíveis encontrados:');
    console.table(candidatosEncontrados);
  } else {
    console.log('Nenhum candidato compatível encontrado para este concurso.');
  }
}

main();
