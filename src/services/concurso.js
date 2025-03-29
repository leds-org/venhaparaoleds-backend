const concursoRepos = require('../repositories/concurso');
const crypt = require('../config/criptography');

async function getConcursos(limit, offset, role){
    
    if(limit != null && offset != null && role != null){

        try{
            const list_concursos = await concursoRepos.listConcursosByRole(limit, offset, role);
            return {
                sucess: true,
                concursos: list_concursos.data
            };

        }catch(err){
            return {
                sucess: false,
                message: err,
                status_code: 400
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