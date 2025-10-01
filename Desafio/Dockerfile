# 1️⃣ Build usando SDK do .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código e publica
COPY . ./
RUN dotnet publish -c Release -o /app/out

# 2️⃣ Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

# Porta padrão
EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "Desafio.dll"]
