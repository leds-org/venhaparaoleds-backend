
# Desafio Backend - Leds: Matching de Concursos e Candidatos

**Candidato**: Artur Pedro Carvalho

**Email**: arturcarvalho05@gmail.com

---
### Visão Geral

Esta solução busca relacionar candidatos e concursos públicos através da compatibilidade de profissões, implementando:

- API REST com endpoints para matching

- Arquitetura MVC adaptada:
    - Controllers: Gerenciam requisições/respostas HTTP

    - Services: Lógica de negócio (regras de matching)

    - Entities/Model: Modelagem dos dados (JPA/Hibernate)

    - Repository: Acesso aos dados.

    - Sem Camada View, sendo substituída por DTOs estruturados


---

### Tecnologias Utilizadas

- Java 17

- Spring Boot 3.5.3

- H2 Database

---

### Ferramentas e Bibliotecas

- Maven 4.0

- Lombok (Redução de boilerplate)

- Spring Data JPA

- JUnit 5 + Mockito

- IntelliJ IDEA (recomendada)
---
# Lógica de Matching (Buscas)


O sistema utiliza o mesmo critério de compatibilidade para ambas as direções (candidato→concurso e concurso→candidato), baseado na intersecção de profissões. Para exemplificar, analisemos o fluxo de compatibilidade de Concurso → Candidatos.
 
**Buscar Candidatos:** 

   - O sistema recebe o **código** do concurso que deseja procurar candidatos compativeis.
   - Passa o código recebido, como parâmetro para o método `buscarCandidatosCompativeisPorCodigoConcurso()` que está dentro da classe `MatchingService`(responsavel por cuidar da compatibilidade) e aguarda a resposta.
```java
    @GetMapping(value = {"/{codigo}/candidatos"})
    public ResponseEntity<List<CandidatoResponseDTO>> buscarCandidatos(@PathVariable Long codigo) {
        return ResponseEntity.ok(matchingService.buscarCandidatosCompativeisPorCodigoConcurso(codigo));
    }
 ```
- Na MatchingService, busca-se o Concurso que tem o Codigo recebido;
- Captura somente sua lista de profissoes;
- Envia essa lista para o método ```buscarPorProfissoes()```contida na ```CandidatoService``` e espera o retorno de uma Lista contendo Dtos de Candidatos. 

```java
    public List<CandidatoResponseDTO> buscarCandidatosCompativeisPorCodigoConcurso(Long codigo) {
    List<Concurso> concursos = concursoService.buscarPorCodigo(codigo);
    Set<String> profissoes = concursos.stream()
            .flatMap(concurso -> concurso.getProfissoes().stream())
            .collect(Collectors.toSet());

    List<CandidatoResponseDTO> candidatos = candidatoService.buscarPorProfissoes(profissoes);
```

- No ```CandidatoService```, repassa essa lista ao ```CandidatoRepository``` aguarda o repository retornar com uma Lista de Candidatos, para depois retornar essa mesma lista tranformada em uma lista de objetos DTOs

```java
    public List<CandidatoResponseDTO> buscarPorProfissoes(Set<String> profissoes){
        List<Candidato> candidatos = candidatoRepository.buscarCandidatosCompativeis(profissoes);
        return candidatos.stream().map(CandidatoMapper::toDTO).toList();
    }
```

- Por fim, o ```CandidatoRepository``` realiza uma consulta SQL (Query), que busca por candidatos que tenham a determinada lista de profissoes em comum, e retorna todos esses candidatos ao CandidatoService, gerando uma cadeia de retornos.
```java
    @Query("Select c From Candidato c JOIN c.profissoes p WHERE p IN :profissoes")
    List<Candidato> buscarCandidatosCompativeis(@Param("profissoes") Set<String> profissoes); 
```


---
### Endpoints da API

Buscar concursos compatíveis com um candidato

```http 
GET /candidatos/{cpf}/concursos
```

Descrição: Retorna todos os **concursos** públicos compatíveis com a profissão do candidato informado pelo CPF, no seguinte formato de exemplo:
```json
[
    {
        "orgão": "SEDU",
        "edital": "9/2016",
        "codigo": "61828450843"
    },
    {
        "orgão": "SEJUS",
        "edital": "15/2017",
        "codigo": "61828450843"
    }
]
```


---
Buscar candidatos compatíveis com um concurso

```http
GET /concursos/{codigo}/candidatos
```

Descrição: De forma análoga retorna todos os **candidatos** que têm profissões compatíveis com o concurso de código informado.

```json
[
    {
        "nome": "Lindsey Craft",
        "dataNascimento": "19/05/1976",
        "cpf": "182.845.084-34"
    },
    {
        "nome": "Jackie Dawson",
        "dataNascimento": "14/08/1970",
        "cpf": "311.667.973-47"
    }
]
```
---
### Tratamento de Erros

A API utiliza respostas customizadas para erro com, HTTP Status codes específicos e um corpo de resposta.

**Exceptions customizadas**

| **Código** | **Situação**                               | **Mensagem de Erro** | **Exemplo de requisição**               |
|------------|--------------------------------------------|-----------------|-----------------------------------------|
| 204        | Não existem concursos compativeis          |   | GET /candidatos/12345678900/concursos   |
| 204        | Não existem candidatos compativeis         |   | GET /concursos/987654321912/candidadots |
| 404        | Candidato com o CPF informado não existe   | Cpf não foi encontrado. | GET /candidatos/12345678900/concursos   |   
| 404        | Concurso com o Codigo informado não existe | Concurso não foi encontrado. | GET /concursos/987654321912/candidatos  |
| 400        | Cpf invalido                               | Cpf invalido.   | GET /candidatos/abc123/concursos        |
| 400        | Código invalido                            | Codigo invalido. | GET /concursos//candidatos              |




---
### Banco de Dados
- Utiliza H2 em memória (os dados são resetados a cada execução)
- DataLoader Inicial:
    - Popula automaticamente candidatos e concursos a partir dos arquivos `candidatos.txt` e `concursos.txt`

---
### Testes Unitários
- Junit 5 + Mockito (Nativos do Spring Boot Test)
- Validação de exceções customizadas e principais funcionalidades

Para rodar os testes:
```bash
./mvnw test
```

---
### Como Executar

Pré-Requisitos
- Git
- Java 17+
- Maven

Clone o projeto:

```bash
git clone https://github.com/artcarvalho/venhaparaoleds-backend.git
cd venhaparaoleds-backend
```

Execute com Maven:

```bash
./mvnw spring-boot:run
```
A aplicação estará acessível em: http://localhost:8080

