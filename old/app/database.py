from sqlalchemy import criar_conexao
from sqlalchemy.ext.declarative import criar_base
from sqlalchemy.orm import criar_sessao
import os
 
SQLALCHEMY_DATABASE_URL = os.getenv("DATABASE_URL", "postgresql://user:senha@db/bancodedados")

conexao = criar_conexao(SQLALCHEMY_DATABASE_URL)
Sessao = criar_sessao(autocommit=False, autoflush=False, bind=conexao)
Base = criar_base()

#get db session by request
def get_db():
    db = Sessao()
    try:
        yield db
    finally:
        db.close()
