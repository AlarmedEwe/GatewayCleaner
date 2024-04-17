USE [DW_TEST]
GO

SET IDENTITY_INSERT TDimCliente ON

INSERT INTO TDimCliente (pkCliente, cdCliente, nmCliente)
VALUES
    (1, 'RJ001', 'Luís João Almada'),
    (2, 'SP001', 'Emanuel Márcio Bernardes'),
    (3, 'RJ002', 'Elaine Louise Caroline Pereira')

SET IDENTITY_INSERT TDimCliente OFF
GO

SET IDENTITY_INSERT TFatoVenda ON

INSERT INTO TFatoVenda (pkVenda, vlTotal, fkCliente)
VALUES
    (1, 10.75, 3),
    (2, 30.50, 2),
    (3, 20.25, 1)

SET IDENTITY_INSERT TFatoVenda OFF
GO