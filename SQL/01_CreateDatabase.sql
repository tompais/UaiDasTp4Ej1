-- ================================================================
-- Script de creación de base de datos para Sistema de Biblioteca
-- ================================================================

USE master;
GO

-- Eliminar la base de datos si existe
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BibliotecaDB')
BEGIN
    ALTER DATABASE BibliotecaDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BibliotecaDB;
END
GO

-- Crear la base de datos
CREATE DATABASE BibliotecaDB;
GO

USE BibliotecaDB;
GO

-- ================================================================
-- TABLA: Alumnos
-- ================================================================
CREATE TABLE Alumnos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Dni NVARCHAR(20) NOT NULL UNIQUE,
    Email NVARCHAR(150) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL
);
GO

CREATE INDEX IX_Alumnos_Dni ON Alumnos(Dni);
CREATE INDEX IX_Alumnos_Activo ON Alumnos(Activo);
GO

-- ================================================================
-- TABLA: Obras
-- ================================================================
CREATE TABLE Obras (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(200) NOT NULL,
    Autor NVARCHAR(150) NOT NULL,
    Editorial NVARCHAR(100) NOT NULL,
    Isbn NVARCHAR(20) NOT NULL UNIQUE,
    AnioPublicacion INT NOT NULL,
    Genero NVARCHAR(50) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
  FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
  FechaModificacion DATETIME NULL
);
GO

CREATE INDEX IX_Obras_Isbn ON Obras(Isbn);
CREATE INDEX IX_Obras_Titulo ON Obras(Titulo);
CREATE INDEX IX_Obras_Activo ON Obras(Activo);
GO

-- ================================================================
-- TABLA: Ejemplares
-- ================================================================
CREATE TABLE Ejemplares (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ObraId INT NOT NULL,
    NumeroInventario NVARCHAR(50) NOT NULL UNIQUE,
    Precio DECIMAL(10,2) NOT NULL,
    Disponible BIT NOT NULL DEFAULT 1,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL,
 CONSTRAINT FK_Ejemplares_Obras FOREIGN KEY (ObraId) REFERENCES Obras(Id)
);
GO

CREATE INDEX IX_Ejemplares_ObraId ON Ejemplares(ObraId);
CREATE INDEX IX_Ejemplares_NumeroInventario ON Ejemplares(NumeroInventario);
CREATE INDEX IX_Ejemplares_Disponible ON Ejemplares(Disponible);
CREATE INDEX IX_Ejemplares_Activo ON Ejemplares(Activo);
GO

-- ================================================================
-- TABLA: Prestamos
-- ================================================================
CREATE TABLE Prestamos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
  AlumnoId INT NOT NULL,
    EjemplarId INT NOT NULL,
FechaPrestamo DATETIME NOT NULL,
    FechaDevolucionPrevista DATETIME NOT NULL,
    FechaDevolucionReal DATETIME NULL,
    Devuelto BIT NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL,
    CONSTRAINT FK_Prestamos_Alumnos FOREIGN KEY (AlumnoId) REFERENCES Alumnos(Id),
    CONSTRAINT FK_Prestamos_Ejemplares FOREIGN KEY (EjemplarId) REFERENCES Ejemplares(Id)
);
GO

CREATE INDEX IX_Prestamos_AlumnoId ON Prestamos(AlumnoId);
CREATE INDEX IX_Prestamos_EjemplarId ON Prestamos(EjemplarId);
CREATE INDEX IX_Prestamos_FechaPrestamo ON Prestamos(FechaPrestamo);
CREATE INDEX IX_Prestamos_Devuelto ON Prestamos(Devuelto);
CREATE INDEX IX_Prestamos_Activo ON Prestamos(Activo);
GO

-- ================================================================
-- Datos de prueba
-- ================================================================

-- Alumnos de prueba
INSERT INTO Alumnos (Nombre, Apellido, Dni, Email, Telefono, FechaNacimiento)
VALUES 
    ('Juan', 'Pérez', '12345678', 'juan.perez@email.com', '1134567890', '2000-05-15'),
  ('María', 'González', '23456789', 'maria.gonzalez@email.com', '1145678901', '1999-08-20'),
    ('Carlos', 'Rodríguez', '34567890', 'carlos.rodriguez@email.com', '1156789012', '2001-03-10');
GO

-- Obras de prueba
INSERT INTO Obras (Titulo, Autor, Editorial, Isbn, AnioPublicacion, Genero)
VALUES 
    ('Cien años de soledad', 'Gabriel García Márquez', 'Editorial Sudamericana', '978-0307474728', 1967, 'Realismo Mágico'),
    ('El principito', 'Antoine de Saint-Exupéry', 'Reynal & Hitchcock', '978-0156012195', 1943, 'Fábula'),
    ('1984', 'George Orwell', 'Secker & Warburg', '978-0451524935', 1949, 'Distopía'),
    ('Don Quijote de la Mancha', 'Miguel de Cervantes', 'Francisco de Robles', '978-0060934347', 1605, 'Novela'),
    ('Rayuela', 'Julio Cortázar', 'Editorial Sudamericana', '978-8437604572', 1963, 'Novela Experimental');
GO

-- Ejemplares de prueba
INSERT INTO Ejemplares (ObraId, NumeroInventario, Precio)
VALUES 
    (1, 'INV-001', 15000.00),
    (1, 'INV-002', 15000.00),
    (2, 'INV-003', 8000.00),
    (2, 'INV-004', 8000.00),
    (3, 'INV-005', 12000.00),
    (4, 'INV-006', 20000.00),
    (5, 'INV-007', 18000.00);
GO

PRINT 'Base de datos BibliotecaDB creada exitosamente con datos de prueba';
GO
