const db = require('../config/database');//Importando módulo para conexão com banco de dados
const postgre_errors = require('../config/pgerrors');


//Importa funções de criptografia e descriptografia
import { criptInfo, decriptInfo } from '../config/criptography';

//Importa função para gerar o uuid
import uuid4 from 'uuid4';
 

async function insertNewCandidato(data){
    //Extraindo os parametros do objeto data
    const { nome, cpf, data_nascimento, profissoes } = data;

    //Transformando o array de profissoes em uma string (para posterior criptografia)
    profissoes = JSON.stringify(profissoes);

    try {

        //criptografar os dados
        const c_nome = {
            iv: criptInfo(nome).iv, 
            encryptedData: criptInfo(nome).encryptedData
        };

        const c_cpf = {
            iv: criptInfo(cpf).iv, 
            encryptedData: criptInfo(cpf).encryptedData
        };
        
        const c_data_nascimento = {
            iv: criptInfo(data_nascimento).iv, 
            encryptedData: criptInfo(data_nascimento).encryptedData

        };

        const c_profissoes = {
            iv: criptInfo(profissoes).iv, 
            encryptedData: criptInfo(profissoes).encryptedData
        };

        
        //gerar o uuid
        const id = uuid4();
        const id_iv = uuid4();

        //inserir os ivs dos dados
        const insert_ivs = await db.query("INSERT INTO CANDIDATO_IV (id, cpf_iv, nome_iv, data_nascimento_iv, profissoes_iv) VALUES ($1, $2, $3, $4)", [id_iv, c_cpf.iv, c_nome.iv, c_data_nascimento.iv, c_profissoes.iv]);

        //inserir os dados criptografados
        const insert_encrypted = await db.query("INSERT INTO CANDIDATO (id, id_iv, cpf, nome, data_nascimento, profissoes) VALUES ($1, $2, $3, $4, $5)", [id, id_iv, c_cpf.encryptedData, c_nome.encryptedData, c_data_nascimento.encryptedData, c_profissoes.encryptedData]);
        
         return {
            sucess: true,
            status_code: 200
         };
    } 
    catch(err) {
        const err_code = err.code;
        if(postgre_errors.hasOwnProperty(err_code)){
            return {
                sucess: false,
                message: postgre_errors.err_code.message,
                status_code: postgre_errors.err_code.message 
            };
        }
        else{
            return {
                sucess: false,
                message: "Erro desconhecido no banco de dados ou na aplicacao.",
                status_code: 500
            };
        }
    }  
}

async function selectCandidatoById(id){

    try {
        //Faz query de busca por candidato
        const select_candidato = await db.query("SELECT * FROM CANDIDATO WHERE id=$1;", [id]);

        //Se retornar algum record do banco de dados, faz query de busca pelos ivs do resultado da query anterior
        if(select_candidato.rowCount > 0){

            //Pegando os dados criptografados do candidato
            const candidato_crypted = select_candidato.rows[0];

            //Consultando bd para pegar os ivs dos dados do candidato
            const select_candidato_iv = await db.query("SELECT * FROM CANDIDATO_IV WHERE iv_id=$1",[candidato_crypted.iv_id]);

            //Pegando resultado da consulta aos ivs (obrigatoriamente deve ter um resultado, pois sempre que um candidato é inserido, sempre os ivs são inseridos junto)
            const candidato_ivs = select_candidato_iv.rows[0];
            
            //Descriptografando dados com decriptInfo e salvando valores em um objeto
            const data = {
                nome: decriptInfo({iv: candidato_ivs.nome_iv, encryptedData: candidato_crypted.nome}),

                cpf: decriptInfo({iv: candidato_ivs.cpf_iv, encryptedData: candidato_crypted.cpf}),

                data_nascimento: decriptInfo({iv: candidato_ivs.data_nascimento_iv, encryptedData: candidato_crypted.data_nascimento}),

                profissoes: decriptInfo({iv: candidato_ivs.profissoes_iv, encryptedData: candidato_crypted.profissoes})
            };

            //Retorna objeto com os dados do candidato
            return {
                sucess: true,
                data,
                status_code: 200
            };
        }
        //Se não, retorna resposta de erro 404 (Not found)
        else{
            return {
                sucess: false,
                message: "Candidato nao encontrado.",
                status_code: 404
            };
        }
    } 

    //Se, por algum motivo, der erro nas operações de consulta ao banco de dados, o fluxo desvia para o catch abaixo
    catch(err) {
        //Dado o código do erro gerado (considerando que seja um erro de banco de dados), verifica se código está registrado no pacote pgerrors
        const err_code = err.code;
        if(postgre_errors.hasOwnProperty(err_code)){
            //Retorna objeto com mensagem de erro de acordo com o código gerado
            return {
                sucess: false,
                message: postgre_errors.err_code.message,
                status_code: postgre_errors.err_code.message 
            };
        }
        else{
            //Retorna um objeto com mensagem de erro desconhecido de bd caso o código seja de um erro não registrado 
            return {
                sucess: false,
                message: "Erro desconhecido no banco de dados ou na aplicacao.",
                status_code: 500
            };
        }
    }
}

async function selectCandidatosCompativeis(codigo){
    //...
    
    
    try {

    } 
    catch(err) {
        const err_code = err.code;
        if(postgre_errors.hasOwnProperty(err_code)){
            return {
                sucess: false,
                message: postgre_errors.err_code.message,
                status_code: postgre_errors.err_code.message 
            };
        }
        else{
            return {
                sucess: false,
                message: "Erro desconhecido no banco de dados ou na aplicacao.",
                status_code: 500
            };
        }
    }
}




module.exports = {
    insertNewCandidato,
    selectCandidatoById,
    selectCandidatosCompativeis,
};