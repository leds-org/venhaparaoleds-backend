from pydantic import BaseModel
from typing import List

class DadosConcurso(BaseModel):
    codigo_concurso: str
    orgao: str
    edital: str
    vagas: List[str]

class CriarConcurso(DadosConcurso):
    pass

class Concurso(DadosConcurso):
    class Config:
        orm_mode = True

# Schema para a resposta da busca de concursos por candidato
class InfoConcurso(BaseModel):
    orgao: str
    codigo_concurso: str
    edital: str

    class Config:
        orm_mode = True