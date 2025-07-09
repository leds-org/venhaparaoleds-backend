const db = require('../config/database');//Importando módulo para conexão com banco de dados
const postgre_errors = require('../config/pgerrors');
const dotenv = require('dotenv').config();//Módulo para interagir com variáveis de ambiente da aplicação
const cpf_iv_buffer = Buffer.from(process.env.CPF_FIX_SALT, 'hex'); //iv fixo usado para criptografia dos cpfs (o valor original usado na criação desse iv está escondido no .env)

//Importa módulo de criptografia e descriptografia
const crypt = require("../config/criptography");

//Importa módulo com função para gerar o uuid
// const create_id = require('uuid4');
const uuid4 = require('uuid4');

const auxiliarRepos = require('./auxiliar');//Repositório auxiliar para operações entre as entidades de candidato e concurso
 
async function insertNewCandidato(data){
    //Extraindo os parametros do objeto data
    const { nome, cpf, data_nascimento, profissoes } = data;

    //Transformando o array de profissoes em uma string (para posterior criptografia)
    const profissoes_str = JSON.stringify(profissoes);


    //criptografando os dados do candidato
    const cript_nome = crypt.criptInfo(nome, crypt.generateRandomIV());
    const cript_cpf = crypt.criptInfo(cpf, cpf_iv_buffer);
    const cript_data_nascimento = crypt.criptInfo(data_nascimento, crypt.generateRandomIV());
    const cript_profissoes = crypt.criptInfo(profissoes_str, crypt.generateRandomIV());
    
    //gerando os uuids (do registro de candidato e do registro das ivs das infos. do candidato)
    const id = uuid4();
    const id_iv = uuid4();

    //Operação de consulta ao banco de dados. Se em algum momento da operação ocorrer algum erro, o mesmo será retornado em um objeto de resposta
    try {

        //inserindo os ivs dos dados
        const insert_ivs = await db.query("INSERT INTO CANDIDATO_IV (id, nome_iv, data_nascimento_iv, profissoes_iv) VALUES ($1, $2, $3, $4)", [id_iv, cript_nome.iv, cript_data_nascimento.iv, cript_profissoes.iv]);

        //inserindo os dados criptografados
        const insert_encrypted = await db.query("INSERT INTO CANDIDATO (id, id_iv, cpf, nome, data_nascimento, profissoes) VALUES ($1, $2, $3, $4, $5, $6)", [id, id_iv, cript_cpf.encryptedData, cript_nome.encryptedData, cript_data_nascimento.encryptedData, cript_profissoes.encryptedData]);
        
         return {
            sucess: true,
            status_code: 200
         };
    } 
    catch(err) {   
        const err_code = err.code;
        let error_message = "Um erro não registrado ocorreu.";

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

async function selectCandidatoByCpf(cpf){
    try{
        const encoded_cpf = crypt.criptInfo(cpf, cpf_iv_buffer);
        // A consulta comparará o cpf fornecido criptografado com os registros no banco de dados
        const select_candidato = await db.query("SELECT * FROM CANDIDATO WHERE cpf=$1",[encoded_cpf.encryptedData]);
        
        //Se for encontrado algum candidato, continua a operação
        if(select_candidato.rowCount > 0){
            
            const id_iv = select_candidato.rows[0].id_iv;
            const select_candidato_ivs = await db.query("SELECT * FROM CANDIDATO_IV WHERE id=$1",[id_iv]);

            //Pegando os valores das duas consultas
            const { nome, data_nascimento, profissoes } = select_candidato.rows[0];
            const { nome_iv, data_nascimento_iv, profissoes_iv } = select_candidato_ivs.rows[0];

            //Pega a string e converte de volta para um array de profissoes
            let profissoes_array = crypt.decriptInfo({iv: profissoes_iv, encryptedData: profissoes});
            profissoes_array = JSON.parse(profissoes_array);

            //Descriptografando dados com decriptInfo() e salvando valores em um objeto
            const data = {
                nome: crypt.decriptInfo({iv: nome_iv, encryptedData: nome}),

                cpf: crypt.decriptInfo({iv: encoded_cpf.iv, encryptedData: encoded_cpf.encryptedData}),
                
                data_nascimento: crypt.decriptInfo({iv: data_nascimento_iv, encryptedData: data_nascimento}),

                profissoes: profissoes_array
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
                error_message: "Candidato nao encontrado",
                status_code: 404
            }
        }

    }catch(err){
        const err_code = err.code;
        let error_message = "Um erro não registrado ocorreu.";

        if(postgre_errors.hasOwnProperty(err_code)){
            error_message = postgre_errors[err_code].message;
        }

        return {
            sucess: false,
            error_message,
            status_code: 500
        }
    }
};

async function selectCandidatosCompativeis(codigo_concurso){
    try {
        //Criando uma lista vazia de candidatos compativeis
        let compatibles = [];

        //Buscar pelo concurso
        const concurso = await auxiliarRepos.getVagasFromConcurso(codigo_concurso);

        if(concurso.sucess !== true){
            return {
                sucess: false,
                error_message: concurso.error_message,
                status_code: concurso.status_code,
            };
        }

        //Pegando o array de vagas do concurso
        const vagas = concurso.data;

        //Selecionando todos os candidatos
        const select_all_candidatos = await db.query("SELECT * FROM CANDIDATO");
        
        //Pegando a lista de registros de candidatos
        const all_candidatos = select_all_candidatos.rows;
       
        //Para cada item  na lista de candidatos, verifica se há compatibilidade 
        //entre as profissoes do candidato e as profissões do candidato
        for(let i = 0; i < all_candidatos.length; i++){
            
            //Buscando pelos ivs do candidato
            const candidato_ivs = await db.query("SELECT * FROM CANDIDATO_IV WHERE id=$1", [all_candidatos[i].id_iv]);
            
            //Array de profissoes criptografado e seu iv para descriptografia
            const crypt_profissoes = all_candidatos[i].profissoes;
            const profissoes_iv = candidato_ivs.rows[0].profissoes_iv;

            //Descriptografando a string do array de profissoes e convertendo ela de novo em array
            let profissoes = crypt.decriptInfo({encryptedData: crypt_profissoes, iv: profissoes_iv});

            //Transformando em array de novo
            profissoes = JSON.parse(profissoes);
         
            //flag que verifica se ao menos uma das profissoes do candidato está inclusa na lista de profissoes do candidato
            let profissoes_compativeis = await auxiliarRepos.getListsCommonElements(profissoes, vagas);
           
            //Se houver profissoes compatíveis entre as duas listas, 
            //adiciona os dados do candidato na lista de compativeis
            if(profissoes_compativeis.length > 0){
                const cpf_iv = cpf_iv_buffer.toString('hex');

                let candidato_data = {
                    nome: crypt.decriptInfo({ encryptedData: all_candidatos[i].nome, iv: candidato_ivs.rows[0].nome_iv}),
                    cpf: crypt.decriptInfo({ encryptedData: all_candidatos[i].cpf, iv: cpf_iv}),
                    data_nascimento: crypt.decriptInfo({ encryptedData: all_candidatos[i].data_nascimento, iv: candidato_ivs.rows[0].data_nascimento_iv}),
                    profissoes: crypt.decriptInfo({ encryptedData: all_candidatos[i].profissoes, iv: candidato_ivs.rows[0].profissoes_iv})
                };

                //Converte o array escrito em string para um array de fato 
                candidato_data.profissoes = JSON.parse(candidato_data.profissoes);

                compatibles.push(candidato_data);
            }

        }
        //Retornando os candidatos compativeis em um json de resposta
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
    insertNewCandidato,
    selectCandidatoByCpf,
    selectCandidatosCompativeis,
};