# Desafio Backend - LEDS

*Por Lucas Cunha de Souza*

## Visão geral

Solução desenvolvida para o Desafio LEDS Academy, com o objetivo de desenvolver o desafio proposto e atender aos critérios de avaliação estabelecidos.

A aplicação lhe oferece duas opções de utilização que lhe permite:

1. Buscar concursos públicos compatíveis com as profissões de um candidato, com base em seu CPF.
2. Buscar candidatos compatíveis com as vagas de um concurso, com base no código do concurso.

## Tecnologias utilizadas 

- .NET 9 Framework
- C#


## Diferenciais utilizados

- Implementar o padrão de programação da tecnologia escolhida.

## Justificativa de escolha

Na solução desenvolvida, optamos por utilizar os padrões de projeto Template Method e Strategy, que são padrões de projeto de software que visam a reutilização de código e a organização de classes.

O padrão Template Method é um padrão de projeto comportamental que define o esqueleto de um algoritmo na superclasse, mas permite que as subclasses substituam etapas específicas desse algoritmo sem alterar sua estrutura.

Quanto ao Strategy, é um padrão de projeto comportamental que permite definir uma família de algoritmos, encapsular cada um deles e torná-los intercambiáveis. O Strategy permite que o algoritmo varie independentemente dos clientes que o utilizam, e ainda permite algumas comodidades, como por exemplo, ao usar junto de Reflector, é possível criar um menu automático através de todas as classes que herdam da classe OpcaoMenu.

Em algumas etapas, foi realizada uma escolha de não seguir a risca o Princípio de Responsabilidade Única, aja vista que criação de classes para cada responsabilidade, como a impressão das informações obtidas da busca, pouco agregaria ao projeto além de interpretar que faz parte do escopo do das classes opções, a realização da busca.

## Alimentando o projeto com dados

Como os arquivos candidatos.txt e concursos.txt estão originalmente vazios, tomei a liberdade de preenchê-los com dados fictícios para que fosse possível realizar os testes. Na minha interpretação, um arquivo json, apesar de mais simples de ser manipulado, foge do esperado para um txt, dessa forma, optei por alimentá-lo com linhas cujo conteúdo é separado por ponto-e-vírgula (;), além da lista de vagas e profissões estar delimitada por colchetes ([]) e as vagas e profissões separadas por vírgula (,).

Segue linha de exemplo para utilização:

```candidatos.txt
João Silva;15/05/1990;12345678901;[professor de matemática, assistente administrativo]
```

```concursos.txt
SEDU;123/2023;12345678901;[professor de inglês, assistente administrativo, analista de sistemas]
```

## Executando o projeto

Para executar o projeto, é necessário seguir o passo-a-passo descrito a baixo.

1. Clone o repositório em sua máquina local.
Abra o terminal e execute o comando:
```bash
git clone https://github.com/project-lc/venhaparaoleds-backend.git
```

2. Acesse a pasta do projeto.
```bash
cd venhaparaoleds-backend
```

3. Verifique a instalação do .NET 9
Existem diversas formas de verificar se o .NET 9 está instalado em sua máquina, a Microsoft disponibiliza algumas dessas maneiras [nesse site](https://learn.microsoft.com/pt-br/dotnet/core/install/how-to-detect-installed-versions?pivots=os-windows).

Caso não disponha, segue o link para instalação do .NET 9 [aqui](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0).

4. Restaure os pacotes do projeto.
```bash
dotnet restore
```

5. Execute o projeto.
```bash
dotnet run
```

E pronto! O projeto estará rodando em sua máquina local. Começará imprimindo o menu de opções e aguardará a sua escolha.