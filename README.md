Desafio 🚀

Este projeto foi desenvolvido com .NET 8.0, Angular 20.3 e SQL Server, utilizando Entity Framework como ORM.

📌 Configuração do Projeto

Banco de Dados

Altere as configurações de conexão no arquivo appsettings.json e no docker-compose.yml de acordo com o seu ambiente de banco de dados e Docker.

Arquivo create-database.sql presente em Desafio/init-db/create-database.sql é utilizado para auxiliar na criação do banco de dados e usuario, verifique a necessidade do mesmo.

Migrações

Caso necessário, execute os comandos de migration antes de rodar o projeto.

🧩 Arquitetura e Padrões

Utilização dos princípios S.O.L.I.D, MVC e Clean Code

Separação das funcionalidades em camadas e pastas para melhor organização, manutenção e evolução

Conteinerização com Dockerfile e docker-compose

🧪 Testes

Os testes unitários estão localizados no projeto, em Desafio.Tests, incluindo:

Desafio.Tests/ControllersTeste/TituloControllerTeste.cs

▶️ Como rodar o projeto: 
docker-compose up -d --build

📂 Estrutura de Telas

As telas do sistema preenchidas estão disponíveis em:

Desafio/Desafio/Images-Telas/

Desafio esta em anexo em:

Desafio/Desafio/Desafio.pdf
