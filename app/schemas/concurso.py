from pydantic import BaseModel
from typing import List

class ContestBase(BaseModel):
    codigo_concurso: str
    orgao: str
    edital: str
    vagas: List[str]

class ContestCreate(ContestBase):
    pass

class Contest(ContestBase):
    class Config:
        orm_mode = True

# Schema para a resposta da busca de concursos por candidato
class ContestInfo(BaseModel):
    organ: str
    code: str
    edital: str

    class Config:
        orm_mode = True