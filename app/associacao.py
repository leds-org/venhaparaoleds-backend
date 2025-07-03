from sqlalchemy.orm import Session
from sqlalchemy import or_
from . import models, schemas

#* Listar orgãos / códigos / editais que encaixam no perfil pelo CPF
#* Listar nome / dataNascimento / cpf baseado no código do concurso


def obter_candidato_por_cpf(db: Session, cpf: str):
    return db.query(models.Candidato).filter(models.Candidato.cpf == cpf).first()

def criar_candidato(db: Session, candidate: schemas.CriarCandidato):
    db_candidate = models.Candidato(**candidate.dict())
    db.add(db_candidate)
    db.commit()
    db.refresh(db_candidate)
    return db_candidate

def get_candidates_by_professions(db: Session, professions: list[str]):
    return db.query(models.Candidato).filter(
        or_(*[models.Candidato.professions.any(p) for p in professions])
    ).all()

# Funções para Concursos
def get_contest_by_code(db: Session, code: str):
    return db.query(models.Concurso).filter(models.Concurso.code == code).first()

def create_contest(db: Session, contest: schemas.ContestCreate):
    db_contest = models.Concurso(**contest.dict())
    db.add(db_contest)
    db.commit()
    db.refresh(db_contest)
    return db_contest

def get_contests_by_vacancies(db: Session, vacancies: list[str]):
    return db.query(models.Concurso).filter(
        or_(*[models.Concurso.vacancies.any(v) for v in vacancies])
    ).all()
