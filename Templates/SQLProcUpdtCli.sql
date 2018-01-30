USE [Uppertools_Dev-TAPP]

GO

CREATE PROCEDURE UpdateCliente
	@ID int,
	@nome nvarchar(100),
	@sobrenome nvarchar(200),
	@dataNasc datetime,
	@sexo nchar(1),
	@endCEP nchar(8),
	@endNum int,
	@endComplemento nvarchar(50)
AS
BEGIN
	UPDATE Clientes
	SET nome = @nome, 
		sobrenome = @sobrenome, 
		dataNasc = @dataNasc, 
		sexo = @sexo,
		endCEP = @endCEP,
		endNum = @endNum,
		endComplemento = @endComplemento
	WHERE IDCli = @ID
END
