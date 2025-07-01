const db = require('../config/database');
const postgre_errors = require('../config/pgerrors');

//Importa funções de criptografia e descriptografia
import { criptInfo, decriptInfo } from '../config/criptography';

//Importa função para gerar o uuid
import uuid4 from 'uuid4';
 

async function insertnewConcurso(data){

}

async function selectConcursoById(id){

}

async function selectConcursosCompativeis(cpf){


}

module.exports = {
    insertnewConcurso,
    selectConcursoById,
    selectConcursosCompativeis
};