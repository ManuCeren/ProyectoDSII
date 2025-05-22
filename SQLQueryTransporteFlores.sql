CREATE DATABASE TransporteFloresDB;

USE TransporteFloresDB;

-- Tabla Direcciones
CREATE TABLE Direcciones (
    idDirecciones INT PRIMARY KEY IDENTITY(1,1),
    Direccion1 VARCHAR(255),
    Direccion2 VARCHAR(100) -- Opcional
);

-- Crear tabla Unidades
CREATE TABLE Unidades (
    idUnidades INT PRIMARY KEY IDENTITY(1,1),
	TipoUnidad VARCHAR(50),
    Placa VARCHAR(20),
    Marca VARCHAR(50),
    Modelo VARCHAR(50),
    Año INT,
    Estado VARCHAR(20),
    KilometrajeActual INT
);

-- Crear tabla Conductores
CREATE TABLE Conductores (
    idConductores INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100),
    Licencia VARCHAR(20),
    FechaIngreso DATE,
    Estado VARCHAR(20),
    Telefono VARCHAR(15),
    idVehiculo INT,
    FOREIGN KEY (idVehiculo) REFERENCES Unidades(idUnidades)
);

-- Crear tabla Clientes
CREATE TABLE Clientes (
    idClientes INT PRIMARY KEY IDENTITY(1,1),
    nombreCliente VARCHAR(100),
    Direccion VARCHAR(255),
    Telefono VARCHAR(20),
    Email VARCHAR(50),
    TipoCliente VARCHAR(50)
);

-- Crear tabla Rutas
CREATE TABLE Rutas (
    idRutas INT PRIMARY KEY IDENTITY(1,1),
    Origen VARCHAR(50),
    Destino VARCHAR(50),
    Distancia INT -- Distancia en kilómetros
);

-- Crear tabla Mantenimientos
CREATE TABLE Mantenimientos (
    idMantenimientos INT PRIMARY KEY IDENTITY(1,1),
    idUnidad INT,
    FechaMantenimiento DATE,
    FechaSiguienteMantenimiento DATE,
    FOREIGN KEY (idUnidad) REFERENCES Unidades(idUnidades)
);

-- Crear tabla Envios
CREATE TABLE Envios (
    idEnvios INT PRIMARY KEY IDENTITY(1,1),
    idCliente INT,
    idRuta INT,
    FechaSolicitud DATE,
    FechaEntregaEsperada DATE,
    Estado VARCHAR(20),
    Mercancia VARCHAR(255),
    PesoTotal DECIMAL(18,2),
    VolumenTotal DECIMAL(18,2),
    FOREIGN KEY (idCliente) REFERENCES Clientes(idClientes),
    FOREIGN KEY (idRuta) REFERENCES Rutas(idRutas)
);

-- Crear tabla Facturación
CREATE TABLE Facturacion (
    idFacturacion INT PRIMARY KEY IDENTITY(1,1),
    idCliente INT,
    FechaFactura DATE,
    MontoTotal DECIMAL(18,2),
    EstadoPago VARCHAR(20),
    idEnvio INT,
    FOREIGN KEY (idCliente) REFERENCES Clientes(idClientes),
    FOREIGN KEY (idEnvio) REFERENCES Envios(idEnvios)
);

-- Crear tabla DetalleFacturación
CREATE TABLE DetalleFacturacion (
    idDetalleFacturacion INT PRIMARY KEY IDENTITY(1,1),
    idFacturacion INT,
    Detalle VARCHAR(200),
    Precio DECIMAL(18,2),
    FOREIGN KEY (idFacturacion) REFERENCES Facturacion(idFacturacion)
);

-- Crear tabla Usuarios
CREATE TABLE Usuarios (
    idUsuarios INT PRIMARY KEY IDENTITY(1,1),
    nombreUsuario VARCHAR(100),
    Rol VARCHAR(45),
    Contraseña VARCHAR(255),
    Email VARCHAR(50)
);

--Registros para la base de datos
-- Insertar en la tabla Direcciones
INSERT INTO Direcciones (Direccion1, Direccion2) VALUES 
('Calle 123, A', NULL),
('Calle 456, B', NULL),
('Calle 789, C', NULL);

-- Insertar en la tabla Unidades
INSERT INTO Unidades (TipoUnidad, Placa, Marca, Modelo, Año, Estado, KilometrajeActual) VALUES 
('Cabezal','C 68272', 'Volvo', 'FH16', 2019, 'Activo', 500000),
('Camion','C 111503', 'Mercedes', 'Actros', 2018, 'En Mantenimiento', 300000),
('Cabezal','C 74283', 'Scania', 'R500', 2020, 'Activo', 400000);

-- Insertar en la tabla Conductores
INSERT INTO Conductores (Nombre, Licencia, FechaIngreso, Estado, Telefono, idVehiculo) VALUES 
('Héctor Alfredo Hernández López', '0315-280291-107-1', '2020-01-01', 'Activo', '123456789', 1),
('Carlos René Medina Tobar', '0315-171075-102-0', '2019-05-15', 'Activo', '987654321', 2),
('Santiago Israel Méndez Cordero', '0316-221158-102-7', '2018-03-10', 'Inactivo', '564738291', 3);

-- Insertar en la tabla Rutas
INSERT INTO Rutas (Origen, Destino, Distancia) VALUES 
('Ciudad A', 'Panama, Chiriquí', 150),
('Ciudad B', 'MZT, CZT, RZT', 200),
('Ciudad C', 'Reu, CCT, TTP', 250);

--Insertar en la Tabla Clientes
INSERT INTO Clientes (nombreCliente, Direccion, Telefono, Email, TipoCliente) VALUES
('Juan Pérez', 'Calle 123, A', '123456789', 'juan@cliente.com', 'Nuevo'),
('María López', 'Calle 456, B', '987654321', 'maria@cliente.com', 'Existente'),
('Carlos Ruiz', 'Calle 789, C', '564738291', 'carlos@cliente.com', 'Nuevo');

-- Insertar en la tabla Envíos
INSERT INTO Envios (idCliente, idRuta, FechaSolicitud, FechaEntregaEsperada, Estado, Mercancía, PesoTotal, VolumenTotal) VALUES 
(1, 1, '2022-01-01', '2022-01-05', 'En Proceso', 'Carga General', 500, 10),
(2, 2, '2022-01-03', '2022-01-07', 'Pendiente', 'Ropa', 200, 5),
(3, 3, '2022-01-04', '2022-01-08', 'Completado', 'Electrónica', 150, 2);

-- Insertar en la tabla Facturación
INSERT INTO Facturacion (idCliente, FechaFactura, MontoTotal, EstadoPago, idEnvio) VALUES 
(1, '2022-01-05', 1200, 'Pagado', 1),
(2, '2022-01-07', 500, 'Pendiente', 2),
(3, '2022-01-08', 300, 'Pagado', 3);

-- Insertar en la tabla DetalleFacturación
INSERT INTO DetalleFacturacion (idFacturacion, Detalle, Precio) VALUES 
(1, 'Carga General', 1200),
(2, 'Ropa', 500),
(3, 'Electrónica', 300);


