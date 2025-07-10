from pydantic import BaseModel
from datetime import date
from typing import List

class DadosCandidato(BaseModel):
    cpf: str
    nome: str
    data_nascimento: date
    profissoes: List[str]

class CriarCandidato(DadosCandidato):
    pass

class Candidato(DadosCandidato):
    class Config:
        orm_mode = True

class InfoCandidato(BaseModel):
    nome: str
    data_nascimento: date
    cpf: str

    class Config:
        orm_mode = True