USE [DW_TEST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP VIEW IF EXISTS [DimCliente]
GO

CREATE VIEW [DimCliente]
AS
	SELECT
		pkCliente,
		cdCliente	AS "Cod. Cliente",
		nmCliente	AS "Nome Cliente"
	FROM TDimCliente WITH (NOLOCK);
GO

DROP VIEW IF EXISTS [FatoVenda]
GO

CREATE VIEW [FatoVenda]
AS
	SELECT
		vlTotal		AS "Vl. Total",
		fkCliente
	FROM TFatoVenda WITH (NOLOCK);
GO