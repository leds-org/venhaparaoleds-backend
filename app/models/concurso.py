from sqlalchemy import Column, String
from sqlalchemy.dialects.postgresql import ARRAY
from ..database import Base

class Concurso(Base):
    __tablename__ = 'concursos'
    codigo_concurso = Column(String, primary_key=True, index=True)
    orgao = Column(String, index=True, nullable=False)
    edital = Column(String, nullable=False)
    vagas = Column(ARRAY(String))