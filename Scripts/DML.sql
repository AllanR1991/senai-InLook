USE inLock_games_manha;

INSERT INTO dbo.TipoUsuario VALUES
	('ADMINISTRADOR'),
	('CLIENTE')

INSERT INTO dbo.Usuario VALUES
	(1,'admin@admin.com ','admin'),
	(2,'cliente@cliente.com','cliente')

INSERT INTO dbo.Estudio VALUES
	('Blizzard'),
	('Rockstar Studios'),
	('Square Enix')

INSERT INTO dbo.Jogo VALUES
	(1,'Diablo 3','é um jogo que contém bastante ação e é viciante, seja você um novato ou um fã','2012-05-15',99.00),
	(2,'Red Dead Redemption II,','jogo eletrônico de ação-aventura western.','2018-10-26',120.00)