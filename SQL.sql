-- Clientes
CREATE TABLE Cliente (
    CPF VARCHAR(11) PRIMARY KEY,
    Nome VARCHAR(100),
    UF CHAR(2),
    Celular VARCHAR(20)
);
--------------------------------------------------------------------------
-- Financiamentos
CREATE TABLE Financiamento (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    CPFCliente VARCHAR(11),
    TipoFinanciamento VARCHAR(50),
    ValorTotal DECIMAL(10, 2),
    DataUltimoVencimento DATE,
    FOREIGN KEY (CPFCliente) REFERENCES Cliente(CPF)
);
--------------------------------------------------------------------------
-- Parcelas
CREATE TABLE Parcela (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    IDFinanciamento INT,
    NumeroParcela INT,
    ValorParcela DECIMAL(10, 2),
    DataVencimento DATE,
    DataPagamento DATE,
    FOREIGN KEY (IDFinanciamento) REFERENCES Financiamento(ID)
);
--------------------------------------------------------------------------

-- insert cliente
INSERT INTO Cliente (CPF, Nome, UF, Celular)
VALUES
    ('11122233344', 'João Silva', 'RJ', '(11) 90000-1111'),
    ('22233344455', 'José Ivo', 'RJ', '(11) 90000-1234'),
    ('33344455566', 'Paulo Ferreira', 'SP', '(21) 90000-1234');
    

-- insert financiamento
INSERT INTO Financiamento (CPFCliente, TipoFinanciamento, ValorTotal, DataUltimoVencimento)
VALUES
    ('12345678901', 'Automóvel', 30000.00, '2023-08-31');
   

-- Definir a quantidade de parcelas e calcular o valor de cada parcela
DECLARE @QuantidadeParcelas INT = 6;
DECLARE @ValorTotalFinanciamento DECIMAL(10, 2) = 30000.00;
DECLARE @ValorParcela DECIMAL(10, 2) = @ValorTotalFinanciamento / @QuantidadeParcelas;

-- Inserir parcelas
DECLARE @ParcelaAtual INT = 1;
WHILE @ParcelaAtual <= @QuantidadeParcelas
BEGIN
    INSERT INTO Parcela (IDFinanciamento, NumeroParcela, ValorParcela, DataVencimento, DataPagamento)
    VALUES
        (1, @ParcelaAtual, @ValorParcela, DATEADD(MONTH, @ParcelaAtual - 1, '2023-08-31'), NULL);

    SET @ParcelaAtual = @ParcelaAtual + 1;
END;
--------------------------------------------------------------------------

-- Listar os primeiros quatro clientes que possuem alguma parcela com mais de cinco dias em atraso:
SELECT TOP 4 c.CPF, c.Nome
FROM Cliente c
	INNER JOIN Financiamento f ON c.CPF = f.CPFCliente
	INNER JOIN Parcela p ON f.ID = p.IDFinanciamento
WHERE p.DataVencimento < GETDATE() - 5 AND p.DataPagamento IS NULL
ORDER BY c.CPF;

-- Listar todos os clientes do estado de SP que possuem mais de 60% das parcelas pagas:
SELECT c.CPF, c.Nome
FROM Cliente c
WHERE c.UF = 'SP' AND
      (
        SELECT COUNT(p.ID)
        FROM Parcela p
        INNER JOIN Financiamento f ON p.IDFinanciamento = f.ID
        WHERE f.CPFCliente = c.CPF AND p.DataPagamento IS NOT NULL
      ) >= (
        SELECT COUNT(p.ID)
        FROM Parcela p
        INNER JOIN Financiamento f ON p.IDFinanciamento = f.ID
        WHERE f.CPFCliente = c.CPF
      ) * 0.6;