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

def obter_candidatos_por_profissao(db: Session, profissoes: list[str]):
    return db.query(models.Candidato).filter(
        or_(*[models.Candidato.profissoes.any(p) for p in profissoes])
    ).all()

def obter_concurso_por_codigo_concurso(db: Session, codigo_concurso: str):
    return db.query(models.Concurso).filter(models.Concurso.codigo_concurso == codigo_concurso).first()

def criar_concurso(db: Session, concurso: schemas.CriarConcurso):
    db_concurso = models.Concurso(**concurso.dict())
    db.add(db_concurso)
    db.commit()
    db.refresh(db_concurso)
    return db_concurso

def obter_concurso_por_vagas(db: Session, vagas: list[str]):
    return db.query(models.Concurso).filter(
        or_(*[models.Concurso.vagas.any(v) for v in vagas])
    ).all()