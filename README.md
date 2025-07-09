# Sobre o projeto
Este projeto de software, descrito nesta documentação, implementa o Backend de uma aplicação web de **gerenciamento de concursos online e candidatos**, feito baseado nas descrições e requisitos especificados no desafio técnico proposto pelo **Laboratório de Extensão e Desenvolvimento de Soluções (LEDS)**, programa de extensão ligado ao **Instituto Federal do Espírito Santo (IFES)**.

---

## 🛠️ Tecnologias utilizadas


**Framework:** Express JS


**Ling de programação:** Javascript (JS)

**Banco de Dados/RDBMS:** PostgreSQL

**Principais módulos:**

- crypto (pacote usado para criptografia e descriptografia de dados no backend); 
- express (módulo que facilita a criação de aplicações backend em node js e auxilia no gerenciamento de rotas );

---

# 📌 Modelagem do Projeto
Esta seção apresentará o projeto sob o ponto de vista arquitetural, mostrando sua estrutura, implementações além das esperadas pelo teste proposto e a organização dos módulos e componentes nos diretórios de cada parte da aplicação.


---


## 💡 Diferenciais de implementação

- **Criptografia de dados:** Para atribuir uma camada a mais de segurança à aplicação, um módulo com métodos de criptografia foi desenvolvido para criptografia e descriptografia dos dados de cada entidade da aplicação. Como apontado na estrutura de diretório do projeto (nas seções *"🏗 Padrão de Arquitetura"* e *"📁 Estrutura de Diretório"*), esse módulo estará na pasta **"config"** com o nome **"criptography.js"**.
<br>

- **Randomização de chave primária com UUID:** As chaves primárias da duas entidades Candidato e Concurso serão randômicas para acrescentar mais segurança no acesso aos dados. Essa randomização dos IDs será feita aplicando o conceito de <u>*UUID*</u> e utilizando um módulo do Javascript especializado nessa randomização (o nome do módulo é <u>*uuidv4*</u>).

---

## 🏗 Padrão de Arquitetura
Esta API possui a arquitetura construída com base na representação da imagem abaixo:

