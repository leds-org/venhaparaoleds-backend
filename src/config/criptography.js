const crypto = require('crypto');

const dotenv = require('dotenv').config();

//Objeto com informações importantes para criptografia
const CRIPT_INFOS = {
    algorithm: 'aes-256-cbc',//Algoritmo de criptografia
    secret: process.env.SECRET_KEY //Um valor usado na criação da chave de criptografia
    
}

//Gera um iv aleatorio
function generateRandomIV(){
    return crypto.randomBytes(16);
}

//Método para criptografia de dados
//obs.: O iv (vetor de inicialização),é uma entrada responsável por misturar com um 
//bloco de dados e randomizar a geração da chave de criptografia
function criptInfo(data, iv){

    //Chamada ao método de geração de chave de criptografia, randomizada a partir de dois valores: 
        // -> um valor secreto (secret);
        // -> um valor "salt" (pode ser o iv);
    const key = crypto.pbkdf2Sync(CRIPT_INFOS.secret, iv, 150000, 32, 'sha256');


    //Instancia um bloco de cifragem (que é o objeto a ser usado para criptografar o dado)
    const cipher = crypto.createCipheriv(CRIPT_INFOS.algorithm, Buffer.from(key), Buffer.from(iv));


    //Criptografa o dado e atualiza bloco de cifragem já com o dado criptografado
    let cripted_data = cipher.update(data);

    
    //Concatena os buffers do dado criptografado e do bloco de crifragem
    cripted_data = Buffer.concat([cripted_data, cipher.final()]);


    /* ----------- */
    //obs.: cipher.final() fecha o bloco de cifragem, impossibilitando que novos dados sejam criptografados a partir dele
    /* ----------- */
    
    // console.log(iv.toString('hex'));

    //Retorna objeto com o dado criptografado e o iv associado ao mesmo
    return {
        iv: iv.toString('hex'),
        encryptedData: cripted_data.toString('hex')
    };
}

function decriptInfo(cripted_data){

    //Atribui o iv e o dado criptografado a variáveis
    const iv = Buffer.from(cripted_data.iv, 'hex');
    // console.log(iv);
    const encryptedData = Buffer.from(cripted_data.encryptedData, 'hex');


    //Gera uma chave partir do mesmo iv e do mesmo segredo do dado criptografado 
    const key = crypto.pbkdf2Sync(CRIPT_INFOS.secret, iv, 150000, 32, 'sha256');
    

    /* ----------- */
    //obs.: A chave gerada, por usar os mesmos parâmetros que a mesma gerada para criptografar o dado, também será capaz de descriptografar o dado.
    /* ----------- */
    
    //Instancia um objeto para descriptografia (um decipher block)
    const decipher = crypto.createDecipheriv(CRIPT_INFOS.algorithm, Buffer.from(key), Buffer.from(iv));
    

    //Descriptografa o dado passado
    let decripted_data = decipher.update(encryptedData);
    

    decripted_data = Buffer.concat([decripted_data, decipher.final()])
    
    //Retorna dado descriptografado
    return decripted_data.toString();

    /* ----------- */
    //obs.: o decipher.final() faz a mesma coisa que o cipher.final(), porém agora sendo um bloco de descriptografia sendo finalizado
    /* ----------- */
}

module.exports = {
    criptInfo,
    decriptInfo,
    generateRandomIV,
}