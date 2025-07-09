const concursoRepos = require('../repositories/concurso');//Import da camada de repositorio, para acessar aos métodos de interção direta com o banco de dados


//Função auxiliar para verificar o formato de um edital fornecido
function isCorrectEditalFormat(edital){
    const edital_regex = new RegExp("^\\d{1,2}/\\d{4}$");

    return edital_regex.test(edital);
}


async function newConcurso(data){
    const tempo_inicial = performance.now();//Constante para marcar tempo inicial de execução

    const { codigo, edital, orgao, vagas } = data;

    //Verifica se algum dos valores não foi passado no corpo da requisição
    if (!codigo || !edital || !orgao || !vagas) {
        return {
            sucess: false,
            error_message: "Dados do concurso incompletos.",
            status_code: 400,
            response_time: performance.now() - tempo_inicial
        };
    }

    // //Verifica se o código informado já está registrado no banco de dados
    // if((await concursoRepos.selectConcursoByCodigo(codigo)).sucess === true){
    //     return {
    //         sucess: false,
    //         message: "Codigo já registrado.",
    //         status_code: 400,
    //         response_time: performance.now() - tempo_inicial
    //     }
    // }

    //Verifica se o edital segue um padrão de escrita (XX/XXXX ou X/XXXX)
    if(!isCorrectEditalFormat(edital)){
        return {
            sucess: false,
            error_message: "Formato de edital inválido. Tente seguir o padrão XX/XXXX",
            status_code: 400,
            response_time: performance.now() - tempo_inicial
        }
    }

    //Puxa operação de consulta ao repositório para inserir um novo registro
    const insert_new_concurso = await concursoRepos.insertNewConcurso(data);

    //Se a operação de inserção for bem-sucedida, retorna objeto com resposta de status 200
    if(insert_new_concurso.sucess === true){
        return {
            sucess: true,
            status_code: insert_new_concurso.status_code,
            response_time: performance.now() - tempo_inicial,
        }
    }
    //Se não, retorna objeto com o status de erro fornecido pela resposta do repositório
    else{
        return {
            sucess:false,
            error_message: insert_new_concurso.error_message,
            status_code: insert_new_concurso.status_code,
            response_time: performance.now() - tempo_inicial
        }
    }


}
 
async function getConcurso(codigo){
    //constante tempo_inicial inicializada para ajudar a contar o tempo de execução
    const tempo_inicial = performance.now();

    //Se o código não for informado, retorna objeto com status 400
    if (!codigo) {
        return {
            sucess: false,
            error_message: "Codigo do concurso não informado.",
            status_code: 400,
            response_time: performance.now() - tempo_inicial
        };
    }

    //Chama método do repositório para busca de um concurrso pelo seu código  
    const get_concurso_by_codigo = await concursoRepos.selectConcursoByCodigo(codigo);

    //Se a operação de busca for bem-sucedida, retorna json com os dados desejados
    if (get_concurso_by_codigo.sucess === true){
        return {
            sucess: true,
            data: get_concurso_by_codigo.data,
            status_code: get_concurso_by_codigo.status_code,
            response_time: performance.now() - tempo_inicial
        };
    }
    //Caso contrário, retorna um objeto com um status de erro fornecido pelo retorno do repositório
    else{
        return {
            sucess: false,
            error_message: get_concurso_by_codigo.error_message,
            status_code: get_concurso_by_codigo.status_code,
            response_time: performance.now() - tempo_inicial
        };
    }
}

async function getConcursosCompativeis(cpf_candidato){
    //Assim como nas outras funções, o tempo_inicial servirá para ajudar a calcular o tempo de execução da API.
    const tempo_inicial = performance.now();

    //Se o cpf não for informado, retorna objeto de erro com status 400
    if (!cpf_candidato) {
        return {
            sucess: false,
            error_message: "Cpf do candidato não informado.",
            status_code: 400,
            response_time: performance.now() - tempo_inicial
        };
    }

    //Chama método do repósitorio de concurso para realizar listagem de concursos compatíveis a um candidato dado seu cpf
    const get_concursos_compativeis = await concursoRepos.selectConcursosCompativeis(cpf_candidato);
    
    //Se a operação for bem-sucedida, retorna objeto com 
    //todos os concursos compatíveis ao candidato informado
    if(get_concursos_compativeis.sucess === true){
        return {
            sucess: true,
            data: get_concursos_compativeis.data,
            status_code: get_concursos_compativeis.status_code,
            response_time: performance.now() - tempo_inicial
        };

    //Caso contrário, retorna objeto com mensagem de erro e status fornecido pelo repositório
    }else{
        return {
            sucess: false,
            error_message: get_concursos_compativeis.error_message,
            status_code: get_concursos_compativeis.status_code,
            response_time: performance.now() - tempo_inicial
        };
    }

}


module.exports = {
    newConcurso,
    getConcurso,
    getConcursosCompativeis,
};