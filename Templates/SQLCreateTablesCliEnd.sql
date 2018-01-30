
CREATE TABLE [dbo].[Enderecos2](
	[CEP] [nchar](8) NOT NULL PRIMARY KEY,
	[logradouro] [nvarchar](200) NOT NULL,
	[bairro] [nvarchar](100) NOT NULL,
	[cidade] [nvarchar](100) NOT NULL,
	[estado] [nchar](2) NOT NULL)

GO

CREATE TABLE [dbo].[Clientes2](
	[IDCli] [int] NOT NULL PRIMARY KEY,
	[nome] [nvarchar](100) NOT NULL,
	[sobrenome] [nvarchar](200) NOT NULL,
	[dataNasc] [datetime] NOT NULL,
	[sexo] [nchar](1) NOT NULL,
	[endCEP] [nchar](8) NOT NULL,
	[endNum] [int] NOT NULL,
	[endComplemento] [nvarchar](50))

GO
