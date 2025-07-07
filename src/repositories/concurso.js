const db = require('../config/database');//Import para conexão com banco de dados
const postgre_errors = require('../config/pgerrors');//Import de objeto que lista alguns erros comuns de banco de dados Postgre
const crypt = require('../config/criptography');//Import de módulo para criptografia e descriptografia de dados
const uuid4 = require('uuid4');//Módulo para geração de uuid

async function insertNewConcurso(data){
    //Extraindo os parametros do objeto data
    const { orgao, codigo, edital, vagas } = data;

    //Transformando o array de vagas em uma string (para posterior criptografia)
    const vagas_str = JSON.stringify(vagas);


    //criptografando os dados do concurso
    const cript_orgao = crypt.criptInfo(orgao);
    const cript_codigo = crypt.criptInfo(codigo);
    const cript_edital = crypt.criptInfo(edital);
    const cript_vagas = crypt.criptInfo(vagas_str);
    
    //gerando os uuids (do registro de concurso e do registro das ivs das infos. do concurso)
    const id = uuid4();
    const id_iv = uuid4();

    //Operação de consulta ao banco de dados. Se em algum momento da operação ocorrer algum erro, o mesmo será retornado em um objeto de resposta
    try {
        //inserindo os ivs dos dados
        const insert_ivs = await db.query("INSERT INTO CONCURSO_IV (id, codigo_iv, orgao_iv, edital_iv, vagas_iv) VALUES ($1, $2, $3, $4, $5)", [id_iv, cript_codigo.iv, cript_orgao.iv, cript_edital.iv, cript_vagas.iv]);

        //inserindo os dados criptografados
        const insert_encrypted = await db.query("INSERT INTO CONCURSO (id, id_iv, codigo, orgao, edital, vagas) VALUES ($1, $2, $3, $4, $5, $6)", [id, id_iv, cript_codigo.encryptedData, cript_orgao.encryptedData, cript_edital.encryptedData, cript_vagas.encryptedData]);
        
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

async function selectConcursoById(id){
    try {
        //Faz query de busca por concurso
        const select_concurso = await db.query("SELECT * FROM CONCURSO WHERE id=$1;", [id]);

        //Se retornar algum record do banco de dados, faz query de busca pelos ivs do resultado da query anterior
        if(select_concurso.rowCount > 0){
            //Pegando os dados criptografados do concurso
            const concurso_crypted = select_concurso.rows[0];
            
            // console.log(concurso_crypted.id_iv);
            //Consultando bd para pegar os ivs dos dados do concurso
            const select_concurso_iv = await db.query("SELECT * FROM CONCURSO_IV WHERE id=$1",[concurso_crypted.id_iv]);

            //Pegando resultado da consulta aos ivs (obrigatoriamente deve ter um resultado, pois sempre que um concurso é inserido, sempre os ivs são inseridos junto)
            const concurso_ivs = select_concurso_iv.rows[0];

            //Descriptografando dados com decriptInfo() e salvando valores em um objeto
            const data = {
                orgao: crypt.decriptInfo({iv: concurso_ivs.orgao_iv, encryptedData: concurso_crypted.orgao}),

                codigo: crypt.decriptInfo({iv: concurso_ivs.codigo_iv, encryptedData: concurso_crypted.codigo}),

                edital: crypt.decriptInfo({iv: concurso_ivs.edital_iv, encryptedData: concurso_crypted.edital}),

                vagas: crypt.decriptInfo({iv: concurso_ivs.vagas_iv, encryptedData: concurso_crypted.vagas})
            };

            //Retorna objeto com os dados do concurso
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
                message: "Concurso não encontrado",
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
}

async function selectConcursosCompativeis(cpf){


}

module.exports = {
    insertNewConcurso,
    selectConcursoById,
    selectConcursosCompativeis
};