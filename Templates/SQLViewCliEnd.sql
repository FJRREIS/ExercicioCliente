/****** Script para criar View com dados do cliente formatados e com seu endereço  ******/
USE [Uppertools_Dev-TAPP]

GO

CREATE VIEW ClientesComEndereço as
SELECT IDCli, nome, sobrenome, FORMAT(dataNasc, 'dd/MM/yyyy') as dataNasc, CASE sexo WHEN 'M' THEN 'Masculino' WHEN 'F' THEN 'Feminino' END as sexo, SUBSTRING(CEP,1,5) + '-' + SUBSTRING(CEP, 6,3) as CEP, logradouro, endNum, endComplemento, bairro, cidade, estado 
FROM Clientes
LEFT JOIN
Enderecos
ON endCEP = CEP