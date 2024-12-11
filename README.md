Motor Utilizado: SQL Server

Definición de la base de datos:

CREATE DATABASE DBcrudSeminario

USE DBcrudSeminario

CREATE TABLE Producto (
    Id INT IDENTITY(1,1) PRIMARY KEY,  
    Nombre NVARCHAR(255) NOT NULL,    
    Precio FLOAT NULL                 
);
