-- ============================================
-- Script de Creación de Base de Datos: BibliotecaDB
-- Versión: 1.0
-- Descripción: Script idempotente para crear la base de datos
--   del sistema de gestión de biblioteca
-- Características:
--   - Eliminación segura de objetos existentes
--   - Normalización de base de datos (3NF)
--   - Relaciones con Foreign Keys
--   - Índices para optimización
--   - Datos de prueba incluidos
-- ============================================

USE master;
GO

-- ============================================
-- 1. ELIMINACIÓN Y CREACIÓN DE BASE DE DATOS
-- ============================================

-- Verificar si existe la base de datos y eliminarla
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BibliotecaDB')
BEGIN
    -- Configurar la BD en modo de usuario único para poder eliminarla
    ALTER DATABASE BibliotecaDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BibliotecaDB;
    PRINT 'Base de datos BibliotecaDB eliminada exitosamente.';
END
GO

-- Crear la base de datos
CREATE DATABASE BibliotecaDB;
GO

PRINT 'Base de datos BibliotecaDB creada exitosamente.';
GO

-- Usar la base de datos
USE BibliotecaDB;
GO

-- ============================================
-- 2. CREACIÓN DE TABLAS
-- ============================================

-- --------------------------------------------
-- 2.1. Tabla: Alumnos
-- Descripción: Almacena información de los alumnos
-- --------------------------------------------
CREATE TABLE Alumnos
(
 Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Dni NVARCHAR(20) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
  FechaNacimiento DATE NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    
    -- Constraints
    CONSTRAINT PK_Alumnos PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT UQ_Alumnos_Dni UNIQUE (Dni),
    CONSTRAINT CK_Alumnos_Email CHECK (Email LIKE '%@%.%'),
    CONSTRAINT CK_Alumnos_FechaNacimiento CHECK (FechaNacimiento < GETDATE())
);
GO

PRINT 'Tabla Alumnos creada exitosamente.';
GO

-- --------------------------------------------
-- 2.2. Tabla: Obras
-- Descripción: Almacena información de las obras literarias
-- --------------------------------------------
CREATE TABLE Obras
(
    Id INT IDENTITY(1,1) NOT NULL,
    Titulo NVARCHAR(200) NOT NULL,
    Autor NVARCHAR(150) NOT NULL,
    Editorial NVARCHAR(100) NOT NULL,
    Isbn NVARCHAR(20) NOT NULL,
    AnioPublicacion INT NOT NULL,
    Genero NVARCHAR(50) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    
    -- Constraints
    CONSTRAINT PK_Obras PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT UQ_Obras_Isbn UNIQUE (Isbn),
    CONSTRAINT CK_Obras_AnioPublicacion CHECK (AnioPublicacion >= 1000 AND AnioPublicacion <= YEAR(GETDATE()))
);
GO

PRINT 'Tabla Obras creada exitosamente.';
GO

-- --------------------------------------------
-- 2.3. Tabla: Ejemplares
-- Descripción: Almacena los ejemplares físicos de cada obra
-- --------------------------------------------
CREATE TABLE Ejemplares
(
    Id INT IDENTITY(1,1) NOT NULL,
  ObraId INT NOT NULL,
    NumeroInventario NVARCHAR(50) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Disponible BIT NOT NULL DEFAULT 1,
    Activo BIT NOT NULL DEFAULT 1,
  
    -- Constraints
    CONSTRAINT PK_Ejemplares PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT UQ_Ejemplares_NumeroInventario UNIQUE (NumeroInventario),
    CONSTRAINT FK_Ejemplares_Obras FOREIGN KEY (ObraId) 
        REFERENCES Obras(Id) ON DELETE CASCADE,
    CONSTRAINT CK_Ejemplares_Precio CHECK (Precio > 0)
);
GO

PRINT 'Tabla Ejemplares creada exitosamente.';
GO

