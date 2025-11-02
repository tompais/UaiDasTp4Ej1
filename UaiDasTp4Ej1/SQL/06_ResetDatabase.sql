-- ================================================================
-- Script para resetear/limpiar datos de prueba
-- ================================================================

USE BibliotecaDB;
GO

PRINT 'Eliminando datos de prueba...';

-- Eliminar en orden por dependencias
DELETE FROM Prestamos;
DELETE FROM Ejemplares;
DELETE FROM Obras;
DELETE FROM Alumnos;

PRINT 'Datos eliminados exitosamente';
GO

-- Reiniciar los contadores de identidad
DBCC CHECKIDENT ('Prestamos', RESEED, 0);
DBCC CHECKIDENT ('Ejemplares', RESEED, 0);
DBCC CHECKIDENT ('Obras', RESEED, 0);
DBCC CHECKIDENT ('Alumnos', RESEED, 0);
GO

PRINT 'Contadores de identidad reiniciados';
GO

-- Insertar nuevamente datos de prueba si lo deseas
PRINT 'Insertando nuevos datos de prueba...';

-- Alumnos
INSERT INTO Alumnos (Nombre, Apellido, Dni, Email, Telefono, FechaNacimiento)
VALUES 
('Juan', 'Pérez', '12345678', 'juan.perez@email.com', '1134567890', '2000-05-15'),
    ('María', 'González', '23456789', 'maria.gonzalez@email.com', '1145678901', '1999-08-20'),
    ('Carlos', 'Rodríguez', '34567890', 'carlos.rodriguez@email.com', '1156789012', '2001-03-10');

-- Obras
INSERT INTO Obras (Titulo, Autor, Editorial, Isbn, AnioPublicacion, Genero)
VALUES 
    ('Cien años de soledad', 'Gabriel García Márquez', 'Editorial Sudamericana', '978-0307474728', 1967, 'Realismo Mágico'),
    ('El principito', 'Antoine de Saint-Exupéry', 'Reynal & Hitchcock', '978-0156012195', 1943, 'Fábula'),
    ('1984', 'George Orwell', 'Secker & Warburg', '978-0451524935', 1949, 'Distopía'),
    ('Don Quijote de la Mancha', 'Miguel de Cervantes', 'Francisco de Robles', '978-0060934347', 1605, 'Novela'),
    ('Rayuela', 'Julio Cortázar', 'Editorial Sudamericana', '978-8437604572', 1963, 'Novela Experimental');

-- Ejemplares
INSERT INTO Ejemplares (ObraId, NumeroInventario, Precio)
VALUES 
    (1, 'INV-001', 15000.00),
    (1, 'INV-002', 15000.00),
    (2, 'INV-003', 8000.00),
    (2, 'INV-004', 8000.00),
    (3, 'INV-005', 12000.00),
    (4, 'INV-006', 20000.00),
    (5, 'INV-007', 18000.00);

PRINT 'Datos de prueba insertados exitosamente';
GO

-- Verificar datos
SELECT 'Alumnos' AS Tabla, COUNT(*) AS Cantidad FROM Alumnos
UNION ALL
SELECT 'Obras', COUNT(*) FROM Obras
UNION ALL
SELECT 'Ejemplares', COUNT(*) FROM Ejemplares
UNION ALL
SELECT 'Prestamos', COUNT(*) FROM Prestamos;
GO

PRINT 'Base de datos reseteada y lista para usar';
GO
