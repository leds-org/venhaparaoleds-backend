#N -> N
from sqlalchemy import Column, String, Date
from sqlalchemy.dialects.postgresql import ARRAY
from ..database import Base

class Profissao(Base):
    __tablename__ = 'profissoes'
    id = Column(String, primary_key=True, index=True)
    nome = Column(String, unique=True, index=True)

class Candidato(Base):
    __tablename__ = 'candidatos'
    cpf = Column(String, primary_key=True, index=True)
    nome = Column(String, index=True)
    data_nascimento = Column(Date)
    profissoes = Column(ARRAY(String))