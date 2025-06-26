# Sobre o projeto

<<<<<<< HEAD
Este projeto de software, descrito nesta documentação, implementa o Frontend e o Backend de uma aplicação web de **gerenciamento de concursos online e candidatos**, feito baseado nas descrições e requisitos especificados no teste final de admissão proposto pelo **Laboratório de Extensão e Desenvolvimento de Soluções (LEDS)**, programa de extensão ligado ao **Instituto Federal do Espírito Santo (IFES)**.
=======
Este projeto de software, descrito nesta documentação, implementa o Backend de uma aplicação web de **gerenciamento de concursos online e candidatos**, feito baseado nas descrições e requisitos especificados no desafio técnico proposto pelo **Laboratório de Extensão e Desenvolvimento de Soluções (LEDS)**, programa de extensão ligado ao **Instituto Federal do Espírito Santo (IFES)**.
>>>>>>> 2a3173a (docs(LedsAPI): Melhorando estrutura da documentação)


## 🛠️ Tecnologias utilizadas

<<<<<<< HEAD
**Framework:** Node JS
=======
**Framework:** Express JS
>>>>>>> 2a3173a (docs(LedsAPI): Melhorando estrutura da documentação)

**Ling de programação:** Javascript (JS)

**Banco de Dados/RDBMS:** PostgreSQL

**Principais módulos:**
<<<<<<< HEAD
- crypto (pacote usado para criptografia e descriptografia de dados no backend); 
- express (módulo que facilita a criação de aplicações backend em node js e auxilia no gerenciamento de rotas )

# 📌 Modelagem do Projeto
Esta seção apresentará o projeto sob o ponto de vista arquitetural, mostrando sua estrutura, implementações além das esperadas pelo teste proposto e a organização dos módulos e componentes nos diretórios de cada parte da aplicação.
=======
- crypto (pacote usado para criptografia e descriptografia de dados); 
- express (possibilita uso do Express JS)
>>>>>>> 2a3173a (docs(LedsAPI): Melhorando estrutura da documentação)

---


## 💡 Diferenciais de implementação

- **Criptografia de dados:** Para atribuir uma camada a mais de segurança à aplicação, um módulo com métodos de criptografia foi desenvolvido para criptografia e descriptografia dos dados de cada entidade da aplicação. Como apontado na estrutura de diretório do projeto (nas seções *"🏗 Padrão de Arquitetura"* e *"📁 Estrutura de Diretório"*), esse módulo estará na pasta **"config"** com o nome **"criptography.js"**.
<br>

- **Randomização de chave primária com UUID:** As chaves primárias da duas entidades Candidato e Concurso serão randômicas para acrescentar mais segurança no acesso aos dados. Essa randomização dos IDs será feita aplicando o conceito de <u>*UUID*</u> e utilizando um módulo do Javascript especializado nessa randomização (o nome do módulo é <u>*uuidv4*</u>).

---

## 🏗 Padrão de Arquitetura
Esta API possui a arquitetura construída com base na representação da imagem abaixo:

<<<<<<< HEAD
📌 **Imagem do modelo de arquitetura**:
=======
![diagrama de arquitetura do projeto](https://i.imgur.com/2HCnmax.png)

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

>>>>>>> 2a3173a (docs(LedsAPI): Melhorando estrutura da documentação)

## 🐘 Modelagem do Banco de Dados

Tendo em vista o minimundo oferecido pelo desafio técnico, o banco de dados Postgre foi construído com base no modelo representado na imagem abaixo:

![Modelagem do banco de dados](https://i.ibb.co/fdYwNVZw/bd-new.png)

Os dados das entidades Candidato e Concurso serão criptografados utilizando uma geração de chave de criptografia com um valor "*salt*" e um **Vetor de Inicialização (IV)**. Como, na implementação adota por este projeto, o IV é o valor necessário para que haja a descriptografia desses dados, o mesmo será armazenado no banco de dados como entidades relacionadas diretamente às entidades Candidato e Concurso, cada um contendo seu conjunto de IVs randômicos. Vale informar que o IV é único por dado.

Os IDs das entidades Candidato e Concurso, por outro lado, receberão um valor randômico seguindo o conceito de UUID.


---
## 🛣️ Rotas da Aplicação

Considerando as duas entidades principais mencionadas na seção *"🐘 Modelagem de Banco de Dados"** (Candidato e Concurso), as rotas definidas na camada "Routes" foram as especificadas abaixo já na sintaxe do Javascript, linguagem de programação escolhida para desenvolver este projeto:
<br>
> **Para candidato:**
```javascript
    //Rota para cadastro de um novo candidato
    /*params:
        nome: String,
        cpf: String,
        data_nascimento: Date,
        profissoes: Object<String>
    */
    route.post("/cadastrar", candidatoController.cadastrar);

    //Rota para procurar um candidato já cadastrado
    /*params:
        id: String
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
        vagas: Object<String>
    */
    route.post("/registrar_concurso", concursoController.registrarConcurso);

    //Rota para procurar um concurso registrado
    /*params:
        id: String
    */
    route.get("/procurar_concurso", concursoController.procurarConcurso);

    //Rota para listar os concursos compativeis a um candidato
    /*params:
        cpf: String
    */
    route.get("/listar_concursos_compativeis", concursoController.listarConcursosCompativeis);

```
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

