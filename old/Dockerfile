FROM python:3.9-slim

WORKDIR /code

# Cria e ativa um ambiente virtual
RUN python -m venv /opt/venv
ENV PATH="/opt/venv/bin:$PATH"

# Copia o arquivo de requisitos
COPY ./requirements.txt .

# Instala as dependências e verifica a instalação
RUN pip install --no-cache-dir --upgrade pip && \
    pip install --no-cache-dir -r requirements.txt && \
    echo "--- Verificando o conteúdo do venv ---" && \
    ls -lR /opt/venv/lib/python3.9/site-packages/ | grep uvicorn && \
    echo "--- Verificando com pip show ---" && \
    pip show uvicorn

# Copia o resto do código
COPY ./app /code/app
COPY ./scripts /code/scripts
COPY ./data /code/data

# Expõe a porta
EXPOSE 8000

# Comando para iniciar a aplicação
CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8000"]