Documentação do Projeto: Sistema de Matching de Concursos
1. Visão Geral
Este projeto é uma aplicação Node.js projetada para resolver o "Desafio Leds". Sua principal função é cruzar informações entre um banco de dados de candidatos e um de concursos públicos, ambos armazenados em arquivos de texto (.txt).

O sistema oferece duas funcionalidades centrais:

Dado o CPF de um candidato, listar todos os concursos para os quais ele está qualificado.

Dado o Código de um concurso, listar todos os candidatos qualificados para ele.

A aplicação foi desenvolvida com foco em performance, organização e qualidade de código, incluindo testes automatizados para garantir a confiabilidade da lógica de negócio.

2. Tecnologias Utilizadas
Node.js: Ambiente de execução para o código JavaScript no servidor.

Jest: Framework para a criação e execução dos testes unitários automatizados.

3. Estrutura do Projeto
O código está organizado em uma arquitetura de camadas para separar as responsabilidades:

/
├── index.js                # Ponto de entrada da aplicação (para uso interativo)
├── package.json            # Define as dependências e scripts do projeto
├── src/
│   ├── data/
│   │   ├── candidatos.txt  # "Banco de dados" de candidatos
│   │   ├── concursos.txt   # "Banco de dados" de concursos
│   │   └── database.js     # Módulo responsável por LER e PARSEAR os arquivos .txt
│   └── services/
│       └── matchingService.js # Cérebro da aplicação, contém a lógica de negócio
└── tests/
    └── matchingService.test.js # Testes unitários para o serviço de matching
4. Como o Código Funciona
O fluxo de dados e a lógica da aplicação podem ser entendidos em três partes principais:

4.1. Camada de Dados (src/data/database.js)
Responsabilidade: Atuar como a ponte entre os dados brutos (arquivos .txt) e a aplicação.

Funcionamento: Ele contém funções (loadCandidatos, loadConcursos) que leem os arquivos de texto, quebram cada linha e transformam o texto em um formato estruturado que o JavaScript entende (um array de objetos). A lógica de parsing foi construída para ser resiliente a pequenas variações de formatação nos arquivos.

4.2. Camada de Serviço (src/services/matchingService.js)
Este é o coração da aplicação, onde a "mágica" acontece. Ele foi otimizado para performance.

Otimização de Performance: Ao ser iniciado, o serviço carrega uma única vez todos os dados dos arquivos para a memória RAM. Isso evita a lentidão de ter que ler os arquivos do disco a cada nova busca, tornando a aplicação extremamente rápida, mesmo com arquivos grandes.

Indexação com Map: Após carregar os dados, o serviço os pré-indexa em estruturas de dados do tipo Map. Um Map funciona como o índice de um livro, permitindo uma busca por chave (CPF ou Código do Concurso) de forma praticamente instantânea (complexidade O(1)), em vez de ter que percorrer a lista inteira (complexidade O(N)).

Lógica de Matching: As funções findContestsForCandidate e findCandidatesForContest implementam a lógica de cruzamento. Após encontrar o candidato ou concurso inicial usando o índice Map, elas percorrem a lista oposta, comparando as profissões com as vagas disponíveis para encontrar as correspondências.

4.3. Testes Automatizados (tests/matchingService.test.js)
Qualidade e Confiança: A pasta tests contém os testes unitários que garantem que a lógica do matchingService está funcionando corretamente.

Isolamento com Mocking: Os testes não dependem dos arquivos .txt reais. Em vez disso, eles usam a técnica de "mocking" (jest.mock). Nós "enganamos" o serviço durante o teste, fornecendo a ele um conjunto de dados pequeno, falso e controlado. Isso garante que os testes sejam:

Rápidos: Não há leitura de disco.

Previsíveis: Os resultados são sempre os mesmos.

Isolados: Um teste da lógica de matching não falhará por um erro na leitura do arquivo, por exemplo.

5. Como Executar o Projeto
Pré-requisitos
Ter o Node.js instalado.

Passo a Passo
Instalar as Dependências:
No terminal, na raiz do projeto, execute o comando para instalar o Jest:

Bash

npm install
Executar a Aplicação (Uso Interativo):
Para fazer buscas com CPFs e códigos específicos (usando os dados dos arquivos .txt), edite os valores de exemplo no arquivo index.js e execute:

Bash

node index.js
O resultado será exibido em tabelas no terminal.

Executar os Testes Automatizados:
Para verificar se toda a lógica de negócio está funcionando corretamente de acordo com os casos de teste definidos, execute:

Bash

npm test
A saída deve mostrar que todos os testes passaram com sucesso.