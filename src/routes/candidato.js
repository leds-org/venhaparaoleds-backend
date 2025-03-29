const router = require('express-promise-router')();

const candidatoController = require('../controllers/candidato');


//criando as rotas da API
router.get('/candidatos', candidatoController.listCandidatos);



module.exports = router;
