const router = require('express-promise-router')();

const concursoController = require('../controllers/concurso');


//Rota para registrar um novo concurso
/*params:
    codigo: String,
    edital: String,
    orgao: String,
    vagas: Object<String>
*/
router.post("/registrar_concurso", concursoController.registrarConcurso);

//Rota para procurar um concurso registrado
/*params:
    codigo: String
*/
router.get("/procurar_concurso", concursoController.procurarConcurso);

//Rota para listar os concursos compativeis a um candidato
/*params:
    cpf: String
*/
router.get("/listar_concursos_compativeis", concursoController.listarConcursosCompativeis);

module.exports = router;