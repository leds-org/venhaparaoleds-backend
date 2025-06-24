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

1. Clone o repositório:  
   `git clone https://github.com/LucassenaSM/venhaparaoleds-backend.git`
   
2. Acesse a pasta do projeto:   
   `cd venhaparaoleds-backend` 

3. Instale as dependências:  
   `npm install`

4. Gere o cliente Prisma e o banco de dados:   
   `npx prisma generate`

5. Rode o servidor:

- Para modo desenvolvimento:  
   `npm run dev`
  
- Para modo produção:   
  `npm run build` e  `npm start`

---

## API Endpoints Principais

- `GET /api/candidates/:cpf`  
  Retorna os concursos públicos compatíveis com o candidato pelo CPF.

- `GET /api/contests/:contestCode`  
  Retorna os candidatos que se encaixam no perfil do concurso pelo código.

---

## Testes

Testes unitários estão implementados usando Jest e Supertest

Para rodar os testes:  
`npm test`

## Observações

- O banco de dados padrão é SQLite, armazenado em `prisma/dev.db`.
- O banco já vem com alguns dados de exemplo para facilitar os testes.
- Para explorar os dados de forma visual, você pode usar o **Prisma Studio** com o comando: `npx prisma studio`   
  
