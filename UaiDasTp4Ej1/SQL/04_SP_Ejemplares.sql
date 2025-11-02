-- ================================================================
-- Stored Procedures para la tabla EJEMPLARES
-- ================================================================

USE BibliotecaDB;
GO

-- ================================================================
-- SP: Obtener todos los ejemplares
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Ejemplar_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
  SELECT 
     Id, ObraId, NumeroInventario, Precio, 
        Disponible, Activo
    FROM Ejemplares
    ORDER BY NumeroInventario;
END
GO

-- ================================================================
-- SP: Obtener ejemplar por ID
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Ejemplar_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, ObraId, NumeroInventario, Precio, 
        Disponible, Activo
  FROM Ejemplares
    WHERE Id = @Id;
END
GO

-- ================================================================
-- SP: Obtener ejemplares por ObraId
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Ejemplar_GetByObraId
    @ObraId INT
AS
BEGIN
  SET NOCOUNT ON;
    
    SELECT 
        Id, ObraId, NumeroInventario, Precio, 
    Disponible, Activo
    FROM Ejemplares
    WHERE ObraId = @ObraId
    ORDER BY NumeroInventario;
END
GO

-- ================================================================
-- SP: Obtener ejemplares disponibles
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Ejemplar_GetDisponibles
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, ObraId, NumeroInventario, Precio, 
        Disponible, Activo
    FROM Ejemplares
    WHERE Disponible = 1 AND Activo = 1
    ORDER BY NumeroInventario;
END
GO

-- ================================================================
-- SP: Obtener ejemplar por número de inventario
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Ejemplar_GetByNumeroInventario
    @NumeroInventario NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, ObraId, NumeroInventario, Precio, 
     Disponible, Activo
    FROM Ejemplares
    WHERE NumeroInventario = @NumeroInventario;
END
GO

-- ================================================================
-- SP: Insertar ejemplar
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Ejemplar_Insert
    @ObraId INT,
    @NumeroInventario NVARCHAR(50),
  @Precio DECIMAL(10,2),
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
BEGIN TRY
        INSERT INTO Ejemplares (ObraId, NumeroInventario, Precio)
   VALUES (@ObraId, @NumeroInventario, @Precio);
        
        SET @Id = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- ================================================================
-- SP: Actualizar ejemplar
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Ejemplar_Update
    @Id INT,
    @ObraId INT,
    @NumeroInventario NVARCHAR(50),
    @Precio DECIMAL(10,2),
    @Disponible BIT,
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE Ejemplares
     SET 
    ObraId = @ObraId,
       NumeroInventario = @NumeroInventario,
      Precio = @Precio,
  Disponible = @Disponible,
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
-- SP: Eliminar ejemplar (baja lógica)
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Ejemplar_Delete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
 
    BEGIN TRY
 UPDATE Ejemplares
      SET Activo = 0,
            FechaModificacion = GETDATE()
        WHERE Id = @Id;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

PRINT 'Stored Procedures de EJEMPLARES creados exitosamente';
GO
