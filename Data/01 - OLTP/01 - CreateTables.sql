USE [DW_TEST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TFatoVenda]') AND type in (N'U'))
	ALTER TABLE [TFatoVenda] DROP CONSTRAINT IF EXISTS [FK_FatoVenda_DimCliente]
GO

DROP TABLE IF EXISTS [TDimCliente]
GO

CREATE TABLE [TDimCliente] (
    pkCliente   BIGINT IDENTITY(1,1) NOT NULL,
    cdCliente   VARCHAR(5)      NOT NULL,
    nmCliente   VARCHAR(MAX)    NOT NULL,
    CONSTRAINT [PK_DimCliente] PRIMARY KEY
    CLUSTERED ([pkCliente] ASC) ON [PRIMARY]
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS [TFatoVenda]
GO

CREATE TABLE [TFatoVenda] (
    pkVenda     BIGINT IDENTITY(1,1) NOT NULL,
    vlTotal     DECIMAL(7, 2)   NULL,
    fkCliente   BIGINT          NOT NULL,
    CONSTRAINT [PK_FatoVenda] PRIMARY KEY
    CLUSTERED ([pkVenda] ASC) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [TFatoVenda] WITH CHECK
ADD CONSTRAINT [FK_FatoVenda_DimCliente] FOREIGN KEY([fkCliente])
REFERENCES [dbo].[TDimCliente] ([pkCliente])
GO

ALTER TABLE [dbo].[TFatoVenda] CHECK CONSTRAINT [FK_FatoVenda_DimCliente]
GO