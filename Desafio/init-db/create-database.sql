-- Cria o banco Desafio
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Desafio')
BEGIN
    CREATE DATABASE Desafio;
END
GO

-- Garante que o sa tenha acesso
USE Desafio;
GO

ALTER LOGIN sa WITH PASSWORD = 'SuaSenhaForte123!';
GO

ALTER AUTHORIZATION ON DATABASE::Desafio TO sa;
GO
