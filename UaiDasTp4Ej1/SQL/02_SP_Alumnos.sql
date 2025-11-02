-- ================================================================
-- Stored Procedures para la tabla ALUMNOS
-- ================================================================

USE BibliotecaDB;
GO

-- ================================================================
-- SP: Obtener todos los alumnos
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Alumno_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, Nombre, Apellido, Dni, Email, Telefono, 
        FechaNacimiento, Activo
    FROM Alumnos
    ORDER BY Apellido, Nombre;
END
GO

-- ================================================================
-- SP: Obtener alumno por ID
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Alumno_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
 SELECT 
 Id, Nombre, Apellido, Dni, Email, Telefono, 
        FechaNacimiento, Activo
    FROM Alumnos
    WHERE Id = @Id;
END
GO

-- ================================================================
-- SP: Obtener alumno por DNI
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Alumno_GetByDni
    @Dni NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
  
    SELECT 
 Id, Nombre, Apellido, Dni, Email, Telefono, 
        FechaNacimiento, Activo
    FROM Alumnos
    WHERE Dni = @Dni;
END
GO

-- ================================================================
-- SP: Insertar alumno
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Alumno_Insert
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Dni NVARCHAR(20),
    @Email NVARCHAR(150),
    @Telefono NVARCHAR(20),
    @FechaNacimiento DATE,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO Alumnos (Nombre, Apellido, Dni, Email, Telefono, FechaNacimiento)
    VALUES (@Nombre, @Apellido, @Dni, @Email, @Telefono, @FechaNacimiento);
        
   SET @Id = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- ================================================================
-- SP: Actualizar alumno
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Alumno_Update
  @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Dni NVARCHAR(20),
    @Email NVARCHAR(150),
    @Telefono NVARCHAR(20),
    @FechaNacimiento DATE,
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE Alumnos
        SET 
       Nombre = @Nombre,
        Apellido = @Apellido,
        Dni = @Dni,
Email = @Email,
   Telefono = @Telefono,
            FechaNacimiento = @FechaNacimiento,
         Activo = @Activo,
            FechaModificacion = GETDATE()
        WHERE Id = @Id;
    END TRY
    BEGIN CATCH
  THROW;
    END CATCH
END
GO

-- ================================================================
-- SP: Eliminar alumno (baja lógica)
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Alumno_Delete
    @Id INT
AS
BEGIN
  SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE Alumnos
     SET Activo = 0,
  FechaModificacion = GETDATE()
 WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

PRINT 'Stored Procedures de ALUMNOS creados exitosamente';
GO
