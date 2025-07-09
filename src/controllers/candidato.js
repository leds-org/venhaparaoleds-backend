const candidatoService = require('../services/candidato');
const tempo_inicial = performance.now(); // Para ajudar a medir o tempo de resposta da API

//Função para cadastrar um novo candidato
//Parâmetros: nome, cpf, data_nascimento, profissoes
//Retorna: mensagem de sucesso ou erro
async function cadastrar(req, res){
    const { nome, cpf, data_nascimento, profissoes } = req.body;

    const data = {
        nome,
        cpf,
        data_nascimento,
        profissoes
    };

    const newCandidato = await candidatoService.newCandidato(data);

    if(newCandidato.sucess === true){
        const res_time = (newCandidato.response_time - tempo_inicial)/1000;

        res.status(200).send(
            {
                sucess: true,
                response_time: res_time.toFixed(2)
            }
        );
    }else{
        const res_time = (newCandidato.response_time - tempo_inicial)/1000;
        
        res.status(newCandidato.status_code).send(
            {
                sucess: false,
                error_message: newCandidato.error_message,
                response_time: res_time.toFixed(2)  
            }
        );
    }
};

//Função para procurar um candidato já cadastrado
//Parâmetros: id do candidato
//Retorna: dados do candidato ou mensagem de erro
async function procurarCandidato(req, res){
    const { cpf } = req.query;
  
    const get_candidato = await candidatoService.getCandidato(cpf);

    if(get_candidato.sucess === true){
        const res_time = (get_candidato.response_time - tempo_inicial)/1000;
        res.status(200).send(
            {
                sucess: true,
                data: get_candidato.data,
                response_time: res_time.toFixed(2)
            }
        );
    } 
    else{
        const res_time = (get_candidato.response_time - tempo_inicial)/1000;
        res.status(get_candidato.status_code).send(
            {
                sucess: false,
                error_message: get_candidato.error_message,
                response_time: res_time.toFixed(2)
            }
        );
    }

}

//Função para listar os candidatos compativeis a um concurso
//Parâmetros: lista de profissoes do candidato
//Retorna: lista de candidatos compatíveis ou mensagem de erro
async function listarCandidatosCompativeis(req, res){
    const { codigo } = req.query;
        
    const get_candidatos = await candidatoService.getCandidatosCompativeis(codigo);

    if(get_candidatos.sucess === true){
        const res_time = (get_candidatos.response_time - tempo_inicial)/1000;
        res.status(200).send(
            {
                sucess: true,
                data: get_candidatos.data,
                response_time: res_time.toFixed(2)
            }
        );
    } 
    else{
        const res_time = (get_candidatos.response_time - tempo_inicial)/1000;
        res.status(get_candidatos.status_code).send(
            {
                sucess: false,
                error_message: get_candidatos.error_message,
                response_time: res_time.toFixed(2)
            }
        );
    }
        
}

module.exports = {
    cadastrar,
    procurarCandidato,
    listarCandidatosCompativeis,
};