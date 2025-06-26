const db = require('../config/database');//Importando módulo para conexão com banco de dados

const PostgreErrors = require('../config/pgerrors');

async function insertNewCandidato(data){
    const { nome, cpf, data_nascimento, profissoes } = data;

    try {

    } 
    catch(err) {
        return {

        }
    }
}

module.exports = {
    insertNewCandidato,
};