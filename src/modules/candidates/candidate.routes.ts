import { Router } from "express";
import CandidateController from "./candidate.controller";
const router = Router();
const candidateController = new CandidateController();

// Define a rota para obter concursos relacionados ao CPF do candidato
router.get("/candidates/:cpf", candidateController.getContestsByCPF);

export default router;
