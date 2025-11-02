# ?? Scripts SQL - Sistema de Gestión de Biblioteca

Este directorio contiene todos los scripts SQL necesarios para crear y configurar la base de datos del sistema de gestión de biblioteca.

---

## ?? Orden de Ejecución

Los scripts deben ejecutarse en el siguiente orden:

### **1?? 01_CreateDatabase.sql** (OBLIGATORIO - PRIMERO)
**Descripción:** Script maestro que crea la base de datos completa
- ? Elimina la base de datos si existe (idempotente)
- ? Crea la base de datos BibliotecaDB
- ? Crea todas las tablas con sus relaciones
- ? Crea índices para optimización
- ? Inserta datos de ejemplo

**Contenido:**
- Tablas: Alumnos, Obras, Ejemplares, Prestamos
- Foreign Keys con CASCADE
- Índices para búsquedas optimizadas
- 5 Alumnos de ejemplo
- 8 Obras de ejemplo
- 12 Ejemplares de ejemplo
- 3 Préstamos de ejemplo

**Ejecutar en:** SQL Server Management Studio (SSMS)

---

### **2?? 02_SP_Alumnos.sql**
**Descripción:** Stored Procedures para operaciones CRUD de Alumnos

**Stored Procedures creados:**
- `sp_Alumno_GetAll` - Obtener todos los alumnos
- `sp_Alumno_GetById` - Obtener alumno por ID
- `sp_Alumno_GetByDni` - Obtener alumno por DNI
- `sp_Alumno_Insert` - Insertar nuevo alumno
- `sp_Alumno_Update` - Actualizar alumno existente
- `sp_Alumno_Delete` - Eliminar alumno (baja lógica)

---

### **3?? 03_SP_Obras.sql**
**Descripción:** Stored Procedures para operaciones CRUD de Obras

**Stored Procedures creados:**
- `sp_Obra_GetAll` - Obtener todas las obras
- `sp_Obra_GetById` - Obtener obra por ID
- `sp_Obra_GetByIsbn` - Obtener obra por ISBN
- `sp_Obra_Insert` - Insertar nueva obra
- `sp_Obra_Update` - Actualizar obra existente
- `sp_Obra_Delete` - Eliminar obra (baja lógica)

---

### **4?? 04_SP_Ejemplares.sql**
**Descripción:** Stored Procedures para operaciones CRUD de Ejemplares

**Stored Procedures creados:**
- `sp_Ejemplar_GetAll` - Obtener todos los ejemplares
- `sp_Ejemplar_GetById` - Obtener ejemplar por ID
- `sp_Ejemplar_GetByObraId` - Obtener ejemplares por obra
- `sp_Ejemplar_GetDisponibles` - Obtener ejemplares disponibles
- `sp_Ejemplar_GetByNumeroInventario` - Obtener ejemplar por número de inventario
- `sp_Ejemplar_Insert` - Insertar nuevo ejemplar
- `sp_Ejemplar_Update` - Actualizar ejemplar existente
- `sp_Ejemplar_Delete` - Eliminar ejemplar (baja lógica)

---

### **5?? 05_SP_Prestamos.sql**
**Descripción:** Stored Procedures para operaciones de Préstamos con transacciones

**Stored Procedures creados:**
- `sp_Prestamo_GetAll` - Obtener todos los préstamos
- `sp_Prestamo_GetById` - Obtener préstamo por ID
- `sp_Prestamo_GetByAlumnoId` - Obtener préstamos por alumno
- `sp_Prestamo_GetActivos` - Obtener préstamos activos (no devueltos)
- `sp_Prestamo_GetVencidos` - Obtener préstamos vencidos
- `sp_Prestamo_Insert` - Insertar préstamo (con transacción)
- `sp_Prestamo_Update` - Actualizar préstamo
- `sp_Prestamo_MarcarDevuelto` - Marcar préstamo como devuelto (con transacción)
- `sp_Prestamo_Delete` - Eliminar préstamo (baja lógica)

---

## ?? Instrucciones de Instalación

### **Opción 1: Ejecución Manual (Recomendado)**

1. Abrir **SQL Server Management Studio (SSMS)**
2. Conectarse a la instancia de SQL Server
3. Ejecutar los scripts en orden:

```sql
-- 1. Crear la base de datos y tablas
:r UaiDasTp4Ej1\SQL\01_CreateDatabase.sql

-- 2. Crear Stored Procedures de Alumnos
:r UaiDasTp4Ej1\SQL\02_SP_Alumnos.sql

-- 3. Crear Stored Procedures de Obras
:r UaiDasTp4Ej1\SQL\03_SP_Obras.sql

-- 4. Crear Stored Procedures de Ejemplares
:r UaiDasTp4Ej1\SQL\04_SP_Ejemplares.sql

-- 5. Crear Stored Procedures de Préstamos
:r UaiDasTp4Ej1\SQL\05_SP_Prestamos.sql
```

