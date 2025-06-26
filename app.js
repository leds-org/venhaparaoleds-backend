// O express é uma lib que adiciona uma camada a mais de abtração na criação de aplicações em node js, facilitando o processo de desenvolvimento
const express = require('express');


//CORS é um recurso que restringe quais domínios podem acessar a API
const cors = require('cors');

//Importando as rotas da API
const candidato = require('./src/routes/candidato');
const concurso = require('./src/routes/concurso');

//Instanciando a aplicação express
const app = express();

//Fazendo configurações na aplicação
app.use(express.urlencoded({extended: true}));//O extend define qual lib do node vai lidar com os dados enviados nas rotas

app.use(express.json())//Permitindo a API enviar e receber dados do tipo JSON

app.use(express.json({type:'application/vnd.ai+json'}));

app.use(cors());

//Incluindo as rotas
app.use("/api/",candidato);
app.use("/api/", concurso);


//Exportando módulo para uso externo
module.exports = app;

