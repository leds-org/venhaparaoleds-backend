# Sobre o projeto

Este projeto de software, descrito nesta documentação, implementa o Backend de uma aplicação web de **gerenciamento de concursos online e candidatos**, feito baseado nas descrições e requisitos especificados no teste final de admissão proposto pelo **Laboratório de Extensão e Desenvolvimento de Soluções (LEDS)**, programa de extensão ligado ao **Instituto Federal do Espírito Santo (IFES)**.


# 🛠️ Tecnologias utilizadas

**Framework:** Node JS

**Ling de programação:** JavaScript (JS)

**Banco de Dados:** Postgres

**Principais módulos:**
- crypto (pacote usado para criptografia e descriptografia de dados no backend); 
- express (módulo que facilita a criação de aplicações backend em node js e auxilia no gerenciamento de rotas )

# 📌 Modelagem do Projeto
Esta seção apresentará o projeto sob o ponto de vista arquitetural, mostrando sua estrutura, implementações além das esperadas pelo teste proposto e a organização dos módulos e componentes nos diretórios de cada parte da aplicação.

---

## 💡 Diferenciais de implementação

- **Criptografia de chave primária (UUID):** Para atribuir uma camada a mais de segurança à aplicação, um módulo com métodos de criptografia foi desenvolvido para criptografia e descriptografia das chaves primárias das entidades da aplicação; nesse caso, para criptografar o campo de CPF da entidade Candidato e o campo de ID da entidade Concurso. Como apontado na estrutura de diretório do projeto (no tópico posterior), esse módulo estará na pasta **"config"** com o nome **"criptography.js"**.

---

## 🏗 Padrão de Arquitetura
Para o Backend da aplicação, é usada uma abordagem personalizada do tipo **"RCSR"(Route-Controller-Service-Repository)**, ilustrada na seguinte imagem:  

📌 **Imagem do modelo de arquitetura**:
![diagrama de arquitetura do projeto](https://github.com/GBLucas1809/gabriel-barbosa-lucas/blob/main/arquitetura_backend.png)



A camada **Route** será responsável por disponibilizar meios para o Frontend se comunicar à API do Backend. O **Controller** fará a mediação e controle dos dados entre as extremidades da comunicação, sendo responsável pelo JSON de resposta às requisições do cliente. O **Service** oferecerá às outras partes do Backend os métodos que acessam a camada **Repository**, responsável por fazer a comunicação direta com o banco de dados e por fazer as operações básicas de CRUD+L (Create, Read, Update, Delete and List) da aplicação. 
 

---


## 📁 Estrutura de Diretórios 
Seguem abaixo as estruturas de diretório de cada lado da aplicação (Frontend e Backend), levando em conta o padrão de arquitetura proposto para cada um em tópicos anteriores:


### (🐘) Backend

```bash
📂 teste-leds-backend
 ┣ 📂 src
 ┃ ┣ 📂 controllers      # Controladores, para mediação entre o acesso da rota pelo front e as camadas de acesso ao bd  
 ┃ ┣ 📂 repositories     # Métodos de acesso direto ao BD (aqui estarão as consultas SQL)
 ┃ ┣ 📂 routes           # Rotas da API   
 ┃ ┣ 📂 services         # Métodos de acesso indireto ao BD (onde terão as regras de negócio e controle de retorno de status)
 ┃ ┗ 📂 config           # Arquivos de configuração e módulos personalizados (ex.: database e criptography)
 ┣ 📜 README.md          # Documentação principal
 ┣ 📜 package.json       # Dependências do projeto
 ┗ 📜 .gitignore         # Arquivos ignorados pelo Git
```

Vale salientar que as estruturas acima ocultam outras pastas secundárias mas relevantes do projeto; como, por exemplo, as pastas com os imports de módulos necessários para cada lado do projeto (node_modules).  