-- --------------------------------------------
-- 2.4. Tabla: Prestamos
-- Descripción: Almacena los préstamos de ejemplares a alumnos
-- --------------------------------------------
CREATE TABLE Prestamos
(
    Id INT IDENTITY(1,1) NOT NULL,
    AlumnoId INT NOT NULL,
    EjemplarId INT NOT NULL,
    FechaPrestamo DATETIME NOT NULL DEFAULT GETDATE(),
    FechaDevolucionPrevista DATETIME NOT NULL,
    FechaDevolucionReal DATETIME NULL,
    Devuelto BIT NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    
    -- Constraints
    CONSTRAINT PK_Prestamos PRIMARY KEY CLUSTERED (Id ASC),
  CONSTRAINT FK_Prestamos_Alumnos FOREIGN KEY (AlumnoId) 
        REFERENCES Alumnos(Id),
    CONSTRAINT FK_Prestamos_Ejemplares FOREIGN KEY (EjemplarId) 
        REFERENCES Ejemplares(Id),
    CONSTRAINT CK_Prestamos_FechaDevolucionPrevista 
        CHECK (FechaDevolucionPrevista > FechaPrestamo),
    CONSTRAINT CK_Prestamos_FechaDevolucionReal 
        CHECK (FechaDevolucionReal IS NULL OR FechaDevolucionReal >= FechaPrestamo)
);
GO

PRINT 'Tabla Prestamos creada exitosamente.';
GO

-- ============================================
-- 3. ÍNDICES PARA OPTIMIZACIÓN
-- ============================================

-- Índice para búsquedas por DNI en Alumnos
CREATE NONCLUSTERED INDEX IX_Alumnos_Dni 
    ON Alumnos(Dni ASC);
GO

-- Índice para búsquedas por Email en Alumnos
CREATE NONCLUSTERED INDEX IX_Alumnos_Email 
    ON Alumnos(Email ASC);
GO

-- Índice para búsquedas por ISBN en Obras
CREATE NONCLUSTERED INDEX IX_Obras_Isbn 
    ON Obras(Isbn ASC);
GO

-- Índice para búsquedas por Autor en Obras
CREATE NONCLUSTERED INDEX IX_Obras_Autor 
    ON Obras(Autor ASC);
GO

-- Índice para búsquedas por ObraId en Ejemplares
CREATE NONCLUSTERED INDEX IX_Ejemplares_ObraId 
    ON Ejemplares(ObraId ASC);
GO

-- Índice para búsquedas de ejemplares disponibles
CREATE NONCLUSTERED INDEX IX_Ejemplares_Disponible 
 ON Ejemplares(Disponible ASC) 
    WHERE Activo = 1;
GO

-- Índice para búsquedas por AlumnoId en Prestamos
CREATE NONCLUSTERED INDEX IX_Prestamos_AlumnoId 
    ON Prestamos(AlumnoId ASC);
GO

-- Índice para búsquedas por EjemplarId en Prestamos
CREATE NONCLUSTERED INDEX IX_Prestamos_EjemplarId 
    ON Prestamos(EjemplarId ASC);
GO

-- Índice para búsquedas de préstamos activos
CREATE NONCLUSTERED INDEX IX_Prestamos_Devuelto 
    ON Prestamos(Devuelto ASC, FechaDevolucionPrevista ASC) 
    WHERE Activo = 1;
GO

PRINT 'Índices creados exitosamente.';
GO

-- ============================================
-- 4. INSERCIÓN DE DATOS DE EJEMPLO
-- ============================================

PRINT 'Insertando datos de ejemplo...';
GO

-- --------------------------------------------
-- 4.1. Insertar Alumnos
-- --------------------------------------------
INSERT INTO Alumnos (Nombre, Apellido, Dni, Email, Telefono, FechaNacimiento, Activo)
VALUES 
    ('Juan', 'Pérez', '12345678', 'juan.perez@ejemplo.com', '11-1234-5678', '2000-05-15', 1),
    ('María', 'González', '23456789', 'maria.gonzalez@ejemplo.com', '11-2345-6789', '1999-08-22', 1),
    ('Carlos', 'Rodríguez', '34567890', 'carlos.rodriguez@ejemplo.com', '11-3456-7890', '2001-03-10', 1),
    ('Ana', 'Martínez', '45678901', 'ana.martinez@ejemplo.com', '11-4567-8901', '2000-11-30', 1),
    ('Luis', 'Fernández', '56789012', 'luis.fernandez@ejemplo.com', '11-5678-9012', '1998-07-18', 1);
