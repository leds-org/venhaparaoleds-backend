const db = require('../config/database');//Import para conexão com banco de dados
const postgre_errors = require('../config/pgerrors');//Import de objeto que lista alguns erros comuns de banco de dados Postgre
const crypt = require('../config/criptography');//Import de módulo para criptografia e descriptografia de dados
const uuid4 = require('uuid4');//Módulo para geração de uuid
const dotenv = require('dotenv').config();//Para interagir com as variáveis de ambiente da aplicação
const codigo_iv_buffer = Buffer.from(process.env.CODIGO_FIX_SALT, 'hex'); //iv fixo para código do concurso (o valor usado para criação desse iv é guardado em um .env)


const auxiliarRepos = require('./auxiliar');//Repositório auxiliar para operações entre as entidades de candidato e concurso

async function insertNewConcurso(data){
    //Extraindo os parametros do objeto data
    const { orgao, codigo, edital, vagas } = data;

    //Transformando o array de vagas em uma string (para posterior criptografia)
    const vagas_str = JSON.stringify(vagas);


    //criptografando os dados do concurso
    const cript_orgao = crypt.criptInfo(orgao, crypt.generateRandomIV());
    const cript_codigo = crypt.criptInfo(codigo, codigo_iv_buffer);
    const cript_edital = crypt.criptInfo(edital, crypt.generateRandomIV());
    const cript_vagas = crypt.criptInfo(vagas_str, crypt.generateRandomIV());
    
    //gerando os uuids (do registro de concurso e do registro das ivs das infos. do concurso)
    const id = uuid4();
    const id_iv = uuid4();

    //Operação de consulta ao banco de dados. Se em algum momento da operação ocorrer algum erro, o mesmo será retornado em um objeto de resposta
    try {
        //inserindo os ivs dos dados
        const insert_ivs = await db.query("INSERT INTO CONCURSO_IV (id, orgao_iv, edital_iv, vagas_iv) VALUES ($1, $2, $3, $4)", [id_iv, cript_orgao.iv, cript_edital.iv, cript_vagas.iv]);

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

//Função que busca um candidato pelo seu código
async function selectConcursoByCodigo(codigo){
    try{
        const encoded_codigo = crypt.criptInfo(codigo, codigo_iv_buffer);
        // A consulta comparará o codigo fornecido criptografado com os registros no banco de dados
        const select_concurso = await db.query("SELECT * FROM CONCURSO WHERE codigo=$1",[encoded_codigo.encryptedData]);

        //Se for encontrado algum candidato, continua a operação
        if(select_concurso.rowCount > 0){
            
            const id_iv = select_concurso.rows[0].id_iv;
            
            const select_concurso_ivs = await db.query("SELECT * FROM CONCURSO_IV WHERE id=$1",[id_iv]);
            

            //Pegando os valores das duas consultas
            const { orgao, edital, vagas } = select_concurso.rows[0];
            const { orgao_iv, edital_iv, vagas_iv } = select_concurso_ivs.rows[0];

            //Converte de volta para um array de vagas
            let vagas_array = crypt.decriptInfo({iv: vagas_iv, encryptedData: vagas});
            vagas_array = JSON.parse(vagas_array);

            //Descriptografando dados com decriptInfo() e salvando valores em um objeto
            const data = {
                orgao: crypt.decriptInfo({iv: orgao_iv, encryptedData: orgao}),

                codigo: crypt.decriptInfo({iv: encoded_codigo.iv, encryptedData: encoded_codigo.encryptedData}),
                
                edital: crypt.decriptInfo({iv: edital_iv, encryptedData: edital}),

                vagas: vagas_array
            };

            //Retorna objeto com os dados do concurso
            return {
                sucess: true,
                data,
                status_code: 200
            };

        //Se nada for encontrado, retorna objeto com erro 404
        }else{
            return {
                sucess: false,
                error_message: "Concurso nao encontrado.",
                status_code: 404
            }
        }

    }catch(err){
        let message = "";

        if(postgre_errors.hasOwnProperty(err_code)){
            message = postgre_errors[err_code].message;
        }else{
            message = err.code;
        }

        return {
            sucess: false,
            error_message: err_code,
            status_code: 500
        }
    }
}

async function selectConcursosCompativeis(cpf_candidato){
    try {
        //Criando uma lista vazia de concursos compativeis
        let compatibles = [];

        //Buscar pelo candidato
        const candidato = await auxiliarRepos.getProfissoesFromCandidato(cpf_candidato);
    
        if(candidato.sucess !== true){
            return {
                sucess: false,
                error_message: concurso.error_message,
                status_code: concurso.status_code,
            };
        }

        //Pegando o array de profissoes do candidato
        const profissoes = candidato.data;

        //Selecionando todos os concursos
        const select_all_concursos = await db.query("SELECT * FROM CONCURSO");
        
        //Pegando a lista de registros de concursos
        const all_concursos = select_all_concursos.rows;
       
        //Para cada item  na lista de concursos, verifica se há compatibilidade 
        //entre as vagas do concurso e as profissões do candidato
        for(let i = 0; i < all_concursos.length; i++){

            const concurso_ivs = await db.query("SELECT * FROM CONCURSO_IV WHERE id=$1", [all_concursos[i].id_iv]);

            //Descriptografando a string do array de vagas e convertendo ela de novo em array
            let vagas = crypt.decriptInfo({encryptedData: all_concursos[i].vagas, iv: concurso_ivs.rows[0].vagas_iv});

            //Transformando em array de novo
            vagas = JSON.parse(vagas);

            //flag que verifica se ao menos uma das vagas do concurso está inclusa na lista de profissoes do candidato
            let vagas_compativeis = await auxiliarRepos.getListsCommonElements(vagas, profissoes);
            
            //Se houver vagas compatíveis entre as duas listas, 
            //adiciona os dados do concurso na lista de compativeis
            if(vagas_compativeis.length > 0){
                const codigo_iv = codigo_iv_buffer.toString('hex');

                let concurso_data = {
                    orgao: crypt.decriptInfo({ encryptedData: all_concursos[i].orgao, iv: concurso_ivs.rows[0].orgao_iv}),
                    codigo: crypt.decriptInfo({ encryptedData: all_concursos[i].codigo, iv: codigo_iv}),
                    edital: crypt.decriptInfo({ encryptedData: all_concursos[i].edital, iv: concurso_ivs.rows[0].edital_iv}),
                    vagas: crypt.decriptInfo({ encryptedData: all_concursos[i].vagas, iv: concurso_ivs.rows[0].vagas_iv})
                };

                //Transforma a string da lista de vagas em array
                concurso_data.vagas = JSON.parse(concurso_data.vagas);

                compatibles.push(concurso_data);
            }

        }
        //Retornando os concursos compativeis em um json de resposta
        return {
            sucess: true,
            data: compatibles,
            status_code: 200
        };

    //Se durante a operação houver algum erro, será retornado um 
    //objeto de resposta com uma mensagem a depender do tipo de erro ocorrido
    } catch (err) {
        const err_code = err.code;
        let error_message = "Um erro não registrado ocorreu.";

        if(postgre_errors.hasOwnProperty(err_code)){
            error_message = postgre_errors[err_code].message;
        }

        return {
            sucess: false,
            error_message,
            status_code: 500
        };
    }
}

module.exports = {
    insertNewConcurso,
    selectConcursoByCodigo,
    selectConcursosCompativeis
};