const candidatoRepos = require('../repositories/candidato');
const crypt = require('../config/criptography');

async function newCandidato(data){
    const tempo_inicial = Date.now();
    const { nome, cpf, data_nascimento, profissoes } = data;

    if (!nome || !cpf || !data_nascimento || !profissoes) {
        return {
            sucess: false,
            message: "Dados do candidato incompletos.",
            status_code: 400,
            response_time: Date.now() - tempo_inicial
        };
    }

    const insert_new_candidato = await candidatoRepos.insertNewCandidato(data);

    if(insert_new_candidato.sucess === true){
        return {
            sucess: true,
            response_time: Date.now() - tempo_inicial,
        }
    }


}

module.exports = {
    newCandidato,
};