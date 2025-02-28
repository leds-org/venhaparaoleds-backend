# Desafio Backend - LEDS

**Candidato:** Guilherme Henrique Omena Costa  
**Data de Entrega:** 28/02/2025  

---

## Visão Geral do Projeto
Solução desenvolvida para o desafio do LEDS Academy, com o objetivo de demonstrar habilidades em desenvolvimento backend utilizando **ASP.NET Core MVC**, **Entity Framework** e **SQLite**. A aplicação permite:

1. **Buscar concursos públicos compatíveis** com as profissões de um candidato, com base em seu CPF.
2. **Buscar candidatos compatíveis** com as vagas de um concurso, com base no código do concurso.

---

## Tecnologias Utilizadas
- **ASP.NET Core 8.0.13** (MVC Pattern)
- **Entity Framework Core 9.0.2**
- **SQLite** (Banco de dados embutido)
- **LINQ** (Consultas à base de dados)
- **HTML5/Razor** (Interface de usuário)
- **Bootstrap 5.1.0** (Estilização)

## Justificativa do Uso destas Tecnologias
Utilizei essas tecnologias por estar fazendo um curso da Udemy sobre **ASP.NET CORE + ENTITY FRAMEWORK + DDD (Domain-Driven Design)**. Como estou na metade do curso, a abordagem que o professor utilizou foi de primeiro implementar a aplicação com o uso do MVC e ao final fazer toda a refatoração do projeto, migrando para a arquitetura em camadas. Para que houvesse a integração com um banco de dados local sem a necessidade de instalações "extra", pesquisei e, com a ajuda do Deepseek, fiz a implementação usando SQLite e LINQ, tecnologias que eu nunca havia utilizado.

---

## 🏆 Diferenciais Implementados
- **Utilizar banco de dados** Implementei o SQLite como banco de dados local, armazenando dados em projeto.db. Optei por essa solução por ser leve, não exigir instalação adicional e ser ideal para projetos pequenos.
- **Implementar Clean Code**
- **Padrão de programação da tecnologia** Segui o padrão MVC do ASP.NET Core
