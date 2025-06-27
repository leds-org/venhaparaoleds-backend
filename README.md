# Solução do Desafio Backend - LEDS

O objetivo foi desenvolver uma API para gerenciar candidatos e concursos públicos, permitindo buscas conforme o perfil dos candidatos e concursos.

---

## Tecnologias Utilizadas

- Node.js 18  
- TypeScript  
- Express.js  
- Prisma ORM com SQLite  
- Jest para testes unitários
- Supertest para testes de integração
- Docker para containerização da aplicação

---

## Como Rodar

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/LucassenaSM/venhaparaoleds-backend.git](https://github.com/LucassenaSM/venhaparaoleds-backend.git)
    ```
   
2.  **Acesse a pasta do projeto:**
    ```bash
    cd venhaparaoleds-backend
    ```

3. **Instale as dependências:**  
   ```bash
   npm install
   ```

5. **Gere o cliente Prisma e o banco de dados:**
   ```bash
   npx prisma generate
   ```

7. **Rode o servidor**:

- Para modo desenvolvimento:  
   ```bash
  npm run dev
   ```
  
- Para modo produção:   
  ```bash
  npm run build
  ```
  ```bash
  npm start
  ```

### Como Rodar com Docker

Se você prefere rodar o projeto usando **Docker** e **Docker Compose**, siga estes passos:

---

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/LucassenaSM/venhaparaoleds-backend.git](https://github.com/LucassenaSM/venhaparaoleds-backend.git)
    ```

2.  **Acesse a pasta do projeto:**
    ```bash
    cd venhaparaoleds-backend
    ```

3.  **Inicie os serviços com Docker Compose:**
    ```bash
    docker-compose up -d
    ```
    
---

## API Endpoints Principais

- `GET /api/candidates/:cpf`  
  Retorna os concursos públicos compatíveis com o candidato pelo CPF.

- `GET /api/contests/:contestCode`  
  Retorna os candidatos que se encaixam no perfil do concurso pelo código.

---

## Testes

### Testes unitários e comportamentais estão implementados usando Jest e Supertest

Para rodar os testes:  
```bash
npm test
```
### Testes Manuais com Clientes de API
Se você preferir testar a API manualmente, pode usar uma ferramenta como Insomnia ou Postman.    
Abaixo estão exemplos de endpoints que você pode experimentar (certifique-se de que sua aplicação esteja rodando em http://localhost:3000):

Para recuperar um candidato específico:    
`http://localhost:3000/api/candidates/311.667.973-47`

Para recuperar um concurso específico:    
`http://localhost:3000/api/contests/61828450843`

## Observações

- O banco de dados padrão é SQLite, armazenado em `prisma/dev.db`.
- O banco já vem com alguns dados de exemplo para facilitar os testes.
- Para explorar os dados de forma visual, você pode usar o **Prisma Studio** com o comando:
```bash
 npx prisma studio
```   
  
