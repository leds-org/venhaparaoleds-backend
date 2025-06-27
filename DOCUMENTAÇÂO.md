# 💡 Projeto Leds API

API REST desenvolvida com **Spring Boot 3**, **JPA**, **Lombok** e **H2 Database**, responsável por gerenciar candidatos, concursos públicos e relacioná-los com base em suas profissões e vagas.

---
src/
 └── main/
     ├── java/com/leds/leds/
     │   ├── models/         -> Entidades (Candidatos, Concursos, Vagas, Profissao)
     │   ├── dtos/           -> DTOs para retorno de API
     │   ├── repositories/   -> Interfaces JPA
     │   ├── services/       -> Interface de serviço
     │   ├── IMPL/           -> Implementações da lógica
     │   └── controllers/    -> Endpoints REST
     └── resources/
         ├── application.properties
         └── data.sql        -> Dados para popular o banco dinâmico (opcional)


## 📦 Tecnologias e Dependências

- **Java 17**
- **Spring Boot 3.5.3**
  - spring-boot-starter-web
  - spring-boot-starter-data-jpa
- **H2 Database** (banco em memória)
- **Lombok** (anotações como `@Data`, `@Builder`, etc.)
- **Spring Boot DevTools** (auto-restart para desenvolvimento)

---

## ▶️ Endpoints
GET /candidatos
GET /candidatos/cpf
GET /concursos
GET /concursos/codigo

### ✅ Pré-requisitos

- Java 17 instalado
- Maven 3+ instalado

### 🛠️ Passos

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/leds.git
cd leds

# 2. Compile e rode o projeto
./mvnw spring-boot:run

A aplicação iniciará por padrão em:
📍 http://localhost:8080

