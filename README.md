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
Utilizei essas tecnologias por estar fazendo um curso da Udemy sobre **ASP.NET CORE + ENTITY FRAMEWORK + DDD (Domain-Driven Design)**. Como estou na metade do curso, a abordagem que o professor utilizou foi de primeiro implementar a aplicação com o uso do MVC e ao final fazer toda a refatoração do projeto. Para que houvesse a integração com um banco de dados local sem a necessidade de instalações "extra", pesquisei e, com a ajuda do Deepseek, fiz a implementação usando SQLite e LINQ, tecnologias que eu nunca havia utilizado.
 
---

## Diferenciais Implementados
- **Utilizar banco de dados** - Implementei o SQLite como banco de dados local, armazenando dados em projeto.db. Optei por essa solução por ser leve, não exigir instalação adicional e ser ideal para projetos pequenos.
- **Implementar Clean Code**
- **Padrão de programação da tecnologia** - Segui o padrão MVC do ASP.NET Core

---

## Detalhes da Implementação

### Lógica de Busca
- **Busca de concursos por CPF**: O sistema verifica as profissões do candidato e busca concursos que tenham vagas compatíveis.
  ```csharp
  var concursos = await _context.Concursos
      .Where(c => c.Vagas.Any(v => candidato.Profissoes.Contains(v)))
      .ToListAsync();
  ```

- **Busca de candidatos por código de concurso**: O sistema verifica as vagas do concurso e busca candidatos com profissões compatíveis.
  ```csharp
  var candidatos = await _context.Candidatos
      .Where(c => c.Profissoes.Any(p => concurso.Vagas.Contains(p))))
      .ToListAsync();
  ```

### Armazenamento de Listas
- Profissões e vagas são armazenadas como strings no formato `item1,item2,item3` e convertidas para listas no código.

---

## Execução do Projeto
Para executar o projeto, siga os passos descritos abaixo:

1. **Clone o repositório:**  
   Abra o terminal ou prompt de comando e execute:

   ```bash
   git clone https://github.com/guihocosta/venhaparaoleds-backend.git
   ```

2. **Acesse a pasta do projeto:**  
   Entre na pasta clonada:

   ```bash
   cd venhaparaoleds-backend
   ```

3. **Verifique a instalação do .NET 8:**  
   Certifique-se de que o **.NET 8 SDK** está instalado em sua máquina. Caso não tenha, baixe-o através do [site oficial](https://dotnet.microsoft.com/download/dotnet/8.0).

4. **Restaure os pacotes:**  
   No terminal, execute:

   ```bash
   dotnet restore
   ```

5. **Execute o projeto:**  
   Inicie a aplicação com o comando:

   ```bash
   dotnet run
   ```

6. **Acesse a aplicação:**  
   Abra o navegador e acesse o endereço informado no terminal (geralmente `http://localhost:5000` ou outro especificado).

> **Observação:**  
> O projeto utiliza SQLite como banco de dados. Ao executá-lo, o arquivo `projeto.db` (ou o nome configurado) será criado automaticamente na pasta do projeto, permitindo o armazenamento dos dados localmente.
