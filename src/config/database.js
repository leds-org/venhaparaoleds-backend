//Importa classe para criação de uma Pool de conexão com banco de dados
const { Pool } = require('pg'); 

//Módulo para carregamento de variáveis de ambiente de .env para process.env
const dotenv = require('dotenv').config();

//Módulos voltados para interação com sistema de arquivos
const fs = require('fs');


//Cria uma nova Pool de conexão com bd
const pool = new Pool(
    //json de configuração da instância
    {
        connectionString: process.env.DATABASE_URL, //url do bd
        ssl: {
            rejectUnauthorized: true,
            ca: process.env.CA
        }
    }
);

//Envia uma mensagem ao console ao conectar ao banco de dados
pool.on('connect', () =>{
    console.log('API conectada à base de dados');
});

//Exporta método de consulta a bd (passando uma string SQL e uma lista de valores)
module.exports = {
    query: (text, params) => pool.query(text, params), 
};