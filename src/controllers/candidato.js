const candidatoService = require('../services/candidato');

async function listCandidatos(req, res){

    //A requisição passa valor de limit, offset e o nome de uma profissão
    const { limit, offset, role } = req.body;

    const get_candidatos = await candidatoService.getCandidatos;

    if(get_candidatos.sucess === true){
        res.status(200).send(
            {
                sucess: true,
                candidatos: get_candidatos.candidatos
            }
        )
    }else{
        res.status(get_candidatos.status_code).send(
            {
                sucess: false,
                error_message: get_candidatos.message
            }
        );
    }
};


module.exports = {
    listCandidatos,
};