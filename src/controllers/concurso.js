const concursoService = require('../services/concurso');

async function listConcursos(req, res){
    //A requisição recebe um valor de limit, offset e uma role para ser usada na filtragem da consulta
    const { limit, offset, role } = req.body;

    //Faz a chamada de método de busca da camada Service
    const get_concursos = await concursoService.getConcursos(limit, offset, role);

    //Se a busca for bem-sucedida, retorna json com a lista dos concursos filtrados por profissão
    if(get_concursos.sucess === true){
        res.status(200).send(
            {
                sucess: true,
                concursos: get_concursos.concursos
            }
        );
    //Se houver algum erro, retorna json com mensagem de erro
    }else{
        res.status(get_concursos.status_code).send(
            {
                sucess: false,
                error_message: get_concursos.message
            }
        );
    }

};


module.exports = {
    listConcursos,
};