const concursoRepos = require('../repositories/concurso');//Import da camada de repositorio, para acessar aos métodos de interção direta com o banco de dados

//Import do módulo de criptografia
const crypt = require('../config/criptography');

async function newConcurso(data){
    const tempo_inicial = Date.now();
    const { codigo, edital, orgao, vagas } = data;

    if (!codigo || !edital || !orgao || !vagas) {
        return {
            sucess: false,
            message: "Dados do concurso incompletos.",
            status_code: 400,
            response_time: Date.now() - tempo_inicial
        };
    }

    const insert_new_concurso = await concursoRepos.insertNewconcurso(data);

    if(insert_new_concurso.sucess === true){
        return {
            sucess: true,
            response_time: Date.now() - tempo_inicial,
        }
    }
    else{
        return {
            sucess:false,
            message: insert_new_concurso.message,
            status_code: insert_new_concurso.status_code,
            response_time: Date.now() - tempo_inicial
        }
    }


}

async function getConcurso(id){
    const tempo_inicial = Date.now();
    if (!id) {
        return {
            sucess: false,
            message: "Id do concurso não informado.",
            status_code: 400,
            response_time: Date.now() - tempo_inicial
        };
    }

    //Chama método do repositório de concurso
    const select_concurso_by_id = await concursoRepos.selectconcursoById(id);

    if (select_concurso_by_id.sucess === true){
        return {
            sucess: true,
            data: insert_new_concurso.data,
            response_time: Date.now() - tempo_inicial
        };
    }
    else{
        return {
            sucess: false,
            message: insert_new_concurso.message,
            response_time: Date.now() - tempo_inicial
        };
    }
}

async function getConcursosCompativeis(codigo){
    const tempo_inicial = Date.now();
    if (!codigo) {
        return {
            sucess: false,
            message: "Codigo do concurso não informado.",
            status_code: 400,
            response_time: Date.now() - tempo_inicial
        };
    }

    //Chama método do repósitorio de concurso
    const select_concursos_compativeis = await concursoRepos.selectConcursosCompativeis(codigo);
    
    if(select_concursos_compativeis.sucess === true){
        return {
            sucess: true,
            data: select_concursos_compativeis.data,
            response_time: Date.now() - tempo_inicial
        };
    }else{
        return {
            sucess: false,
            message: select_concursos_compativeis.message,
            response_time: Date.now() - tempo_inicial
        };
    }

}


module.exports = {
    newConcurso,
    getConcurso,
    getConcursosCompativeis,
};