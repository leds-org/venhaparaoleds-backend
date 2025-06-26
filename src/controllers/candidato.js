const candidatoService = require('../services/candidato');
const tempo_inicial = Date.now(); // Para ajudar a medir o tempo de resposta da API

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
        res.status(200).send(
            {
                sucess: true,
                response_time: newCandidato.response_time - tempo_inicial
            }
        )
    }else{
        res.status(get_candidatos.status_code).send(
            {
                sucess: false,
                error_message: newCandidato.message,
                response_time: newCandidato.response_time - tempo_inicial
            }
        );
    }
};

//Função para procurar um candidato já cadastrado
//Parâmetros: id do candidato
//Retorna: dados do candidato ou mensagem de erro
async function procurarCandidato(req, res){
  const { id } = req.query;
  
      if (!id){
          return res.status(400).send(
              {
                  sucess: false,
                  error_message: "ID do candidato não fornecido.",
                  response_time: Date.now() - tempo_inicial
              }
          )
      }else{
          const get_candidato = await candidatoService.getCandidato(id);
  
          if(get_candidato.sucess === true){
              res.status(200).send(
                  {
                      sucess: true,
                      candidato: get_candidato.data,
                      response_time: get_candidato.response_time - tempo_inicial
                  }
              );
          } 
          else{
              res.status(get_candidato.status_code).send(
                  {
                      sucess: false,
                      error_message: get_candidato.message,
                      response_time: get_candidato.response_time - tempo_inicial
                  }
              );
          }
  
    }
}

//Função para listar os concursos compatíveis a um candidato
//Parâmetros: lista de profissoes do candidato
//Retorna: lista de candidatos compatíveis ou mensagem de erro
async function listarCandidatosCompativeis(req, res){
    const { codigo } = req.query;
        if (!codigo){
            return res.status(400).send(
                {
                    sucess: false,
                    error_message: "Codigo do concurso nao foi fornecido.",
                    response_time: Date.now() - tempo_inicial
                }
            )
        }
        else{
            const get_candidatos = await candidatoService.getCandidatosCompativeis(codigo);
    
            if(get_candidatos.sucess === true){
                res.status(200).send(
                    {
                        sucess: true,
                        candidato: get_candidatos.data,
                        response_time: get_candidatos.response_time - tempo_inicial
                    }
                );
            } 
            else{
                res.status(get_candidatos.status_code).send(
                    {
                        sucess: false,
                        error_message: get_candidatos.message,
                        response_time: get_candidatos.response_time - tempo_inicial
                    }
                );
            }
        }
}

module.exports = {
    cadastrar,
    procurarCandidato,
    listarCandidatosCompativeis,
};