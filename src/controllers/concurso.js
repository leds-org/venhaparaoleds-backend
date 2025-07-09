const concursoService = require('../services/concurso');//Se conecta com o serviço de concursos

const tempo_inicial = performance.now();//Para ajudar a medir o tempo de resposta da API

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
            const res_time = ((new_concurso.response_time - tempo_inicial)/1000);
            res.status(200).send(
                {
                    sucess: true,
                    response_time: res_time.toFixed(2)
                }
            )
        }else{
            const res_time = ((new_concurso.response_time - tempo_inicial)/1000);
            res.status(new_concurso.status_code).send(
                {
                    sucess: false,
                    error_message: new_concurso.error_message,
                    response_time: res_time.toFixed(2)
                }
            );
        }

};

// Função para procurar um concurso já cadastrado
// Parâmetros: codigo do concurso
// Retorna: dados do concurso ou mensagem de erro
async function procurarConcurso(req, res){
    const { codigo } = req.query;

    if (!codigo){
        const res_time = ((performance.now() - tempo_inicial)/1000);
        res.status(400).send(
            {
                sucess: false,
                error_message: "Codigo do concurso não fornecido.",
                response_time: res_time.toFixed(2)
            }
        )
    }else{
        const get_concurso = await concursoService.getConcurso(codigo);

        if(get_concurso.sucess === true){
            const res_time = ((get_concurso.response_time - tempo_inicial)/1000);
            res.status(200).send(
                {
                    sucess: true,
                    data: get_concurso.data,
                    response_time: res_time.toFixed(2)
                }
            );
        } 
        else{
            const res_time = ((get_concurso.response_time - tempo_inicial)/1000);
            res.status(get_concurso.status_code).send(
                {
                    sucess: false,
                    error_message: get_concurso.error_message,
                    response_time: res_time.toFixed(2)
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
        const res_time = ((performance.now() - tempo_inicial)/1000);
        return res.status(400).send(
            {
                sucess: false,
                error_message: "CPF do candidato não fornecido.",
                response_time: res_time.toFixed(2)
            }
        )
    }
    else{
        const get_concursos = await concursoService.getConcursosCompativeis(cpf);

        if(get_concursos.sucess === true){
            const res_time = ((get_concursos.response_time - tempo_inicial)/1000);
            res.status(200).send(
                {
                    sucess: true,
                    data: get_concursos.data,
                    response_time: res_time.toFixed(2)
                }
            );
        } 
        else{
            const res_time = ((get_concursos.response_time - tempo_inicial)/1000);
            res.status(get_concursos.status_code).send(
                {
                    sucess: false,
                    error_message: get_concursos.error_message,
                    response_time: res_time.toFixed(2)
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