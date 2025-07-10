import sys
import os
from datetime import datetime
import re

sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), '..')))

from app.database import Sessao
from app.associacao import criar_candidato, criar_concurso
from app.schemas import CriarCandidato, CriarConcurso

def parse_list(list_str):
    """Extrai itens de uma string como '[item1, item2]'."""
    return [item.strip() for item in list_str.strip('[]').split(',')]

def buscar_candidatos():
    db = Sessao()
    print("Carregando candidatos")
    try:
        with open('data/candidatos.txt', 'r', encoding='utf-8') as f:
            next(f)
            for line in f:
                parts = line.strip().split('\t')
                if len(parts) < 4: continue

                nome, data_nascimento, cpf, profissoes = parts
                
                data_nascimento = datetime.strptime(data_nascimento, '%d/%m/%Y').date()
                professions = parse_list(profissoes)

                candidato_schema = CriarCandidato(
                    nome=nome,
                    data_nascimento=data_nascimento,
                    cpf=cpf,
                    professions=professions
                )
                criar_candidato(db, candidato_schema)
        print("Candidatos carregados!!!")
    except Exception as e:
        print(f"Error: {e}")
    finally:
        db.close()

def buscar_concursos():
    db = Sessao()
    print("Carregando concursos...")
    try:
        with open('data/concursos.txt', 'r', encoding='utf-8') as f:
            next(f) 
            for line in f:
                parts = line.strip().split('\t')
                if len(parts) < 4: continue

                orgao, edital, codigo_concurso, vagas = parts
                vagas = parse_list(vagas)

                concurso_schema = CriarConcurso(
                    orgao=orgao,
                    edital=edital,
                    codigo_concurso=codigo_concurso,
                    vagas=vagas
                )
                criar_concurso(db, concurso_schema)
        print("Concursos carregados!!")
    except Exception as e:
        print(f"Erro {e}")
    finally:
        db.close()

if __name__ == "__main__":
    # Idealmente, você limparia as tabelas aqui antes de carregar
    # from app.database import Base, engine
    # print("Limpando tabelas antigas...")
    # Base.metadata.drop_all(bind=engine)
    # Base.metadata.create_all(bind=engine)
    
    buscar_candidatos()
    buscar_concursos()
