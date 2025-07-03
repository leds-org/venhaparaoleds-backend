# Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

# Imagem runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT [ "dotnet" , "VenhaParaOLEDS.dll" ]

# O que esse arquivo faz?
# - Usa a imagem oficial do skd para compilar o app
# - Faz a publicação da aplicação em modo Release
# - Em seguinda uma imagem leve runtime só para rodar a API
# - Finaliza definindo o ponto de entrada da aplicação