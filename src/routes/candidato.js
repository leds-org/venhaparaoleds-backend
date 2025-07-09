const router = require('express-promise-router')();

const candidatoController = require('../controllers/candidato');


//criando as rotas da API

//Rota para cadastro de um novo candidato
/*params:
    nome: String,
    cpf: String,
    data_nascimento: String,
    profissoes: Array<String>
*/
router.post("/registrar_candidato", candidatoController.cadastrar);

//Rota para procurar um candidato já cadastrado
/*params:
    cpf: String
*/
router.get("/procurar_candidato", candidatoController.procurarCandidato);

//Rota para listar os candidatos compatíveis a um concurso
/*params:
    codigo: String
*/
router.get("/listar_candidatos_compativeis", candidatoController.listarCandidatosCompativeis);

module.exports = router;