GO

PRINT 'Alumnos insertados: 5 registros.';
GO

-- --------------------------------------------
-- 4.2. Insertar Obras
-- --------------------------------------------
INSERT INTO Obras (Titulo, Autor, Editorial, Isbn, AnioPublicacion, Genero, Activo)
VALUES 
('Cien años de soledad', 'Gabriel García Márquez', 'Editorial Sudamericana', '978-0307474728', 1967, 'Realismo mágico', 1),
    ('El principito', 'Antoine de Saint-Exupéry', 'Gallimard', '978-0156012195', 1943, 'Fábula', 1),
    ('1984', 'George Orwell', 'Secker & Warburg', '978-0451524935', 1949, 'Distopía', 1),
    ('Don Quijote de la Mancha', 'Miguel de Cervantes', 'Francisco de Robles', '978-0060934347', 1605, 'Novela', 1),
    ('Rayuela', 'Julio Cortázar', 'Editorial Sudamericana', '978-8420471891', 1963, 'Novela experimental', 1),
    ('La casa de los espíritus', 'Isabel Allende', 'Plaza & Janés', '978-1501117015', 1982, 'Realismo mágico', 1),
    ('El amor en los tiempos del cólera', 'Gabriel García Márquez', 'Oveja Negra', '978-0307389732', 1985, 'Romance', 1),
    ('Ficciones', 'Jorge Luis Borges', 'Sur', '978-0802130303', 1944, 'Cuentos', 1);
GO

PRINT 'Obras insertadas: 8 registros.';
GO

-- --------------------------------------------
-- 4.3. Insertar Ejemplares
-- --------------------------------------------
INSERT INTO Ejemplares (ObraId, NumeroInventario, Precio, Disponible, Activo)
VALUES 
    -- Cien años de soledad (2 ejemplares)
    (1, 'INV-001', 15000.00, 1, 1),
  (1, 'INV-002', 15000.00, 1, 1),
    
    -- El principito (2 ejemplares)
 (2, 'INV-003', 8500.00, 1, 1),
    (2, 'INV-004', 8500.00, 1, 1),
    
    -- 1984 (1 ejemplar)
    (3, 'INV-005', 12000.00, 1, 1),
    
    -- Don Quijote (2 ejemplares)
    (4, 'INV-006', 18000.00, 1, 1),
    (4, 'INV-007', 18000.00, 1, 1),
    
    -- Rayuela (1 ejemplar)
    (5, 'INV-008', 14000.00, 1, 1),
    
    -- La casa de los espíritus (1 ejemplar)
    (6, 'INV-009', 13500.00, 1, 1),
    
    -- El amor en los tiempos del cólera (2 ejemplares)
    (7, 'INV-010', 16000.00, 1, 1),
    (7, 'INV-011', 16000.00, 1, 1),
    
    -- Ficciones (1 ejemplar)
    (8, 'INV-012', 11000.00, 1, 1);
GO

PRINT 'Ejemplares insertados: 12 registros.';
GO

-- --------------------------------------------
-- 4.4. Insertar Préstamos de Ejemplo
-- --------------------------------------------
-- Préstamo activo (no devuelto)
INSERT INTO Prestamos (AlumnoId, EjemplarId, FechaPrestamo, FechaDevolucionPrevista, FechaDevolucionReal, Devuelto, Activo)
VALUES 
    (1, 1, DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, 2, GETDATE()), NULL, 0, 1);
GO

-- Actualizar disponibilidad del ejemplar prestado
UPDATE Ejemplares SET Disponible = 0 WHERE Id = 1;
GO

-- Préstamo devuelto
INSERT INTO Prestamos (AlumnoId, EjemplarId, FechaPrestamo, FechaDevolucionPrevista, FechaDevolucionReal, Devuelto, Activo)
VALUES 
    (2, 3, DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -8, GETDATE()), DATEADD(DAY, -7, GETDATE()), 1, 1);
GO

-- Préstamo vencido (no devuelto y fecha de devolución pasada)
INSERT INTO Prestamos (AlumnoId, EjemplarId, FechaPrestamo, FechaDevolucionPrevista, FechaDevolucionReal, Devuelto, Activo)
VALUES 
    (3, 5, DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -13, GETDATE()), NULL, 0, 1);
