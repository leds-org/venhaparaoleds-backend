const concursoService = require('../services/concurso');//Se conecta com o serviço de concursos

const tempo_inicial = Date.now();//Para ajudar a medir o tempo de resposta da API

// Controller para gerenciar concursos
// Parâmetros: codigo, orgao, edital, vagas
// Retorna: mensagem de sucesso ou erro
async function registrarConcurso(req, res){
    const { codigo, orgao, edital, vagas } = req.body;
    
        const data = {
            codigo,
            orgao,
            edital,
            vagas
        };
    
        const new_concurso = await concursoService.newConcurso(data);
    
        if(new_concurso.sucess === true){
            res.status(200).send(
                {
                    sucess: true,
                    response_time: new_concurso.response_time - tempo_inicial
                }
            )
        }else{
            res.status(get_Concursos.status_code).send(
                {
                    sucess: false,
                    error_message: new_concurso.message,
                    response_time: new_concurso.response_time - tempo_inicial
                }
            );
        }

};

// Função para procurar um concurso já cadastrado
// Parâmetros: id do concurso
// Retorna: dados do concurso ou mensagem de erro
async function procurarConcurso(req, res){
    const { id } = req.query;

    if (!id){
        return res.status(400).send(
            {
                sucess: false,
                error_message: "ID do concurso não fornecido.",
                response_time: Date.now() - tempo_inicial
            }
        )
    }else{
        const get_concurso = await concursoService.getConcurso(id);

        if(get_concurso.sucess === true){
            res.status(200).send(
                {
                    sucess: true,
                    concurso: get_concurso.data,
                    response_time: get_concurso.response_time - tempo_inicial
                }
            );
        } 
        else{
            res.status(get_concurso.status_code).send(
                {
                    sucess: false,
                    error_message: get_concurso.message,
                    response_time: get_concurso.response_time - tempo_inicial
                }
            );
        }

    }
}

// Função para listar os concursos compatíveis a um candidato
// Parâmetros: cpf do candidato
// Retorna: lista de concursos compatíveis ou mensagem de erro
async function listarConcursosCompativeis(req, res){
    const { cpf } = req.query;
    if (!cpf){
        return res.status(400).send(
            {
                sucess: false,
                error_message: "CPF do candidato não fornecido.",
                response_time: Date.now() - tempo_inicial
            }
        )
    }
    else{
        const get_concursos = await concursoService.getConcursosCompativeis (cpf);

        if(get_concursos.sucess === true){
            res.status(200).send(
                {
                    sucess: true,
                    concursos: get_concursos.data,
                    response_time: get_concursos.response_time - tempo_inicial
                }
            );
        } 
        else{
            res.status(get_concursos.status_code).send(
                {
                    sucess: false,
                    error_message: get_concursos.message,
                    response_time: get_concursos.response_time - tempo_inicial
                }
            );
        }
    }
}

module.exports = {
    registrarConcurso,
    procurarConcurso,
    listarConcursosCompativeis,
};