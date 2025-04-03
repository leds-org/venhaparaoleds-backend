const concursoRepos = require('../repositories/concurso');
const crypt = require('../config/criptography');

async function getConcursos(limit, offset, role){
    
    if(limit != null && offset != null && role != null){

        
        const list_concursos = await concursoRepos.listConcursosByRole(limit, offset, role);
            
        if(list_concursos.sucess === true){
            return {
                sucess: true,
                concursos: list_concursos.data,
                status_code: 200
            };
        }else{
            const { error_code } = list_concursos;

            if(error_code === 404){
                return {
                    sucess: false,
                    status_code: list_concursos.error_code,
                    message: list_concursos.message
                };
            } 
            else{
                return {
                    sucess: false,
                    status_code: list_concursos.error_code,
                    message: "Erro de banco de dados"
                };
            }
        } 

    }else{
        return {
            sucess: false,
            message: "Faltam valores a serem preenchidos.",
            status_code: 400
        }
    }
}

module.exports = {
    getConcursos,
};