<h1>Desafio Leds</h1>
<h3>Aluno: Arthur Corona</h3>
<h3>email: coronaggp@gmail.com</h3>
<p>Este projeto implementa um sistema em Node.js que realiza o cruzamento de dados entre candidatos e concursos públicos. A aplicação é capaz de determinar a compatibilidade entre eles com base nas profissões exigidas pelas vagas e nas qualificações dos candidatos.</p>

<h2>Funcionalidades Principais</h2>
<p>Busca por Candidato: Fornecendo o CPF de um candidato, o sistema retorna uma lista de todos os concursos públicos para os quais ele está qualificado.</p>

<p>Busca por Concurso: Fornecendo o Código de um concurso, o sistema retorna uma lista de todos os candidatos qualificados para as vagas ofertadas.</p>

<h2>Tecnologias Utilizadas:</h2>
<p>Node.js: Ambiente de execução para o código JavaScript do lado do servidor.</p>

<p>Jest: Framework de testes para garantir a qualidade e a corretude da lógica de negócio.</p>

<h2>Estrutura do Projeto:</h2>


/
├── index.js                # Ponto de entrada para uso interativo da aplicação
├── package.json            # Metadados do projeto e scripts (npm test)
├── src/
│   ├── data/
│   │   ├── candidatos.txt  # Banco de dados de candidatos em formato de texto
│   │   ├── concursos.txt   # Banco de dados de concursos em formato de texto
│   │   └── database.js     # MÓDULO DE DADOS: Responsável por ler e interpretar os arquivos .txt
│   └── services/
│       └── matchingService.js # MÓDULO DE SERVIÇO: Contém toda a lógica de negócio e otimizações
└── tests/
    └── matchingService.test.js # MÓDULO DE TESTES: Testes unitários para o serviço de matching

<h2>1. Camada de Dados (src/data/database.js)</h2>
Esta camada é responsável por abstrair a origem dos dados. Suas funções (loadCandidatos e loadConcursos) leem os arquivos .txt, processam o texto linha a linha e convertem os dados brutos em arrays de objetos JavaScript estruturados, que podem ser facilmente manipulados pela aplicação.

<h2>2. Camada de Serviço (src/services/matchingService.js)</h2>

<p>Otimização de Performance (Carregamento Único): Ao iniciar, o serviço carrega todos os dados dos arquivos .txt para a memória RAM uma única vez. Isso evita a lentidão de operações de disco repetitivas a cada busca, tornando as consultas subsequentes extremamente rápidas.</p>

<p>Lógica de Matching: As funções find utilizam os dados em memória para realizar o cruzamento. Elas primeiro localizam o registro inicial através do Map (busca rápida) e depois filtram a lista oposta, comparando as profissões com as vagas para encontrar todas as correspondências.</p>

<h2>3. Estratégia de Testes (tests/matchingService.test.js)</h2>
Para garantir a confiabilidade do sistema, foram implementados testes unitários com Jest.

<p>Mocking (Simulação): Os testes não leem os arquivos .txt reais. Em vez disso, eles utilizam a técnica de mocking para simular a camada de dados. Um conjunto pequeno e controlado de dados falsos é fornecido ao serviço durante os testes.</p>

<p>Benefícios: Esta abordagem garante que os testes sejam:</p>

<p>Rápidos: Não dependem da lentidão do sistema de arquivos.</p>

<p>Determinísticos: Os resultados são sempre os mesmos, independentemente dos dados reais.</p>

<p>Isolados: Testa-se apenas a lógica do matchingService, sem o risco de falhas por problemas na leitura de arquivos ou formatação de dados.</p>

Como Executar o Projeto
<p>Pré-requisitos</p>
<p>É necessário ter o Node.js (versão 16 ou superior) instalado.</p>

<h2>1. Instalação de Dependências</h2>
Clone o repositório e, no terminal, dentro da pasta raiz do projeto, execute o seguinte comando para instalar as dependências de desenvolvimento (Jest):

```bash
npm install
```


<h2>2. Execução Interativa</h2>
Para usar a aplicação e fazer consultas com os dados reais dos arquivos .txt, edite os exemplos no arquivo index.js e execute:


```bash
node index.js
```

Os resultados da busca serão exibidos em tabelas no seu terminal.

<h2>3. Execução dos Testes Automatizados</h2>
Para verificar a integridade da lógica de negócio e garantir que tudo está funcionando como esperado, execute o seguinte comando:


```bash
npm test
```
O Jest irá rodar a suíte de testes e exibir um relatório de sucesso.

