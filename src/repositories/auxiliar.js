const db = require('../config/database');//Importando módulo de banco de dados
const crypto = require('../config/criptography');

const buffer_cpf = Buffer.from(process.env.CPF_FIX_SALT, 'hex');
const buffer_codigo = Buffer.from(process.env.CODIGO_FIX_SALT, 'hex');

async function getListsCommonElements(l1, l2){
    let compatible_elements = []

    for (let i = 0; i < l1.length; i++){
        if(l2.includes(l1[i])){
            compatible_elements.push(l1[i]);
        }
    }

    return compatible_elements;
}

async function getProfissoesFromCandidato(cpf) {
    try{
        // console.log("ENTREI NO TRY");
        //Criptografando o cpf
        const encoded_cpf = crypto.criptInfo(cpf, buffer_cpf);
        // console.log("Cpf criptografado:" + encoded_cpf.encryptedData);
        const get_profissoes = await db.query("SELECT C.id_iv, C.profissoes FROM CANDIDATO C WHERE cpf=$1", [encoded_cpf.encryptedData]);

        // console.log("PASSEI PELA PRIMEIRA CONSULTA");

        

        if(get_profissoes.rowCount > 0){
            // console.log("ENTREI NO IF");
            const profissoes = get_profissoes.rows[0].profissoes;
            const id_iv = get_profissoes.rows[0].id_iv;
            
            const get_profissoes_iv = await db.query("SELECT C_IV.profissoes_iv FROM CANDIDATO_IV C_IV WHERE C_IV.id=$1",[id_iv]);
            // console.log("PASSEI PELA SEGUNDA CONSULTA");
            
            const iv_profissoes = get_profissoes_iv.rows[0].profissoes_iv; 
            // console.log("Teste");
            // console.log(iv_profissoes);   
            
            let decoded_profissoes = crypto.decriptInfo({iv: iv_profissoes, encryptedData: profissoes});
            decoded_profissoes = JSON.parse(decoded_profissoes);//Transformando de string para array de novo;
            // console.log(decoded_profissoes);

            return {
                sucess: true,
                data: decoded_profissoes,
                status_code: 200
            };
        }
        else{
            return {
                sucess: false,
                error_message: "Cpf não registrado.",
                status_code: 404
            };
        }
    }
    catch(err){
        const err_code = err.code;
            let error_message = "Um erro não registrado de banco de dados ocorreu.";

            if(postgre_errors.hasOwnProperty(err_code)){
                error_message = postgre_errors[err_code].message;
            }

            return {
                sucess: false,
                error_message,
                status_code: 500
        }
    }
}

async function getVagasFromConcurso(codigo){
    try{
        //Criptografando o codigo
        const encoded_codigo = crypto.criptInfo(codigo, buffer_codigo);
        const get_vagas = await db.query("SELECT C.id_iv, C.vagas FROM CONCURSO C WHERE codigo=$1", [encoded_codigo.encryptedData]);

        if(get_vagas.rowCount > 0){
            const vagas = get_vagas.rows[0].vagas;
            const id_iv = get_vagas.rows[0].id_iv;
            
            const get_vagas_iv = await db.query("SELECT C_IV.vagas_iv FROM CANDIDATO_IV C_IV WHERE C_IV.id=$1",[id_iv]);
            
            const iv_vagas = get_vagas_iv.rows[0].vagas_iv; 

            let decoded_vagas = crypto.decriptInfo({iv: iv_vagas, encryptedData: vagas});
            decoded_vagas = JSON.parse(decoded_vagas);//Transformando de string para array de novo;

            return {
                sucess: true,
                data: decoded_vagas,
                status_code: 200
            };
        }
        else{
            return {
                sucess: false,
                error_message: "Codigo não registrado.",
                status_code: 404
            };
        }
    }
    catch(err){
        const err_code = err.code;
        let error_message = "Um erro não registrado de banco de dados ocorreu.";

        if(postgre_errors.hasOwnProperty(err_code)){
            error_message = postgre_errors[err_code].message;
        }

        return {
            sucess: false,
            error_message,
            status_code: 500
        }
    }
}

module.exports = {
    getProfissoesFromCandidato,
    getVagasFromConcurso,
    getListsCommonElements,
};