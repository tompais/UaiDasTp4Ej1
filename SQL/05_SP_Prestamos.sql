-- ================================================================
-- Stored Procedures para la tabla PRESTAMOS
-- ================================================================

USE BibliotecaDB;
GO

-- ================================================================
-- SP: Obtener todos los préstamos
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, AlumnoId, EjemplarId, FechaPrestamo, 
        FechaDevolucionPrevista, FechaDevolucionReal, 
      Devuelto, Activo
    FROM Prestamos
    ORDER BY FechaPrestamo DESC;
END
GO

-- ================================================================
-- SP: Obtener préstamo por ID
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
  
    SELECT 
        Id, AlumnoId, EjemplarId, FechaPrestamo, 
        FechaDevolucionPrevista, FechaDevolucionReal, 
  Devuelto, Activo
    FROM Prestamos
    WHERE Id = @Id;
END
GO

-- ================================================================
-- SP: Obtener préstamos por AlumnoId
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_GetByAlumnoId
    @AlumnoId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
   Id, AlumnoId, EjemplarId, FechaPrestamo, 
FechaDevolucionPrevista, FechaDevolucionReal, 
     Devuelto, Activo
    FROM Prestamos
    WHERE AlumnoId = @AlumnoId
    ORDER BY FechaPrestamo DESC;
END
GO

-- ================================================================
-- SP: Obtener préstamos activos (no devueltos)
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_GetActivos
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
    Id, AlumnoId, EjemplarId, FechaPrestamo, 
        FechaDevolucionPrevista, FechaDevolucionReal, 
        Devuelto, Activo
    FROM Prestamos
    WHERE Devuelto = 0 AND Activo = 1
    ORDER BY FechaPrestamo DESC;
END
GO

-- ================================================================
-- SP: Obtener préstamos vencidos
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_GetVencidos
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
    Id, AlumnoId, EjemplarId, FechaPrestamo, 
        FechaDevolucionPrevista, FechaDevolucionReal, 
Devuelto, Activo
    FROM Prestamos
    WHERE Devuelto = 0 
      AND Activo = 1
      AND FechaDevolucionPrevista < GETDATE()
    ORDER BY FechaDevolucionPrevista ASC;
END
GO

-- ================================================================
-- SP: Insertar préstamo
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_Insert
    @AlumnoId INT,
    @EjemplarId INT,
    @FechaPrestamo DATETIME,
    @FechaDevolucionPrevista DATETIME,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
    -- Insertar préstamo
        INSERT INTO Prestamos (AlumnoId, EjemplarId, FechaPrestamo, FechaDevolucionPrevista)
 VALUES (@AlumnoId, @EjemplarId, @FechaPrestamo, @FechaDevolucionPrevista);
        
        SET @Id = SCOPE_IDENTITY();
        
 -- Marcar ejemplar como no disponible
        UPDATE Ejemplares
      SET Disponible = 0,
            FechaModificacion = GETDATE()
        WHERE Id = @EjemplarId;
   
      COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ================================================================
-- SP: Actualizar préstamo
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_Update
    @Id INT,
    @AlumnoId INT,
    @EjemplarId INT,
    @FechaPrestamo DATETIME,
    @FechaDevolucionPrevista DATETIME,
    @FechaDevolucionReal DATETIME = NULL,
    @Devuelto BIT,
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    
 BEGIN TRY
UPDATE Prestamos
        SET 
            AlumnoId = @AlumnoId,
     EjemplarId = @EjemplarId,
     FechaPrestamo = @FechaPrestamo,
      FechaDevolucionPrevista = @FechaDevolucionPrevista,
            FechaDevolucionReal = @FechaDevolucionReal,
            Devuelto = @Devuelto,
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
-- SP: Marcar préstamo como devuelto
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_MarcarDevuelto
    @Id INT,
    @FechaDevolucionReal DATETIME
AS
BEGIN
  SET NOCOUNT ON;
    
    BEGIN TRY
BEGIN TRANSACTION;
        
        DECLARE @EjemplarId INT;
        
        -- Obtener el ejemplar del préstamo
        SELECT @EjemplarId = EjemplarId
  FROM Prestamos
        WHERE Id = @Id;
     
        -- Actualizar préstamo
        UPDATE Prestamos
        SET FechaDevolucionReal = @FechaDevolucionReal,
 Devuelto = 1,
            FechaModificacion = GETDATE()
        WHERE Id = @Id;
        
        -- Marcar ejemplar como disponible
  UPDATE Ejemplares
        SET Disponible = 1,
  FechaModificacion = GETDATE()
        WHERE Id = @EjemplarId;
  
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
 ROLLBACK TRANSACTION;
   THROW;
    END CATCH
END
GO

-- ================================================================
-- SP: Eliminar préstamo (baja lógica)
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Prestamo_Delete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE Prestamos
 SET Activo = 0,
            FechaModificacion = GETDATE()
        WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

PRINT 'Stored Procedures de PRESTAMOS creados exitosamente';
GO
