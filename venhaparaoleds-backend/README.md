Projeto: Sistema de Matching de Concursos (Desafio Leds)
Este projeto implementa um sistema em Node.js que realiza o cruzamento de dados entre candidatos e concursos públicos. A aplicação é capaz de determinar a compatibilidade entre eles com base nas profissões exigidas pelas vagas e nas qualificações dos candidatos.

O sistema foi desenvolvido com foco em performance, código limpo e testabilidade, servindo como uma solução robusta e escalável para o desafio proposto.

✨ Funcionalidades Principais
Busca por Candidato: Fornecendo o CPF de um candidato, o sistema retorna uma lista de todos os concursos públicos para os quais ele está qualificado.

Busca por Concurso: Fornecendo o Código de um concurso, o sistema retorna uma lista de todos os candidatos qualificados para as vagas ofertadas.

🛠️ Tecnologias Utilizadas
Node.js: Ambiente de execução para o código JavaScript do lado do servidor.

Jest: Framework de testes para garantir a qualidade e a corretude da lógica de negócio.

📁 Estrutura do Projeto
O projeto segue uma arquitetura de camadas para garantir a separação de responsabilidades e a manutenibilidade do código.

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
⚙️ Como o Sistema Funciona
A arquitetura foi pensada para ser eficiente e escalável.

1. Camada de Dados (src/data/database.js)
Esta camada é responsável por abstrair a origem dos dados. Suas funções (loadCandidatos e loadConcursos) leem os arquivos .txt, processam o texto linha a linha e convertem os dados brutos em arrays de objetos JavaScript estruturados, que podem ser facilmente manipulados pela aplicação.

2. Camada de Serviço (src/services/matchingService.js)
Este é o cérebro da aplicação e contém as otimizações de performance mais importantes.

Otimização de Performance (Carregamento Único): Ao iniciar, o serviço carrega todos os dados dos arquivos .txt para a memória RAM uma única vez. Isso evita a lentidão de operações de disco repetitivas a cada busca, tornando as consultas subsequentes extremamente rápidas.

Indexação de Dados com Map: Para acelerar as buscas por um candidato ou concurso específico, os dados são pré-indexados em Maps do JavaScript. Um Map permite uma busca por chave (CPF ou Código do Concurso) com performance de tempo constante (O(1)), o que é drasticamente mais rápido do que percorrer um array inteiro (O(n)), especialmente com milhares de registros.

Lógica de Matching: As funções find... utilizam os dados em memória para realizar o cruzamento. Elas primeiro localizam o registro inicial através do Map (busca rápida) e depois filtram a lista oposta, comparando as profissões com as vagas para encontrar todas as correspondências.

3. Estratégia de Testes (tests/matchingService.test.js)
Para garantir a confiabilidade do sistema, foram implementados testes unitários com Jest.

Mocking (Simulação): Os testes não leem os arquivos .txt reais. Em vez disso, eles utilizam a técnica de mocking para simular a camada de dados. Um conjunto pequeno e controlado de dados falsos é fornecido ao serviço durante os testes.

Benefícios: Esta abordagem garante que os testes sejam:

Rápidos: Não dependem da lentidão do sistema de arquivos.

Determinísticos: Os resultados são sempre os mesmos, independentemente dos dados reais.

Isolados: Testa-se apenas a lógica do matchingService, sem o risco de falhas por problemas na leitura de arquivos ou formatação de dados.

🚀 Como Executar o Projeto
Pré-requisitos
É necessário ter o Node.js (versão 16 ou superior) instalado.

1. Instalação de Dependências
Clone o repositório e, no terminal, dentro da pasta raiz do projeto, execute o seguinte comando para instalar as dependências de desenvolvimento (Jest):

Bash

npm install
2. Execução Interativa
Para usar a aplicação e fazer consultas com os dados reais dos arquivos .txt, edite os exemplos no arquivo index.js e execute:

Bash

node index.js
Os resultados da busca serão exibidos em tabelas no seu terminal.

3. Execução dos Testes Automatizados
Para verificar a integridade da lógica de negócio e garantir que tudo está funcionando como esperado, execute o seguinte comando:

Bash

npm test
O Jest irá rodar a suíte de testes e exibir um relatório de sucesso.

🌱 Próximos Passos e Melhorias
Este projeto serve como uma base sólida que pode ser expandida. Algumas sugestões de melhorias incluem:

Criar uma API REST: Utilizar o framework Express.js para expor as funcionalidades como endpoints HTTP.

Adotar um Banco de Dados Real: Migrar os dados dos arquivos .txt para um sistema de banco de dados como SQLite (para simplicidade) ou PostgreSQL (para robustez).

Construir uma Interface de Usuário: Desenvolver um front-end (React, Vue, etc.) para consumir a API e proporcionar uma experiência mais amigável.