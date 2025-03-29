const candidatoRepos = require('../repositories/candidato');
const crypt = require('../config/criptography');

async function getCandidatos(limit, offset, role){
    
    if(limit != null && offset != null && role != null){

        try{
            const list_candidatos = await candidatoRepos.listCandidatosByRole(limit, offset, role);
            return {
                sucess: true,
                candidatos: list_candidatos.data
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
    getCandidatos,
};