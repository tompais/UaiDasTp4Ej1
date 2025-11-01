-- ================================================================
-- Stored Procedures para la tabla OBRAS
-- ================================================================

USE BibliotecaDB;
GO

-- ================================================================
-- SP: Obtener todas las obras
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Obra_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, Titulo, Autor, Editorial, Isbn, 
        AnioPublicacion, Genero, Activo
    FROM Obras
  ORDER BY Titulo;
END
GO

-- ================================================================
-- SP: Obtener obra por ID
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Obra_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, Titulo, Autor, Editorial, Isbn, 
        AnioPublicacion, Genero, Activo
    FROM Obras
    WHERE Id = @Id;
END
GO

-- ================================================================
-- SP: Obtener obra por ISBN
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Obra_GetByIsbn
    @Isbn NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, Titulo, Autor, Editorial, Isbn, 
    AnioPublicacion, Genero, Activo
    FROM Obras
    WHERE Isbn = @Isbn;
END
GO

-- ================================================================
-- SP: Insertar obra
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Obra_Insert
    @Titulo NVARCHAR(200),
    @Autor NVARCHAR(150),
    @Editorial NVARCHAR(100),
  @Isbn NVARCHAR(20),
    @AnioPublicacion INT,
    @Genero NVARCHAR(50),
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
 INSERT INTO Obras (Titulo, Autor, Editorial, Isbn, AnioPublicacion, Genero)
        VALUES (@Titulo, @Autor, @Editorial, @Isbn, @AnioPublicacion, @Genero);
   
        SET @Id = SCOPE_IDENTITY();
END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- ================================================================
-- SP: Actualizar obra
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Obra_Update
    @Id INT,
    @Titulo NVARCHAR(200),
    @Autor NVARCHAR(150),
    @Editorial NVARCHAR(100),
    @Isbn NVARCHAR(20),
 @AnioPublicacion INT,
    @Genero NVARCHAR(50),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
UPDATE Obras
        SET 
        Titulo = @Titulo,
    Autor = @Autor,
            Editorial = @Editorial,
 Isbn = @Isbn,
          AnioPublicacion = @AnioPublicacion,
     Genero = @Genero,
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
-- SP: Eliminar obra (baja lógica)
-- ================================================================
CREATE OR ALTER PROCEDURE sp_Obra_Delete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE Obras
        SET Activo = 0,
        FechaModificacion = GETDATE()
        WHERE Id = @Id;
    END TRY
    BEGIN CATCH
     THROW;
    END CATCH
END
GO

PRINT 'Stored Procedures de OBRAS creados exitosamente';
GO
