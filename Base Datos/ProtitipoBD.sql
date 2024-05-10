CREATE  DATABASE GestionMiniSuper;
USE GestionMiniSuper;

CREATE TABLE Productos(
idProducto INT NOT NULL PRIMARY KEY,
nombreProducto VARCHAR(100),
costo DECIMAL(7, 2),
precio DECIMAL(7, 2)
);

CREATE TABLE Empleados(
cedula INT NOT NULL PRIMARY KEY,
nombreEmpleado VARCHAR(15),
apellidoEmpleado VARCHAR(15),
puesto VARCHAR (30),
codigoIngreso VARCHAR(8),
salarioMensualBruto DECIMAL(8,2),
salarioMensualNeto DECIMAL(8,2)
);

-------------------------------------------- Procedures ----------------------------------------------------
-------------- Insertar Productos --------------
GO
CREATE PROCEDURE proceInsertarProducto (
    @pIdProducto INT,
    @pNombreProducto VARCHAR(60),
    @pCosto DECIMAL(7, 2),
    @pPrecio DECIMAL(7, 2) )
AS
BEGIN
	DECLARE @res INT;

	IF NOT EXISTS (SELECT idProducto FROM dbo.Productos WHERE idProducto = @pIdProducto)
    BEGIN
        INSERT INTO dbo.Productos (idProducto, nombreProducto, costo, precio)
        VALUES (@pIdProducto, @pNombreProducto, @pCosto, @pPrecio);
		SET @res = 1;
	END
	ELSE
    BEGIN
        SET @res = NULL;
    END

    SELECT @res;
END	

-------------- TOP 3 de los empleados con mayor salario --------------
GO
CREATE PROCEDURE MostrarRankingSalarioBruto
AS
BEGIN
    SELECT TOP 3 cedula, nombreEmpleado, apellidoEmpleado, puesto, codigoIngreso, salarioMensualBruto, salarioMensualNeto
    FROM Empleados
    ORDER BY salarioMensualBruto DESC;
END;


-------------- LLamadas de prueba --------------
EXEC proceInsertarProducto '0803','Avena Quaker Mosh Hojuelas -300gr', 700, 1100;
EXEC proceInsertarProducto '74410','Arroz Indiana 99% Grano Entero -4000gr -300gr', 3500, 4090;
EXEC proceInsertarProducto '74411','Atún Tesoro Del Mar Trozos Vegetal -295gr', 800, 1600;
EXEC proceInsertarProducto '7411','Detergente Líquido Xedex multiacción -5L', 6400, 7700;
EXEC proceInsertarProducto '7442','Yogurt Dos Pinos Griego Original- 500g', 1800, 2800;

INSERT INTO Empleados (cedula, nombreEmpleado, apellidoEmpleado, puesto, codigoIngreso, salarioMensualBruto, salarioMensualNeto)
VALUES 
(102340567, 'Juan', 'Pérez', 'Gerente de Tienda', 'ABC123', 4500.00, 4019.85),
(204890123, 'María', 'García', 'Cajero', 'DEF456', 3800.00, 3394.74),
(301610346, 'Carlos', 'López', 'Dependiente', 'GHI789', 3000.00, 2679.90),
(403210156, 'Ana', 'Martínez', 'Encargado de Almacén', 'JKL012', 4200.00, 3752.46),
(501220349, 'Pedro', 'Rodríguez', 'Supervisor de Ventas', 'MNO345', 4800.00, 4288.32),
(603780941, 'Laura', 'Hernández', 'Cajero', 'PQR678', 3500.00, 3126.55),
(709810119, 'Sofía', 'Díaz', 'Gerente de Sucursal', 'STU901', 5200.00, 4645.76),
(103600452, 'Daniel', 'Gómez', 'Dependiente', 'VWX234', 3200.00, 2858.56),
(207090643, 'Luis', 'Álvarez', 'Encargado de Almacén', 'YZA567', 4100.00, 3662.53),
(307720961, 'Martina', 'Pérez', 'Cajero', 'BCD890', 3700.00, 3305.21);

INSERT INTO Empleados (cedula, nombreEmpleado, apellidoEmpleado, puesto, codigoIngreso, salarioMensualBruto, salarioMensualNeto)
VALUES 
(902650489, 'Pablo', 'Lara', 'Supervisor de Ventas', 'PLN325', 4900.00, 4388.32),


EXEC MostrarRankingSalarioBruto;

SELECT * FROM Productos;
SELECT *FROM EMPLEADOS;

