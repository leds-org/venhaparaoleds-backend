const db = require('../config/database');

async function listConcursosByRole(limit, offset, role){

    try{
        const list_concursos = await db.query("SELECT * FROM CONCURSO WHERE profissoes=ANY($1) LIMIT $2 OFFSET $3", [role, limit, offset]);

        if(list_concursos.rowCount > 0){
            return {
                sucess: true,
                concursos: list_concursos.rows[0]
            }
        }else{
            return {
                sucess: false,
                message: "Nenhum concurso encontrado",
                error_code: 404
            }
        }
        
    }catch(err){
        return {
            sucess: false,
            message: err,
            error_code: 500
        }
    }

};


module.exports = {
    listConcursosByRole,
};