const fs = require('fs');
const path = require('path');

/**
 * listar profissoes
 * @param {string} listAsString 
 * @returns {string[]} 
 */
const parseList = (listAsString) => {
  if (!listAsString) return [];
  return listAsString
    .replace(/["\[\]]/g, '') // remove "" & []
    .split(',')
    .map(item => item.trim())
    .filter(item => item);
};

/**
 * reads CSV
 * @param {string} fileName
 * @param {function} lineParser
 * @returns {object[]}
 */
const loadData = (fileName, lineParser) => {
  try {
    const filePath = path.join(__dirname, fileName);
    const fileContent = fs.readFileSync(filePath, 'utf-8');

    return fileContent
      .split('\n')
      .slice(1) 
      .filter(line => line.trim() !== '') 
      .map(lineParser);
  } catch (error) {
    console.error(`Erro ao ler ou processar o arquivo ${fileName}:`, error);
    return [];
  }
};

const parseCandidatoLine = (line) => {
  const parts = line.split(/,(?="\[)/); // Divide na vírgula que ANTECEDE "[
  const [nome, dataNascimento, cpf] = parts[0].split(',');
  const profissoes = parseList(parts[1]);
  return { nome, dataNascimento, cpf, profissoes };
};

const parseConcursoLine = (line) => {
  const parts = line.split(/,(?=\[|")/); // remover ,
  const [orgao, edital, codigo] = parts[0].split(',');
  const vagas = parseList(parts[1]);
  return { orgao, edital, codigo, vagas };
};


const loadCandidatos = () => loadData('candidatos.txt', parseCandidatoLine);
const loadConcursos = () => loadData('concursos.txt', parseConcursoLine);

module.exports = { loadCandidatos, loadConcursos };