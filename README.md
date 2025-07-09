# LedsAplication

## Descrição do projeto

Esse é um breve projeto desenvolvido em **ASP.NET 8** que busca gerenciar e interligar candidatos e concursos presentes em dois arquivos `.txt`, `candidatos.txt` e `concursos.txt`.

O sistema permite realizar consultas cruzadas entre as vagas disponíveis nos concursos e as profissões cadastradas nos perfis dos candidatos.

> ℹ️ **Também é possível executar e interagir com a aplicação via um frontend básico localizado na pasta `FrontendBasico`.**
> Basta abrir o arquivo `index.html` no navegador após rodar o backend.

---

## Tecnologias empregadas

- **Backend**: ASP.NET
- **Linguagem**: C#
- **Banco de dados**: SQLite com Entity Framework
- **Injeção de Dependência**: Padrão do .NET Core
- **Mapeamento de Objetos**: `MapConverter`

### Bibliotecas utilizadas

- `Microsoft.Extensions.Configuration.FileExtensions`
- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.Configuration`
- `Microsoft.Extensions.DependencyInjection.Abstractions`
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.Migrations`
- `Swashbuckle.AspNetCore` (Swagger para documentação da API)

---

## Como executar o projeto

### Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- Um editor de código como [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Passos

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/LedsAplication.git
cd LedsAplication
```

2. Restaure os pacotes:

```bash
dotnet restore
```

3. Rode o projeto:

```bash
dotnet run --project LedsAPI
```

4. Acesse a API via Swagger em:

```
http://localhost:{porta}/swagger
```

> Substitua `{porta}` pela porta exibida ao rodar o projeto.

---

## Frontend Básico

O projeto inclui uma interface simples localizada na pasta `FrontendBasico`, que permite interagir com o backend para:

- Consultar concursos por CPF
- Consultar candidatos por código de concurso
- Cadastrar candidatos
- Cadastrar concursos

### Como usar:

1. Após rodar o backend, abra o arquivo `FrontendBasico/index.html` no navegador.
2. Preencha os campos e clique nos botões de ação.

---

## Considerações finais

Este projeto foi desenvolvido como parte do **Desafio Backend da LEDS**.  
Todos os critérios essenciais do desafio foram atendidos, com destaque para:

- Importação e cadastro de dados
- Separação em camadas (DTOs, Services, Repositórios)
- Banco de dados relacional via EF Core
- Testes comportamentais com banco InMemory
- Consumo da API por frontend externo