📌 **Imagem do modelo de arquitetura**:
![diagrama de arquitetura do projeto](https://i.imgur.com/X7EH5jz.png)

O projeto foi construído com um padrão específico de retorno de JSONs entre as camadas da aplicação:

```
{
    sucess: boolean
    error_message?: String
    data?: Object<Concurso | Candidato>
    response_time: double
}

```
> ***obs.:*** As propriedades acompanhadas com ? representam informações que podem ou não ser especificadas no objeto dependendo da camada que estiver dando a resposta e da rota que for chamada.

O pacote **config** servirá como uma camada auxiliar para o repository e para o módulo de inicialização da API; isto pois o config tem um módulo que lida com criptografia/descriptografia (utilizado pela camada repository) e o módulo de configuração da instância do banco de dados (utilizado pelo index.js da aplicação).

> ***obs.2:*** Na camada de respositório, haverá um módulo (***auxiliar.js***) responsável por conter operações mediativas entre a entidade Candidato e a entidade Concurso. 


## 🐘 Modelagem do Banco de Dados

Tendo em vista o minimundo oferecido pelo desafio técnico, o banco de dados Postgre foi construído com base no modelo representado na imagem abaixo:

![Modelagem do banco de dados](https://i.imgur.com/f6uLfv3.png)

Os dados das entidades Candidato e Concurso serão criptografados utilizando uma geração de chave de criptografia com um valor "*salt*" e um **Vetor de Inicialização (IV)**. Como, na implementação adota por este projeto, o IV é o valor necessário para que haja a descriptografia desses dados, o mesmo será armazenado no banco de dados como entidades relacionadas diretamente às entidades Candidato e Concurso, cada um contendo seu conjunto de IVs randômicos. Vale informar que o IV é único por dado.Os IDs das entidades Candidato e Concurso, entretanto, receberão um valor randômico seguindo o conceito de UUID.

Para os valores **cpf** e **código** (pertencentes as entidades Candidato e Concurso, respectivamente), o valor <u>*salt*</u> (que, assim como os outros, é o próprio iv), será derivado de um <u>valor **fixo e escondido entre as variáveis de ambiente** da aplicação (no .env)</u>; assim remediando o problema de não haver um iv aleatório para cada cpf e código.



## 🛣️ Rotas da Aplicação

Considerando as duas entidades principais mencionadas na seção *"🐘 Modelagem de Banco de Dados"** (Candidato e Concurso), as rotas definidas na camada "Routes" foram as especificadas abaixo já na sintaxe do Javascript, linguagem de programação escolhida para desenvolver este projeto:
<br>
> **Para candidato:**
```javascript
    //Rota para cadastro de um novo candidato
    /*params:
        nome: String,
        cpf: String,
        data_nascimento: String,
        profissoes: Array<String>
    */
    route.post("/registrar_candidato", candidatoController.cadastrar);

    //Rota para procurar um candidato já cadastrado
    /*params:
        cpf : String
    */
    route.get("/procurar_candidato", candidatoController.procurarCandidato);
    
    //Rota para listar os candidatos compatíveis a um concurso
    /*params:
        codigo: String
    */
    route.get("/listar_candidatos_compativeis", candidatoController.listarCandidatosCompativeis);
    
```

<br><br>

> **Para concurso:**
```javascript
    //Rota para registrar um novo concurso
    /*params:
        codigo: String,
        edital: String,
        orgao: String,
        vagas: Array<String>
    */
    route.post("/registrar_concurso", concursoController.registrarConcurso);

    //Rota para procurar um concurso registrado
    /*params:
        codigo: String
    */
    route.get("/procurar_concurso", concursoController.procurarConcurso);

    //Rota para listar os concursos compativeis a um candidato
    /*params:
        cpf: String
    */
    route.get("/listar_concursos_compativeis", concursoController.listarConcursosCompativeis);

```
>***obs.:*** Todas as rotas (ao rodar a aplicação localmente) são antecedidas pelo prefixo **"http://localhost:3000/api"**
>(a rota de pesquisa por candidato, por exemplo, ficaria "http://localhost:3000/api/procurar_candidato").
---

## 📁 Estrutura de Diretórios 
Segue abaixo a estrutura de diretórios da aplicação:

```bash
📂 teste-leds-backend
 ┣ 📂 src
 ┃ ┣ 📂 controllers      # Controladores, para mediação entre o acesso da rota pelo front e as camadas de acesso ao banco de dados  
 ┃ ┣ 📂 repositories     # Métodos de acesso direto ao BD (aqui estarão as consultas SQL)
 ┃ ┣ 📂 routes           # Rotas da API   
 ┃ ┣ 📂 services         # Métodos de acesso indireto ao BD (onde terão as regras de negócio e controle de retorno de status)
 ┃ ┗ 📂 config           # Arquivos de configuração e módulos personalizados (ex.: database e criptography)
 ┣ 📜 README.md          # Documentação do projeto
 ┣ 📜 package.json       # Dependências do projeto
 ┗ 📜 .gitignore         # Arquivos ignorados pelo Git
```

>***obs.:*** a estrutura acima oculta outras pastas secundárias mas relevantes do projeto como, por exemplo, as pastas com os imports de módulos necessários para cada lado do projeto (node_modules).

---

## 🖥️ Rodando a API Localmente

Para testar a aplicação, é necessário que haja uma ferramenta específica para testes de rotas (como o Postman, por exemplo) 
e a versão mais recente do Node instalado em seu desktop. Desse modo, antes de executar a API, crie um .env na raiz do projeto e cole as variáveis abaixo:

```bash
DATABASE_URL=postgres://avnadmin:AVNS_BO9se-N7qrs3ZdBYgVh@leds-backend-leds-backend.c.aivencloud.com:28115/defaultdb
SECRET_KEY=ledsodespertardaforca237232382912
CPF_FIX_SALT=7a0f3e2d1c5b8a9e4f6d0c1b2a3e5d4c
CODIGO_FIX_SALT=45af5ac76e82e4a29753397bc61061c6
CA="-----BEGIN CERTIFICATE-----
MIIEUDCCArigAwIBAgIUZajGVDoZrEXBk7EeFeEFNhHy2WcwDQYJKoZIhvcNAQEM
BQAwQDE+MDwGA1UEAww1NTg5MzM4M2ItOTVlZC00OTY2LWI5ZjItODYzMTZjNDg5
YTM4IEdFTiAxIFByb2plY3QgQ0EwHhcNMjUwNzAxMTg1ODQ3WhcNMzUwNjI5MTg1
ODQ3WjBAMT4wPAYDVQQDDDU1ODkzMzgzYi05NWVkLTQ5NjYtYjlmMi04NjMxNmM0
ODlhMzggR0VOIDEgUHJvamVjdCBDQTCCAaIwDQYJKoZIhvcNAQEBBQADggGPADCC
AYoCggGBALe73NwZPrJpGHICk8dyrGVJa1x8+1UxYwUaWO8jwXZvN8OoSJQ2qCiN
Zb38k+iMCBO8XWc1Ggrp7AAQeD7od3RO7FxiDTm9/tdJYtc/urZxAE6HLqYk/wiY
ntDock6lKE06X7I+TEB6sa402/cZwVGSndargNexQk0P2DaSImPFuOGAJM5WGKAJ
79PV18cm67n5q9VPZqkWscKYcY5qMsk4+DSNt0nwe15ItWIBLMvRHbohsh3b9Sf0
xyiIm+5Qkj8lF/Q9bfwORbgBRNUVRan31aMljbRlepQKXLKrFSLGJDuWCAIecmIB
cqWoC0RIeU0Y8K3Za2Oz529hWyidkB+IshBFgZy0z4ZVyVtENHxms38i9KBwo2f0
tGovJ7c8og4anrsktwRwt4aN1omZOb5S7PY0yfu9btyXrt3h69uEwJHOhtE39Sin
lFeGd6JPx2Ju9AQyPo4xVBVS721yX4JpxyYl4jjyO+1DNBkFd9amgIwC2rTKiMJy
JENzACLfjwIDAQABo0IwQDAdBgNVHQ4EFgQUOntHqBXDBDsMj3cXs43i3y4XrkUw
EgYDVR0TAQH/BAgwBgEB/wIBADALBgNVHQ8EBAMCAQYwDQYJKoZIhvcNAQEMBQAD
ggGBAI8okKxQ521X4SU+9c/K2oE9joYwMXm4c8ColSbUE9maTLot6cw+qgSBLw84
WBzszduZzf2qcQnbycaR9BlK3ZzYfOpr0m95jdzYUaPPQ/wZrzTBhqFUVaKob8GC
XvKFm0d+DGOH+TuyCpcfVplFv9eL+WS6J94+3/+/gWux3YvtXuEcTIyiIfvdLnIb
9fla+H6PwQNgFaHgtRYwFkzHxivoD9Xu6/4R+ENjnqzsDqWpMiTN1rPo5gQxKxus
ZnOa1wX4e7Hdf6Q5zeA4K53ngmGWcNfAgq88bGOSIB1vQ9dgt+P/29g5RH38iXyo
PkjlRmFcw4ghBKIOtgHb/DAPKgSvbN3lE1+V/R7hwY5FTN37OE3Lji/ni2/4IGeo
i0rW9mEfH3QFIidKy0xVMv/0sGBv4OFW6rh9mtXkNllfwWz9El6CjY1ZpKvfVYFA
0D4nwrlyrj8UN+STS+TpjK6oMTKEBPHLXRnDwYtgH6EQQDo09pxOnD70oRVkSpYtBFnD3A==
-----END CERTIFICATE-----"
```
>***obs.:*** É de conhecimento que esta prática não é segura, mas como se trata de uma aplicação que não utiliza dados reais, se fez necessária essa prática para possibilitar o teste da API em outras máquinas

Após os passos acima, no diretório raiz do projeto, rode os seguintes comandos:

```bash
npm i # Para instalar todas as dependências do projeto
npm start # Para iniciar a execução da API
```

Ao rodar esses comandos, a mensagem "API conectada à base de dados" deve aparecer no terminal, o que indica que a aplicação está rodando.
Assim, é só fazer os testes utilizando a ferramenta para testes de API se baseando no formato de rota especificado na seção ***🛣️ Rotas da Aplicação***.
