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
	(1,'Diablo 3','� um jogo que cont�m bastante a��o e � viciante, seja voc� um novato ou um f�','2012-05-15',99.00),
	(2,'Red Dead Redemption II,','jogo eletr�nico de a��o-aventura western.','2018-10-26',120.00)