### **Opción 2: Ejecución Individual**

Abrir cada archivo `.sql` en SSMS y ejecutar con `F5` en el orden indicado.

---

## ? Verificación

Después de ejecutar todos los scripts, verificar que todo está correcto:

```sql
USE BibliotecaDB;
GO

-- Verificar tablas
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;

-- Verificar Stored Procedures
SELECT ROUTINE_NAME 
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_TYPE = 'PROCEDURE'
ORDER BY ROUTINE_NAME;

-- Verificar datos
SELECT 'Alumnos' AS Tabla, COUNT(*) AS Registros FROM Alumnos
UNION ALL
SELECT 'Obras', COUNT(*) FROM Obras
UNION ALL
SELECT 'Ejemplares', COUNT(*) FROM Ejemplares
UNION ALL
SELECT 'Prestamos', COUNT(*) FROM Prestamos;
```

**Resultado esperado:**
- 4 Tablas creadas
- 29 Stored Procedures creados
- Datos: 5 Alumnos, 8 Obras, 12 Ejemplares, 3 Préstamos

---

## ?? Re-creación de la Base de Datos

Si necesitas recrear la base de datos desde cero:

1. Ejecutar solo `01_CreateDatabase.sql` (es idempotente)
2. Volver a ejecutar los scripts de Stored Procedures (2-5)

El script `01_CreateDatabase.sql` elimina automáticamente la base de datos si existe.

---

## ?? Estructura de la Base de Datos

```
BibliotecaDB
?
??? Alumnos (5 registros)
?   ??? PK: Id (IDENTITY)
?   ??? UQ: Dni
?   ??? IX: Dni, Email
?
??? Obras (8 registros)
?   ??? PK: Id (IDENTITY)
?   ??? UQ: Isbn
?   ??? IX: Isbn, Autor
?
??? Ejemplares (12 registros)
?   ??? PK: Id (IDENTITY)
?   ??? FK: ObraId ? Obras(Id) CASCADE
?   ??? UQ: NumeroInventario
?   ??? IX: ObraId, Disponible
?
??? Prestamos (3 registros)
    ??? PK: Id (IDENTITY)
    ??? FK: AlumnoId ? Alumnos(Id)
    ??? FK: EjemplarId ? Ejemplares(Id)
    ??? IX: AlumnoId, EjemplarId, Devuelto
```

---

## ?? Características Importantes

### **Normalización**
- Base de datos en **3NF** (Tercera Forma Normal)
- Sin redundancia de datos
- Relaciones bien definidas

### **Integridad Referencial**
- Foreign Keys con comportamiento CASCADE en Ejemplares
- Constraints CHECK para validación de datos
- Unique constraints en campos clave (DNI, ISBN, NumeroInventario)

### **Optimización**
- 9 índices no agrupados para búsquedas rápidas
- Índices filtrados para consultas específicas
- Stored Procedures para operaciones eficientes

### **Transacciones**
- `sp_Prestamo_Insert` usa transacciones
- `sp_Prestamo_MarcarDevuelto` usa transacciones
- Rollback automático en caso de error

### **Datos de Ejemplo**
- Alumnos con datos realistas
- Obras literarias clásicas
- Ejemplares con precios en pesos argentinos
- Préstamos en diferentes estados (activo, devuelto, vencido)

---

## ??? Troubleshooting

### **Error: "Database is in use"**
```sql
-- Solución: El script 01_CreateDatabase.sql ya maneja esto
-- con SET SINGLE_USER WITH ROLLBACK IMMEDIATE
```

### **Error: "Cannot drop database because it is currently in use"**
```sql
-- Cerrar todas las conexiones a BibliotecaDB y volver a ejecutar
```

### **Error en Stored Procedures**
```sql
-- Verificar que 01_CreateDatabase.sql se ejecutó correctamente
-- Los SPs requieren que las tablas existan
```

---

## ?? Notas

1. **Idempotencia**: El script `01_CreateDatabase.sql` puede ejecutarse múltiples veces sin problemas
2. **Stored Procedures**: Usan `CREATE OR ALTER` para ser idempotentes
3. **Transacciones**: Los SPs de préstamos garantizan consistencia de datos
4. **Bajas Lógicas**: Todos los deletes son lógicos (campo `Activo = 0`)

---

## ?? Referencias

- [SQL Server Best Practices](https://docs.microsoft.com/sql/relational-databases/best-practices)
- [Database Normalization](https://docs.microsoft.com/sql/relational-databases/tables/database-normalization)
- [Stored Procedures](https://docs.microsoft.com/sql/relational-databases/stored-procedures/stored-procedures-database-engine)

---

**¡La base de datos está lista para usar!** ??
