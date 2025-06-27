# 💡 Projeto Leds API

API REST desenvolvida com **Spring Boot 3**, **JPA**, **Lombok** e **H2 Database**, responsável por gerenciar candidatos, concursos públicos e relacioná-los com base em suas profissões e vagas.

---


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

GET /candidatos/cpf?cpf=xxx.xxx.xxx-xx

GET /concursos

GET /concursos/codigo?codigo=xxxxxxxxxxx

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



[
{
id: 1,
orgao: "SEDU",
edital: "9/2016",
codigoDoConcurso: "61828450843",
listaDeVagas: [
{
id: 1,
cargo: "carpinteiro"
},
{
id: 2,
cargo: "analista de sistemas"
},
{
id: 3,
cargo: "marceneiro"
}
]
},
{
id: 4,
orgao: "IASES",
edital: "14/2016",
codigoDoConcurso: "74078423976",
listaDeVagas: [
{
id: 1,
cargo: "professor de matemática"
}
]
},...