GO

-- Actualizar disponibilidad del ejemplar prestado (vencido)
UPDATE Ejemplares SET Disponible = 0 WHERE Id = 5;
GO

PRINT 'Préstamos insertados: 3 registros.';
GO

-- ============================================
-- 5. VERIFICACIÓN DE DATOS
-- ============================================

PRINT '';
PRINT '============================================';
PRINT 'VERIFICACIÓN DE DATOS INSERTADOS';
PRINT '============================================';

-- Contar registros en cada tabla
DECLARE @CountAlumnos INT, @CountObras INT, @CountEjemplares INT, @CountPrestamos INT;

SELECT @CountAlumnos = COUNT(*) FROM Alumnos;
SELECT @CountObras = COUNT(*) FROM Obras;
SELECT @CountEjemplares = COUNT(*) FROM Ejemplares;
SELECT @CountPrestamos = COUNT(*) FROM Prestamos;

PRINT 'Total de Alumnos: ' + CAST(@CountAlumnos AS VARCHAR(10));
PRINT 'Total de Obras: ' + CAST(@CountObras AS VARCHAR(10));
PRINT 'Total de Ejemplares: ' + CAST(@CountEjemplares AS VARCHAR(10));
PRINT 'Total de Préstamos: ' + CAST(@CountPrestamos AS VARCHAR(10));
PRINT '';

-- Mostrar estadísticas
PRINT 'Ejemplares disponibles: ' + CAST((SELECT COUNT(*) FROM Ejemplares WHERE Disponible = 1) AS VARCHAR(10));
PRINT 'Ejemplares prestados: ' + CAST((SELECT COUNT(*) FROM Ejemplares WHERE Disponible = 0) AS VARCHAR(10));
PRINT 'Préstamos activos: ' + CAST((SELECT COUNT(*) FROM Prestamos WHERE Devuelto = 0) AS VARCHAR(10));
PRINT 'Préstamos devueltos: ' + CAST((SELECT COUNT(*) FROM Prestamos WHERE Devuelto = 1) AS VARCHAR(10));
PRINT '';

PRINT '============================================';
PRINT 'SCRIPT COMPLETADO EXITOSAMENTE';
PRINT '============================================';
PRINT 'La base de datos BibliotecaDB está lista para usar.';
PRINT 'Todas las tablas, relaciones e índices han sido creados.';
PRINT 'Los datos de ejemplo están disponibles.';
PRINT '';
PRINT 'Ahora puede ejecutar los Stored Procedures:';
PRINT '- 02_SP_Alumnos.sql';
PRINT '  - 03_SP_Obras.sql';
PRINT '  - 04_SP_Ejemplares.sql';
PRINT '  - 05_SP_Prestamos.sql';
GO

-- ============================================
-- 6. CONSULTAS DE VERIFICACIÓN (OPCIONAL)
-- ============================================

-- Descomentar para ver los datos insertados
/*
PRINT '';
PRINT 'ALUMNOS:';
SELECT * FROM Alumnos;

PRINT '';
PRINT 'OBRAS:';
SELECT * FROM Obras;

PRINT '';
PRINT 'EJEMPLARES:';
SELECT E.Id, E.NumeroInventario, O.Titulo, E.Precio, E.Disponible
FROM Ejemplares E
INNER JOIN Obras O ON E.ObraId = O.Id;

PRINT '';
PRINT 'PRÉSTAMOS:';
SELECT 
    P.Id,
    A.Nombre + ' ' + A.Apellido AS Alumno,
    O.Titulo AS Obra,
    E.NumeroInventario,
P.FechaPrestamo,
    P.FechaDevolucionPrevista,
    P.FechaDevolucionReal,
    CASE WHEN P.Devuelto = 1 THEN 'Devuelto' ELSE 'Pendiente' END AS Estado
FROM Prestamos P
INNER JOIN Alumnos A ON P.AlumnoId = A.Id
INNER JOIN Ejemplares E ON P.EjemplarId = E.Id
INNER JOIN Obras O ON E.ObraId = O.Id;
*/
