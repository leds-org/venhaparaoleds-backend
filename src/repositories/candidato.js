const db = require('../config/database');//Importando módulo para conexão com banco de dados
const postgre_errors = require('../config/pgerrors');


//Importa módulo de criptografia e descriptografia
const crypt = require("../config/criptography");

//Importa módulo com função para gerar o uuid
// const create_id = require('uuid4');
const uuid4 = require('uuid4');
 

async function insertNewCandidato(data){
    //Extraindo os parametros do objeto data
    const { nome, cpf, data_nascimento, profissoes } = data;

    //Transformando o array de profissoes em uma string (para posterior criptografia)
    const profissoes_str = JSON.stringify(profissoes);


    //criptografando os dados do candidato
    const cript_nome = crypt.criptInfo(nome);
    const cript_cpf = crypt.criptInfo(cpf);
    const cript_data_nascimento = crypt.criptInfo(data_nascimento);
    const cript_profissoes = crypt.criptInfo(profissoes_str);
    
    //gerando os uuids (do registro de candidato e do registro das ivs das infos. do candidato)
    const id = uuid4();
    const id_iv = uuid4();

    //Operação de consulta ao banco de dados. Se em algum momento da operação ocorrer algum erro, o mesmo será retornado em um objeto de resposta
    try {
        //inserindo os ivs dos dados
        const insert_ivs = await db.query("INSERT INTO CANDIDATO_IV (id, cpf_iv, nome_iv, data_nascimento_iv, profissoes_iv) VALUES ($1, $2, $3, $4, $5)", [id_iv, cript_cpf.iv, cript_nome.iv, cript_data_nascimento.iv, cript_profissoes.iv]);

        //inserindo os dados criptografados
        const insert_encrypted = await db.query("INSERT INTO CANDIDATO (id, id_iv, cpf, nome, data_nascimento, profissoes) VALUES ($1, $2, $3, $4, $5, $6)", [id, id_iv, cript_cpf.encryptedData, cript_nome.encryptedData, cript_data_nascimento.encryptedData, cript_profissoes.encryptedData]);
        
         return {
            sucess: true,
            status_code: 200
         };
    } 
    catch(err) {   
        const err_code = err.code;
        let message = "Falha desconhecida na comunicação com o banco de dados.";

        if(postgre_errors.hasOwnProperty(err_code)){
            message = postgre_errors[err_code].message;
        }

        return {
            sucess: false,
            message,
            status_code: 500
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
            
            // console.log(candidato_crypted.id_iv);
            //Consultando bd para pegar os ivs dos dados do candidato
            const select_candidato_iv = await db.query("SELECT * FROM CANDIDATO_IV WHERE id=$1",[candidato_crypted.id_iv]);

            //Pegando resultado da consulta aos ivs (obrigatoriamente deve ter um resultado, pois sempre que um candidato é inserido, sempre os ivs são inseridos junto)
            const candidato_ivs = select_candidato_iv.rows[0];

            const nome_crypt_data = {
                iv: candidato_ivs.nome_iv, 
                encryptedData: candidato_crypted.nome
            };

            //Descriptografando dados com decriptInfo e salvando valores em um objeto
            const data = {
                nome: crypt.decriptInfo(nome_crypt_data),

                cpf: crypt.decriptInfo({iv: candidato_ivs.cpf_iv, encryptedData: candidato_crypted.cpf}),

                data_nascimento: crypt.decriptInfo({iv: candidato_ivs.data_nascimento_iv, encryptedData: candidato_crypted.data_nascimento}),

                profissoes: crypt.decriptInfo({iv: candidato_ivs.profissoes_iv, encryptedData: candidato_crypted.profissoes})
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
                message: "Candidatos não encontrados",
                status_code: 404
            }
        }
        
    }catch(err){
        const err_code = err.code;
        let message = "Falha desconhecida na comunicação com o banco de dados.";

        if(postgre_errors.hasOwnProperty(err_code)){
            message = postgre_errors[err_code].message;
        }

        return {
            sucess: false,
            message: err,
            status_code: 500
        }
    }
};

async function selectCandidatosCompativeis(){
    
}

module.exports = {
    insertNewCandidato,
    selectCandidatoById,
    selectCandidatosCompativeis,
};