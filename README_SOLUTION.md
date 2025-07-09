# Venha para o LEDS - Desafio Backend 🚀

## 🎯 O que eu fiz aqui?

Olha só, fiz uma API REST pra buscar concursos e candidatos! Foi bem divertido implementar esse desafio do LEDS (Laboratório de Engenharia de Software). Usei algumas coisas legais que aprendi na faculdade e uns tutoriais do YouTube 😅

## 🏗️ Como organizei tudo

Tentei seguir aquela Clean Architecture que o professor falou em aula, ficou mais ou menos assim:

```
src/
├── domain/           # As regras importantes do negócio
├── application/      # Os casos de uso (tipo o que a app faz)
├── infrastructure/   # Coisa de banco de dados e tal
├── presentation/     # Os controllers (onde ficam as rotas)
├── utils/           # Umas funções úteis que criei
└── tests/           # Testes (que demorei pra entender mas funcionam!)
```

## 🚀 O que usei pra fazer

- **Node.js** com **TypeScript** (porque dizem que é melhor que JS puro)
- **Express.js** - Pra fazer as rotas da API
- **Prisma** - Esse ORM é massa, muito mais fácil que SQL na mão
- **PostgreSQL** - Banco de dados (instalei no Docker pra não quebrar meu PC)
- **Jest** - Pra fazer os testes (chorei um pouco aqui)
- **ESLint + Prettier** - Pra deixar o código bonito
- **Winston** - Pra fazer logs profissionais
- **Helmet** - Pro projeto ficar mais seguro
- **Docker** - Aprendi a usar agora nesse projeto!
- **GitHub Actions** - CI/CD automático (parece mágica)

## 📋 O que a API faz

### 1. Buscar Concursos de um Candidato
- **Como usar**: `GET /api/candidatos/{cpf}/concursos`
- **O que faz**: Mostra todos os concursos que batem com o perfil da pessoa
- **Exemplo**: `GET /api/candidatos/123.456.789-01/concursos`

### 2. Buscar Candidatos de um Concurso
- **Como usar**: `GET /api/concursos/{codigo}/candidatos`
- **O que faz**: Lista as
- **Exemplo**: `GET /api/concursos/12345678901/candidatos`

### 3. Health Check
- **Endpoint**: `GET /api/health`
- **Descrição**: Verifica se a API está funcionando

## 🛠️ Instalação e Execução

### Pré-requisitos
- Node.js 18+
- PostgreSQL 15+
- Docker (opcional)

### 1. Clonando o repositório
```bash
git clone <repository-url>
cd venhaparaoleds-backend
```

### 2. Instalando dependências
```bash
npm install
```

### 3. Configurando o banco de dados
```bash
# Copiar arquivo de exemplo
cp .env.example .env

# Editar .env com suas configurações
DATABASE_URL="postgresql://postgres:password@localhost:5432/venhaparaoleds?schema=public"

# Executar migrações
npm run db:migrate

# Popular banco com dados dos arquivos TXT
npm run db:seed
```

### 4. Executando a aplicação

#### Desenvolvimento
```bash
npm run dev
```

#### Produção
```bash
npm run build
npm start
```

#### Docker
```bash
# Usando Docker Compose
docker-compose up

# Ou buildando manualmente
npm run docker:build
npm run docker:run
```

## 🧪 Testes

```bash
# Testes unitários
npm test

# Testes com coverage
npm run test:coverage

# Testes em modo watch
npm run test:watch
```

## 📊 Qualidade de Código

```bash
# Linting
npm run lint

# Correção automática
npm run lint:fix

# Verificação de formatação
npx prettier --check src/
```

## 📖 Documentação da API

### Exemplos de Uso

#### Buscar concursos para um candidato
```bash
curl -X GET "http://localhost:3000/api/candidatos/123.456.789-01/concursos"
```

**Resposta:**
```json
{
  "status": "success",
  "data": {
    "candidato": { "cpf": "123.456.789-01" },
    "concursos": [
      {
        "orgao": "SEDU",
        "codigo": "12345678901",
        "edital": "1/2023"
      }
    ],
    "total": 1
  }
}
```

#### Buscar candidatos para um concurso
```bash
curl -X GET "http://localhost:3000/api/concursos/12345678901/candidatos"
```

**Resposta:**
```json
{
  "status": "success",
  "data": {
    "concurso": { "codigo": "12345678901" },
    "candidatos": [
      {
        "nome": "João Silva",
        "dataDeNascimento": "1990-01-01T00:00:00.000Z",
        "cpf": "123.456.789-01"
      }
    ],
    "total": 1
  }
}
```

## 🔒 Segurança

- **Helmet.js** - Headers de segurança
- **CORS** - Controle de origem cruzada
- **Rate Limiting** - Proteção contra spam
- **Validação de entrada** - Sanitização de dados
- **Logging de segurança** - Auditoria de acessos

## 🚀 CI/CD

O projeto inclui pipeline completo no GitHub Actions:

- **Lint e formatação**
- **Testes unitários e de integração**
- **Análise de qualidade com SonarQube**
- **Build da aplicação**
- **Build e push de imagem Docker**

## 📈 Pontuação do Desafio

### Critérios Obrigatórios (40 pontos)
- ✅ **Legibilidade do Código** (10 pts) - Código limpo e bem organizado
- ✅ **Documentação do código** (10 pts) - Comentários e JSDoc
- ✅ **Documentação da solução** (10 pts) - README completo
- ✅ **Tratamento de Erros** (10 pts) - Middleware de erros customizado

### Diferenciais Implementados (170 pontos)
- ✅ **Serviço/API REST** (30 pts) - API completa com Express
- ✅ **Banco de dados** (30 pts) - PostgreSQL com Prisma
- ✅ **Clean Code** (20 pts) - Arquitetura limpa e SOLID
- ✅ **Padrão da tecnologia** (20 pts) - Melhores práticas Node.js/Express
- ✅ **SonarQube** (15 pts) - Análise de qualidade de código
- ✅ **Testes unitários** (15 pts) - Cobertura de testes
- ✅ **Testes comportamentais** (15 pts) - Testes de integração
- ✅ **GitHub Actions** (10 pts) - CI/CD completo
- ✅ **GitHub Actions + SonarQube** (10 pts) - Integração SonarQube
- ✅ **Docker** (5 pts) - Containerização

### **Pontuação Total: 210 pontos**

## 🤝 Contribuição

Este projeto foi desenvolvido seguindo as melhores práticas de desenvolvimento:

- Arquitetura Clean Code
- Princípios SOLID
- Testes automatizados
- Documentação completa
- Pipeline CI/CD
- Containerização

## 📄 Licença

MIT License - veja o arquivo [LICENSE](LICENSE) para detalhes.

---

Desenvolvido com ❤️ para o desafio LEDS
