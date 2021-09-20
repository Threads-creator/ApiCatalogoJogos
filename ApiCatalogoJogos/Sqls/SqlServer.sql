CREATE DATABASE CatalogoJogos;

CREATE TABLE Jogos (
	Id char(40) NOT NULL,
	Nome varchar(101) NOT NULL,
	Produtora varchar(101) NOT NULL,
	Preco decimal(7,2) NOT NULL

	CONSTRAINT pk_Jogos PRIMARY KEY(Id)

);