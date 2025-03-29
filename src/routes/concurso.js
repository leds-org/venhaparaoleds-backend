const router = require('express-promise-router')();

const concursoController = require('../controllers/concurso');


//criando as rotas da API
router.get('/concursos', concursoController.listConcursos);



module.exports = router;