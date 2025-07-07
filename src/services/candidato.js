const candidatoRepos = require('../repositories/candidato');//Import da camada de repositorio, para acessar aos métodos de interção direta com o banco de dados

//Import do módulo de criptografia
const crypt = require('../config/criptography');

async function newCandidato(data){
    const tempo_inicial = performance.now();//Constante para marcar tempo inicial de execução
    const { nome, cpf, data_nascimento, profissoes } = data;

    if (!nome || !cpf || !data_nascimento || !profissoes) {
        return {
            sucess: false,
            message: "Dados do candidato incompletos.",
            status_code: 400,
            response_time: performance.now() - tempo_inicial
        };
    } 

    const insert_new_candidato = await candidatoRepos.insertNewCandidato(data);

    if(insert_new_candidato.sucess === true){
        return {
            sucess: true,
            status_code: insert_new_candidato.status_code,
            response_time: performance.now() - tempo_inicial,
        }
    }
    else{
        return {
            sucess:false,
            message: insert_new_candidato.message,
            status_code: insert_new_candidato.status_code,
            response_time: performance.now() - tempo_inicial
        }
    }


}

async function getCandidato(id){
    const tempo_inicial = performance.now();
    if (!id) {
        return {
            sucess: false,
            message: "Id do candidato não informado.",
            status_code: 400,
            response_time: performance.now() - tempo_inicial
        };
    }

    //Chama método do repositório de candidato
    const select_candidato_by_id = await candidatoRepos.selectCandidatoById(id);

    if (select_candidato_by_id.sucess === true){
        return {
            sucess: true,
            data: select_candidato_by_id.data,
            status_code: select_candidato_by_id.status_code,
            response_time: performance.now() - tempo_inicial
        };
    }
    else{
        return {
            sucess: false,
            message: select_candidato_by_id.message,
            status_code: select_candidato_by_id.status_code,
            response_time: performance.now() - tempo_inicial
        };
    }
}

async function getCandidatosCompativeis(codigo){
    const tempo_inicial = performance.now();

    if (!codigo) {
        return {
            sucess: false,
            message: "Codigo do concurso não informado.",
            status_code: 400,
            response_time: performance.now() - tempo_inicial
        };
    }

    //Chama método do repósitorio de candidato
    const select_candidatos_compativeis = await candidatoRepos.selectCandidatosCompativeis(codigo);
    
    if(select_candidatos_compativeis.sucess === true){
        return {
            sucess: true,
            data: select_candidatos_compativeis.data,
            status_code: select_candidatos_compativeis.status_code,           
            response_time: performance.now() - tempo_inicial
        };
    }else{
        return {
            sucess: false,
            message: select_candidatos_compativeis.message,
            status_code: select_candidatos_compativeis.status_code,
            response_time: performance.now() - tempo_inicial
        };
    }

}


module.exports = {
    newCandidato,
    getCandidato,
    getCandidatosCompativeis,
};