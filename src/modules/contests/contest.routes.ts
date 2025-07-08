import { Router } from "express";
import ContestController from "./contest.controller";

const router = Router();
const contestController = new ContestController();

// Define a rota para obter candidatos por código de concurso
// O código do concurso é passado como parâmetro na URL
router.get("/contests/:code", contestController.getCandidatesByContestCode);

export default router;