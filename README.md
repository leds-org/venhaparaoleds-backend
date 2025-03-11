# Desafio Backend - LEDS

*Por Lucas Cunha de Souza*

## Visão geral

Solução desenvolvida para o Desafio LEDS Academy, com o objetivo de desenvolver o desafio proposto e atender aos critérios de avaliação estabelecidos.

A aplicação lhe oferece duas opções de utilização que lhe permite:

1. Buscar concursos públicos compatíveis com as profissões de um candidato, com base em seu CPF.
2. Buscar candidatos compatíveis com as vagas de um concurso, com base no código do concurso.

### Tecnologias utilizadas

- .NET 9 Framework
- C#


### Diferenciais utilizados

- Implementar o padrão de programação da tecnologia escolhida.

### Justificativa de escolha

Na solução desenvolvida, optamos por utilizar os padrões de projeto Template Method e Strategy, que são padrões de projeto de software que visam a reutilização de código e a organização de classes.

O padrão Template Method é um padrão de projeto comportamental que define o esqueleto de um algoritmo na superclasse, mas permite que as subclasses substituam etapas específicas desse algoritmo sem alterar sua estrutura.

Quanto ao Strategy, é um padrão de projeto comportamental que permite definir uma família de algoritmos, encapsular cada um deles e torná-los intercambiáveis. O Strategy permite que o algoritmo varie independentemente dos clientes que o utilizam, e ainda permite algumas comodidades, como por exemplo, ao usar junto de Reflector, é possível criar um menu automático através de todas as classes que herdam da classe OpcaoMenu.

Em algumas etapas, foi realizada uma escolha de não seguir a risca o Princípio de Responsabilidade Única, aja vista que criação de classes para cada responsabilidade, como a impressão das informações obtidas da busca, pouco agregaria ao projeto além de interpretar que faz parte do escopo do das classes opções, a realização da busca.

### Alimentando o projeto com dados

Como os arquivos candidatos.txt e concursos.txt estão originalmente vazios, tomei a liberdade de preenchê-los com dados fictícios para que fosse possível realizar os testes. Na minha interpretação, um arquivo json, apesar de mais simples de ser manipulado, foge do esperado para um txt, dessa forma, optei por alimentá-lo com linhas cujo conteúdo é separado por ponto-e-vírgula (;), além da lista de vagas e profissões estar delimitada por colchetes ([]) e as vagas e profissões separadas por vírgula (,).

Segue linha de exemplo para utilização:

```candidatos.txt
João Silva;15/05/1990;12345678901;[professor de matemática, assistente administrativo]
```

```concursos.txt
SEDU;123/2023;12345678901;[professor de inglês, assistente administrativo, analista de sistemas]
```

### Executando o projeto

Para executar o projeto, é necessário dar clone no repositorio e executar o comando `dotnet run` no terminal, dentro da pasta do projeto.

###

<br clear="both">

<div align="center">
  <a href="https://www.linkedin.com/school/ledsifes" target="_blank">
    <img src="https://img.shields.io/static/v1?message=LinkedIn&logo=linkedin&label=&color=0077B5&logoColor=white&labelColor=&style=for-the-badge" height="40" alt="linkedin logo"  />
  </a>
  <a href="https://www.instagram.com/ledsifes/" target="_blank">
    <img src="https://img.shields.io/static/v1?message=Instagram&logo=instagram&label=&color=E4405F&logoColor=white&labelColor=&style=for-the-badge" height="40" alt="instagram logo"  />
  </a>
  <a href="https://www.youtube.com/@ledsifes/?sub_confirmation=1" target="_blank">
    <img src="https://img.shields.io/static/v1?message=Youtube&logo=youtube&label=&color=FF0000&logoColor=white&labelColor=&style=for-the-badge" height="40" alt="youtube logo"  />
  </a>
</div>

###