# Desafio Backend - LEDS

Candidato: Luiz Henrique Oliveira da Hora

## Tecnologias Utilizadas
- Python 3.11
- Flask

## Como Rodar o Projeto
### Requisitos
- Python 3.11 instalado
- Pip instalado

### Instalando Dependências
Antes de rodar o projeto, instale as dependências necessárias:
```bash
pip install -r requirements.txt
```

### Executando o Servidor
Para iniciar o servidor Flask em modo de desenvolvimento, execute:
```bash
python app.py
```
O servidor estará rodando em `http://127.0.0.1:5000`.

## Endpoints
### 1. Obter candidatos por concurso
**Rota:**
```
GET /candidatos/concursos?codigo=<codigo_do_concurso>
```
**Exemplo de Requisição:**
```
GET /candidatos/concursos?codigo=61828450843
```

