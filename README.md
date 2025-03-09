# Desafio Backend - LEDS
**Candidato:** Jesse Paiva Carvalho Junior  
**Email:** jessepcarvalhojunior02@gmail.com

---

## Visão Geral
Solução desenvolvida para o Desafio LEDS Academy, com objetivo de demonstrar habilidades em desenvolvimento backend. 

A aplicação oferece uma API para realizar buscas entre candidatos e concursos públicos, de acordo com os seguintes endpoints:

- **Listar concursos por CPF do candidato**: `/api/candidato/concursos/{cpf}`
- **Listar candidatos por Código do Concurso**: `/api/concurso/candidatos/{codigoConcurso}`

---

## Tecnologias (Pré-requisitos)
- **[JDK 21](https://www.oracle.com/br/java/technologies/downloads/#java21)**
- **Spring Boot 3.4.3**
- **PostgreSQL 16.3**
- **Maven**

---

## Diferenciais
- **API** (Criar um serviço com o problema)
- **Banco de Dados** (Utilizar banco de dados)
- **Padrão da Tech** (Implementar o padrão de programação da tecnologia escolhida)

---
## Endpoints

1. **Listar concursos por CPF do candidato**  
   Retorna órgãos, códigos e editais dos concursos com base no CPF do candidato.
    ```java
    @GetMapping("/concursos/{cpf}")
    public ResponseEntity<Object> findCandidatoByCpf(@PathVariable String cpf) {
        if (cpf == null || cpf.length() != 11 || !cpf.matches("\\d+")) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("O CPF deve conter exatamente 11 números");
        }

        List<Concurso> concursos = candidatoService.findConcursoByCpf(cpf);

        if (concursos == null || concursos.isEmpty()) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("Nenhum concurso encontrado para o CPF informado");
        }

        return ResponseEntity.ok(concursos);
    }
   ```
2. **Listar candidatos por Código do Concurso**  
   Retorna nome, data de nascimento e CPF dos candidatos compatíveis com o concurso, baseado no código do concurso.
   ```java
    @GetMapping("/candidatos/{codigoConcurso}")
    public ResponseEntity<Object> findByCodigoConcurso(@PathVariable String codigoConcurso) {
        if (codigoConcurso == null || codigoConcurso.length() != 11 || !codigoConcurso.matches("\\d+")) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("O código deve conter 11 digitos");
        }

        Set<Candidato> candidatos = concursoService.findCandidatoByCodigoConcurso(codigoConcurso);

        if (candidatos == null || candidatos.isEmpty()) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("Nenhum candidato encontrado para o concurso de código: " + codigoConcurso);
        }

        return ResponseEntity.ok(candidatos);
    }
    ```
   
---

## Configuração
1. **Clonar o repositório**
    ``` git
    git clone https://github.com/jessejuniordev/venhaparaoleds-backend/desafioleds
    cd desafioleds
    ```
2. **Configurar o banco de dados**  
   Certifique-se de que o **PostgreSQL** está rodando!  
   Rode o script para criação do banco
   ```
   src/main/resources/schema.sql
    ```
   
3. **Configurar as credenciais no** ```application.yml```   
    Edite ```src/main/resources/application.yml``` com suas credenciasi do **PostgresSQL**
    ```yml
    spring:
        datasource:
          url: jdbc:postgresql://localhost:5432/desafioleds
          username: seu_usuario
          password: sua_senha
   ```
4. **Executar a aplicação**   
Lembrando que para executar com o maven, é necessário ter a versão do **JDK 21** instalada corretamente em sua máquina. [Instalação JDK 21](https://www.oracle.com/br/java/technologies/downloads/#java21)  
#### Compilar o projeto   
``` mvn clean install -DskipTests ```   

Rode o projeto no arquivo ```Application```

A **API** estará disponível em ```http://localhost:8080```

---

## Autor
Jesse Paiva Carvalho Junior   
jessepcarvalhojunior02@gmail.com   
[LinkedIn](linkedin.com/in/jessejuniordev) | [Whatsapp](wa.me/27996160231)
