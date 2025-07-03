from sqlalchemy import Column, String, Date
from sqlalchemy.orm import relationship
from app.core.db import Base
from .uniao import candidato_profissao

class Profissao(Base):
    __tablename__ = 'profissoes'
    id = Column(Integer, primary_key=True, index=True)
    nome = Column(String, unique=True, index=True)

class Candidato(Base):
    __tablename__ = 'candidatos'
    cpf = Column(String, primary_key=True, index=True)
    nome = Column(String, index=True)
    data_nascimento = Column(Date)
    profissoes = relationship("Profissao", secondary=candidato_profissao, backref="candidatos")
