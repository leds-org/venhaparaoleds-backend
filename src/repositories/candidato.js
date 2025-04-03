const db = require('../config/database');

async function listCandidatosByRole(limit, offset, role){

    try{
        const list_candidatos = await db.query("SELECT * FROM CANDIDATO WHERE profissoes=ANY($1) LIMIT $2 OFFSET $3", [role, limit, offset]);

        if(list_candidatos.rowCount > 0){  
            return {
                sucess: true,
                candidatos: list_candidatos.rows[0]
            }
        }else{
            return {
                sucess: false,
                message: "Candidatos não encontrados"
            }
        }
    }catch(err){
        return {
            sucess: false,
            message: err
        }
    }

};


module.exports = {
    listCandidatosByRole,
};