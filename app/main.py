from fastapi import FastAPI, Depends, HTTPException
from sqlalchemy.orm import Session
from typing import List

from . import associacao, models, schemas
from .database import Sessao, conexao, get_db

# Cria as tabelas no banco de dados (se não existirem)
models.Base.metadata.create_all(bind=conexao)

app = FastAPI(
    title="API de Concursos e Candidatos",
    description="API pra realizar a união entre perfis de candidatos X vagas de concursos.",
    version="1.0.0"
)

@app.get("/candidatos/{cpf}/concursos", response_model=List[schemas.InfoConcurso], tags=["Buscas"])
def obter_concursos_por_candidato(cpf: str, db: Session = Depends(get_db)):
    """
    Lista os órgãos, códigos e editais dos concursos públicos que se
    encaixam no perfil de um candidato, baseado no seu CPF.
    """
    candidato = associacao.obter_candidato_por_cpf(db, cpf=cpf)
    if not candidato:
        raise HTTPException(status_code=404, detail="Candidato não encontrado")

    if not candidato.profissoes:
        return []

    associados_concursos = associacao.obter_candidatos_por_profissao(db, vagas=candidato.profissoes)
    return associados_concursos

@app.get("/concursos/{codigo}/candidatos", response_model=List[schemas.CandidateInfo], tags=["Buscas"])
def obter_candidatos_por_concurso(codigo: str, db: Session = Depends(get_db)):
    """
    Lista o nome, data de nascimento e o CPF dos candidatos que se
    encaixam no perfil de um concurso, com base no Código do Concurso.
    """
    concurso = associacao.obter_concurso_por_codigo_c(db, code=codigo)
    if not concurso:
        raise HTTPException(status_code=404, detail="Concurso não encontrado")

    if not concurso.vagas:
        return []

    matching_candidates = associacao.obter_candidatos_por_profissao(db, profissoes=concurso.vagas)
    return matching_candidates

# Endpoints administrativos para adicionar dados (opcional, mas útil para testes)
@app.post("/candidatos/", response_model=schemas.Candidate, tags=["Administração"])
def criar_candidato(candidate: schemas.CandidateCreate, db: Session = Depends(get_db)):
    return associacao.create_candidate(db=db, candidate=candidate)

@app.post("/concursos/", response_model=schemas.Contest, tags=["Administração"])
def criar_concurso(concurso: schemas.ContestCreate, db: Session = Depends(get_db)):
    return associacao.create_contest(db=db, concurso=concurso)
