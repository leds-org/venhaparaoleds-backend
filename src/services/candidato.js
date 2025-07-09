const candidatoRepos = require('../repositories/candidato');//Import da camada de repositorio, para acessar aos métodos de interção direta com o banco de dados

function isCorrectCpfFormat(cpf){
    const cpf_regex = new RegExp("^\\d{3}\\.\\d{3}\\.\\d{3}\\-\\d{2}$");
    //Verifica se o cpf fornecido segue os padrões de escrita do cpf
    return cpf_regex.test(cpf)
}

function isCorrectDateFormat(date){
    const date_regex = new RegExp("^(?:(?:31(\\/)(?:0?[13578]|1[02]))\\1|(?:(?:29|30)(\\/)(?:0?[13-9]|1[0-2]))\\2|(?:29(\\/)0?2\\3))\\d{4}$|^(?:0?[1-9]|1\\d|2[0-8])(\\/)(?:0?[1-9]|1[0-2])\\4\\d{4}$");
    //Verifica se a data fornecida segue os padrões de escrita de data dd/dd/aaaa
    return date_regex.test(date);
}

//Verifica se um cpf já está registrado no banco de dados
async function isRegisteredCpf(cpf){return (await candidatoRepos.selectCandidatoByCpf(cpf)).sucess;}

async function newCandidato(data){
    const tempo_inicial = performance.now();//Constante para marcar tempo inicial de execução
    const { nome, cpf, data_nascimento, profissoes } = data;

    //Se algum dos campos não for informado, retorna objeto com erro 400
    if (!nome || !cpf || !data_nascimento || !profissoes) {
        return {
            sucess: false,
            error_message: "Dados do candidato incompletos.",
            status_code: 400,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    };


    //Verificando se cpf já foi registrado
    if(isRegisteredCpf(cpf)){
        return {
            sucess: false,
            error_message: "Cpf já cadastrado.",
            status_code: 400,
            response_time: Math.abs(performance.now() - tempo_inicial)
        }
    }
    

    //Verifica formato do cpf
    if(!isCorrectCpfFormat(cpf)){
        return {
            sucess: false,
            error_message: "Formato inválido de cpf. Favor seguir o formato XXX.XXX.XXX-XX.",
            status_code: 400,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }

    //Verifica formato da data de nascimento
    if(!isCorrectDateFormat(data_nascimento)){
        return {
            sucess: false,
            error_message: "Formato inválido de data. Favor seguir o padrão DD/MM/AAAA.",
            status_code: 400,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }

    const insert_new_candidato = await candidatoRepos.insertNewCandidato(data);

    if(insert_new_candidato.sucess === true){
        return {
            sucess: true,
            status_code: insert_new_candidato.status_code,
            response_time: Math.abs(performance.now() - tempo_inicial),
        }
    }
    else{
        return {
            sucess:false,
            error_message: insert_new_candidato.error_message,
            status_code: insert_new_candidato.status_code,
            response_time: Math.abs(performance.now() - tempo_inicial)
        }
    }


}

async function getCandidato(cpf){
    const tempo_inicial = performance.now();
    if (!cpf) {
        return {
            sucess: false,
            error_message: "Cpf do candidato não informado.",
            status_code: 400,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }

    //Verifica formato do cpf fornecido
    if(!isCorrectCpfFormat(cpf)){
        return {
            sucess: false,
            error_message: "Formato inválido de cpf.",
            status_code: 400,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }

    //Chama método do repositório de candidato
    const select_candidato_by_cpf = await candidatoRepos.selectCandidatoByCpf(cpf);

    if (select_candidato_by_cpf.sucess === true){
        return {
            sucess: true,
            data: select_candidato_by_cpf.data,
            status_code: select_candidato_by_cpf.status_code,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }
    else{
        return {
            sucess: false,
            error_message: select_candidato_by_cpf.error_message,
            status_code: select_candidato_by_cpf.status_code,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }
}

async function getCandidatosCompativeis(codigo_concurso){
    const tempo_inicial = performance.now();

    if (!codigo_concurso) {
        return {
            sucess: false,
            error_message: "Codigo do concurso não informado.",
            status_code: 400,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }

    //Chama método do repósitorio de candidato
    const select_candidatos_compativeis = await candidatoRepos.selectCandidatosCompativeis(codigo_concurso);
    
    if(select_candidatos_compativeis.sucess === true){
        return {
            sucess: true,
            data: select_candidatos_compativeis.data,
            status_code: select_candidatos_compativeis.status_code,           
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }else{
        return {
            sucess: false,
            error_message: select_candidatos_compativeis.error_message,
            status_code: select_candidatos_compativeis.status_code,
            response_time: Math.abs(performance.now() - tempo_inicial)
        };
    }

}


module.exports = {
    newCandidato,
    getCandidato,
    getCandidatosCompativeis,
};