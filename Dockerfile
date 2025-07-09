# Usando uma imagem base do Node.js
# Uso de Alpine para uma imagem menor
FROM node:20-alpine

# Definindo o diretório de trabalho dentro do container
WORKDIR /usr/src/app

# Copiando os arquivos package.json e package-lock.json para o diretório de trabalho

# Vantagem: Otimização de cache p/ instalação de dependências
COPY package*.json ./

# Instalando as dependências do projeto
RUN npm install

# Copiando o restante dos arquivos do projeto para o diretório de trabalho
COPY . .

# Expondo a porta 3000, que é a porta padrão do servidor Node.js
EXPOSE 3000

# Comando para iniciar a aplicação Node.js quando o container for iniciado
CMD ["npm", "start"]