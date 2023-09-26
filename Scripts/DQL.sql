--DQL

USE inLock_games_manha;

SELECT * FROM dbo.Usuario;

SELECT * FROM dbo.Estudio;

SELECT * FROM dbo.Jogo;

SELECT * FROM dbo.Jogo 
	LEFT JOIN dbo.Estudio on dbo.Estudio.IdEstudio = dbo.Jogo.IdEstudio;

SELECT * FROM dbo.Estudio
	LEFT JOIN dbo.Jogo on dbo.Jogo.IdEstudio = dbo.Estudio.IdEstudio;

SELECT * FROM dbo.Usuario 
	WHERE Email = 'cliente@cliente.com' AND Senha = 'cliente';

SELECT * FROM dbo.Jogo 
	WHERE IdJogo = 1;

SELECT * FROM dbo.Estudio
	WHERE dbo.Estudio.NomeEstudio = 'Square Enix';