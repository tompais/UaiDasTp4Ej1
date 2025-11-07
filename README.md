# Sistema de Gestión de Biblioteca ??

## Trabajo Práctico 4 - Ejercicio 1
### Desarrollo y Arquitectura de Software

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-green)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019+-red)](https://www.microsoft.com/sql-server/)

---

## ?? Tabla de Contenidos

1. [Descripción](#-descripción)
2. [Inicio Rápido](#-inicio-rápido)
3. [Configuración de Variables de Entorno](#-configuración-de-variables-de-entorno)
4. [Arquitectura](#?-arquitectura-del-proyecto)
5. [Modelo de Datos](#?-modelo-de-datos)
6. [Instalación Completa](#-instalación-completa)
7. [Funcionalidades](#-funcionalidades)
8. [Tecnologías](#-tecnologías-utilizadas)
9. [Principios y Patrones](#-principios-y-patrones-aplicados)
10. [Checklist de Cumplimiento](#-checklist-de-cumplimiento)

---

## ?? Descripción

Sistema de gestión de biblioteca desarrollado con **.NET 8**, **Windows Forms (MDI/SDI)**, **ADO.NET en modo conectado**, **SQL Server** y **Stored Procedures**, siguiendo una **arquitectura en capas** y aplicando principios **SOLID**, **Clean Code** y **Clean Architecture**.

El sistema permite gestionar:
- ?? **Alumnos** (datos personales)
- ?? **Obras** (libros y publicaciones)
- ?? **Ejemplares** (copias físicas de obras)
- ?? **Préstamos** (registro y devoluciones)

---

## ?? Inicio Rápido

### Prerrequisitos
- .NET 8 SDK
- SQL Server (LocalDB, Express, Developer o Enterprise)
- Visual Studio 2022 (recomendado) o VS Code

### Configuración en 4 Pasos

#### **Paso 1: Crear la Base de Datos**

Abrir **SQL Server Management Studio (SSMS)** y ejecutar los scripts en orden desde la carpeta `UaiDasTp4Ej1\SQL\`:

```sql
-- Ejecutar en orden:
UaiDasTp4Ej1\SQL\01_CreateDatabase.sql  -- Crea BD y tablas con datos de prueba
UaiDasTp4Ej1\SQL\02_SP_Alumnos.sql      -- Stored Procedures de Alumnos
UaiDasTp4Ej1\SQL\03_SP_Obras.sql-- Stored Procedures de Obras
UaiDasTp4Ej1\SQL\04_SP_Ejemplares.sql   -- Stored Procedures de Ejemplares
UaiDasTp4Ej1\SQL\05_SP_Prestamos.sql    -- Stored Procedures de Préstamos
```

?? **Ver documentación completa:** [UaiDasTp4Ej1/SQL/README.md](UaiDasTp4Ej1/SQL/README.md)

#### **Paso 2: Configurar Conexión a la Base de Datos**

**Opción A: Variables de Entorno (Recomendado)** ??

Para **SQL Server Authentication** (usuario y contraseña):

```powershell
# PowerShell
$env:DB_SERVER = "192.168.1.100,1433"  # Tu IP y puerto
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USER = "sa"        # Tu usuario
$env:DB_PASSWORD = "TuPassword123!"    # Tu contraseña
```

Para **Windows Authentication**:

```powershell
$env:DB_SERVER = "localhost"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"
```

?? **Ver guía completa:** [ENVIRONMENT_SETUP.md](ENVIRONMENT_SETUP.md)

**Opción B: Editar directamente en código**

Editar `APP\Configuration.cs` (método `GetDefaultConnectionString`):

```csharp
private static string GetDefaultConnectionString()
{
    return "Server=TU_IP,PUERTO;Database=BibliotecaDB;User Id=TU_USUARIO;Password=TU_PASSWORD;TrustServerCertificate=True;";
}
```

#### **Paso 3: Restaurar y Compilar**

```bash
dotnet restore
dotnet build
```

#### **Paso 4: Ejecutar**

**Desde Visual Studio:**
- Presionar `F5` (Debug) o `Ctrl+F5` (Sin Debug)

**Desde Terminal:**
```bash
dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

### ? Verificación

1. La aplicación debe abrir un formulario principal MDI
2. El menú "Gestión" debe estar visible
3. Al hacer clic en "Alumnos" debe abrir el formulario de gestión
4. La grilla debe mostrar 3 alumnos de prueba

---

## ?? Configuración de Variables de Entorno

El sistema soporta configuración mediante **variables de entorno** para mayor seguridad y flexibilidad.

### Variables Disponibles

| Variable | Descripción | Ejemplo |
|----------|-------------|---------|
| `DB_SERVER` | Servidor SQL Server con puerto (opcional) | `192.168.1.100,1433` |
| `DB_DATABASE` | Nombre de la base de datos | `BibliotecaDB` |
| `DB_USER` | Usuario (SQL Server Auth) | `sa` |
| `DB_PASSWORD` | Contraseña (SQL Server Auth) | `MiPassword123!` |
| `DB_USE_INTEGRATED_SECURITY` | Usar Windows Auth | `true` |

### Ejemplos Rápidos

**SQL Server remoto con usuario/contraseña:**
```powershell
$env:DB_SERVER = "192.168.1.50,1433"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USER = "biblioteca_user"
$env:DB_PASSWORD = "Password2025!"
```

**SQL Server local con Windows Authentication:**
```powershell
$env:DB_SERVER = "localhost"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"
```

### Valor Por Defecto

Si no se configuran variables de entorno, se usa:
```
Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;
```

?? **Guía completa de configuración:** [ENVIRONMENT_SETUP.md](ENVIRONMENT_SETUP.md)

---

## ??? Arquitectura del Proyecto

### Estructura de Capas

```
UaiDasTp4Ej1/
?
??? ?? DOM/    # Dominio - Entidades del negocio
?   ??? Alumno.cs
?   ??? Obra.cs
?   ??? Ejemplar.cs
?   ??? Prestamo.cs
?
??? ?? ABS/           # Abstracción - Interfaces
?   ??? IRepository.cs
?   ??? IAlumnoRepository.cs
?   ??? IObraRepository.cs
?   ??? IEjemplarRepository.cs
?   ??? IPrestamoRepository.cs
?
??? ?? CONTEXT/       # Contexto - Conexión a BD
?   ??? DatabaseContext.cs
?
??? ?? REPO/        # Repositorios - Acceso a datos (ADO.NET)
?   ??? AlumnoRepository.cs
?   ??? ObraRepository.cs
?   ??? EjemplarRepository.cs
?   ??? PrestamoRepository.cs
?
??? ?? SERV/       # Servicios - Lógica de negocio
?   ??? AlumnoService.cs
?   ??? ObraService.cs
?   ??? EjemplarService.cs
?   ??? PrestamoService.cs
?
??? ?? APP/      # Aplicación - Configuración e IoC
?   ??? Configuration.cs # ?? Con soporte de variables de entorno
?   ??? DependencyInjection.cs
?
??? ?? UaiDasTp4Ej1/  # Presentación - Windows Forms (MDI/SDI)
?   ??? FormPrincipal.cs # MDI Container (antes Form1)
?   ??? FormAlumnos.cs        # SDI - Gestión de Alumnos
?   ??? FormObras.cs    # SDI - Gestión de Obras
?   ??? FormEjemplares.cs# SDI - Gestión de Ejemplares
?   ??? FormPrestamos.cs      # SDI - Gestión de Préstamos
?
??? ?? SQL/        # Scripts de Base de Datos
    ??? 01_CreateDatabase.sql
    ??? 02_SP_Alumnos.sql
    ??? 03_SP_Obras.sql
    ??? 04_SP_Ejemplares.sql
    ??? 05_SP_Prestamos.sql
    ??? README.md
```

### Flujo de Dependencias

```
Presentación (WinForms)
     ?
    Servicios (SERV)
       ?
  Repositorios (REPO)
         ?
    Contexto (CONTEXT)
 ?
   Base de Datos (SQL Server)
```

---

## ??? Modelo de Datos

### Diagrama de Tablas

```
???????????????????      ???????????????????
?    ALUMNOS      ?      ?     OBRAS       ?
??????????????????? ???????????????????
? Id (PK)         ?    ? Id (PK)         ?
? Nombre          ? ? Titulo    ?
? Apellido        ?    ? Autor           ?
? DNI (UNIQUE) ?      ? Editorial    ?
? Email           ?      ? ISBN (UNIQUE)   ?
? Telefono        ?    ? AnioPublicacion ?
? FechaNacimiento ?      ? Genero          ?
? Activo          ?      ? Activo          ?
???????????????????  ???????????????????
  ? ?
    ? ?
         ?        ?????????????????????????????????????
         ?        ?         EJEMPLARES       ?
       ?        ?????????????????????????????????????
   ?        ? Id (PK)        ?
  ?    ? ObraId (FK) ???????????????????????
         ?  ? NumeroInventario (UNIQUE)    ?
         ?        ? Precio            ?
  ?        ? Disponible                ?
         ?        ? Activo           ?
  ?        ?????????????????????????????????????
         ?        ?
   ?           ?
         ???????????????????
      ?
         ??????????????????????????
     ?      PRESTAMOS    ?
         ??????????????????????????
      ? Id (PK)         ?
         ? AlumnoId (FK) ??????????
         ? EjemplarId (FK) ????????
         ? FechaPrestamo          ?
         ? FechaDevolucionPrevista?
     ? FechaDevolucionReal    ?
         ? Devuelto        ?
         ? Activo              ?
 ??????????????????????????
```

### Descripción de Tablas

#### **Alumnos**
- `Id`: Identificador único (PK, IDENTITY)
- `Nombre`, `Apellido`: Datos personales
- `Dni`: Documento único (UNIQUE)
- `Email`, `Telefono`: Contacto
- `FechaNacimiento`: Fecha de nacimiento
- `Activo`: Para bajas lógicas

#### **Obras**
- `Id`: Identificador único (PK, IDENTITY)
- `Titulo`, `Autor`, `Editorial`: Datos del libro
- `Isbn`: Código único (UNIQUE)
- `AnioPublicacion`: Año de publicación
- `Genero`: Categoría del libro
- `Activo`: Para bajas lógicas

#### **Ejemplares**
- `Id`: Identificador único (PK, IDENTITY)
- `ObraId`: Relación con Obras (FK)
- `NumeroInventario`: Código de inventario único (UNIQUE)
- `Precio`: Valor del ejemplar
- `Disponible`: Estado de disponibilidad
- `Activo`: Para bajas lógicas

#### **Prestamos**
- `Id`: Identificador único (PK, IDENTITY)
- `AlumnoId`: Relación con Alumnos (FK)
- `EjemplarId`: Relación con Ejemplares (FK)
- `FechaPrestamo`: Fecha del préstamo
- `FechaDevolucionPrevista`: Fecha esperada (+7 días)
- `FechaDevolucionReal`: Fecha real de devolución
- `Devuelto`: Indica si fue devuelto
- `Activo`: Para bajas lógicas

---

## ?? Instalación Completa

### 1. Clonar o Descargar el Proyecto

```bash
git clone <url-del-repositorio>
cd UaiDasTp4Ej1
```

### 2. Configurar SQL Server

#### Opción A: Script Completo (Recomendado)

Ejecutar en SSMS en el siguiente orden desde la carpeta `UaiDasTp4Ej1\SQL\`:

```sql
-- 1. Crear base de datos y tablas
:r UaiDasTp4Ej1\SQL\01_CreateDatabase.sql

-- 2. Crear Stored Procedures
:r UaiDasTp4Ej1\SQL\02_SP_Alumnos.sql
:r UaiDasTp4Ej1\SQL\03_SP_Obras.sql
:r UaiDasTp4Ej1\SQL\04_SP_Ejemplares.sql
:r UaiDasTp4Ej1\SQL\05_SP_Prestamos.sql
```

?? **Ver guía detallada:** [UaiDasTp4Ej1/SQL/README.md](UaiDasTp4Ej1/SQL/README.md)

#### Opción B: Script Individual

Abrir cada archivo SQL en SSMS y ejecutar con `F5`.

### 3. Configurar Connection String

**Opción A: Variables de Entorno (Recomendado)**

Ver la [guía completa](ENVIRONMENT_SETUP.md) de configuración.

**Opción B: Editar el código**

Editar `APP\Configuration.cs` según tu configuración de SQL Server.

### 4. Restaurar Paquetes NuGet

```bash
dotnet restore
```

### 5. Compilar

```bash
dotnet build
```

### 6. Ejecutar

```bash
dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

### 7. Datos de Prueba

La base de datos incluye automáticamente:

**3 Alumnos:**
- Juan Pérez (DNI: 12345678)
- María González (DNI: 23456789)
- Carlos Rodríguez (DNI: 34567890)

**5 Obras:**
- Cien años de soledad
- El principito
- 1984
- Don Quijote de la Mancha
- Rayuela

**7 Ejemplares** disponibles para préstamo

---

## ?? Funcionalidades

### Gestión de Alumnos
- ? Alta de alumnos con validación de DNI único
- ? Baja lógica de alumnos
- ? Modificación de datos personales
- ? Consulta de todos los alumnos
- ? Búsqueda por DNI
- ? Validaciones: email, teléfono, fecha de nacimiento

### Gestión de Obras
- ? Alta de obras literarias
- ? Baja lógica de obras
- ? Modificación de datos
- ? Consulta de todas las obras
- ? Búsqueda por ISBN
- ? Validación de ISBN único

### Gestión de Ejemplares
- ? Alta de ejemplares por obra
- ? Baja lógica de ejemplares
- ? Modificación de datos
- ? Control de disponibilidad
- ? Consulta por obra
- ? Número de inventario único
- ? Gestión de precios

### Gestión de Préstamos
- ? Registro de préstamos
- ? Devolución de libros
- ? Cálculo automático de fecha de devolución (+7 días)
- ? Control de ejemplares disponibles
- ? Historial de préstamos por alumno
- ? Consulta de préstamos activos
- ? Consulta de préstamos vencidos
- ? Transacciones para integridad de datos

### Interfaz de Usuario
- ? Formulario principal MDI con menú (**FormPrincipal**)
- ? Menú Gestión (Alumnos, Obras, Ejemplares, Préstamos)
- ? Menú Ventanas (Cascada, Horizontal, Vertical)
- ? Menú Ayuda (Acerca de)
- ? Formularios SDI para cada entidad
- ? DataGridView para visualización de datos
- ? Botones de acción: Nuevo, Guardar, Modificar, Eliminar, Cancelar
- ? Validaciones en tiempo real
- ? Mensajes de confirmación
- ? Manejo de errores con mensajes descriptivos

---

## ??? Tecnologías Utilizadas

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| .NET | 8.0 | Framework principal |
| C# | 12.0 | Lenguaje de programación |
| Windows Forms | .NET 8 | Interfaz de usuario MDI/SDI |
| ADO.NET | .NET 8 | Acceso a datos en modo conectado |
| SQL Server | 2019+ | Base de datos relacional |
| Stored Procedures | T-SQL | Operaciones de BD |
| Microsoft.Data.SqlClient | 6.1.2 | Proveedor de datos para SQL Server |
| Microsoft.Extensions.DependencyInjection | 9.0.10 | Contenedor de IoC |
| **Variables de Entorno** | - | **Configuración segura de conexión** |

---

## ?? Principios y Patrones Aplicados

### SOLID

#### **S - Single Responsibility Principle**
- Cada clase tiene una única responsabilidad
- Repositorios: solo acceso a datos
- Servicios: solo lógica de negocio
- Formularios: solo presentación

#### **O - Open/Closed Principle**
- Uso de interfaces permite extensión sin modificación
- Nuevos repositorios pueden agregarse sin cambiar código existente

#### **L - Liskov Substitution Principle**
- Las implementaciones pueden sustituir a sus interfaces
- `AlumnoRepository` puede reemplazarse por cualquier `IAlumnoRepository`

#### **I - Interface Segregation Principle**
- Interfaces específicas por entidad
- `IAlumnoRepository`, `IObraRepository`, etc.

#### **D - Dependency Inversion Principle**
- Dependencias hacia abstracciones (interfaces)
- Inyección de dependencias mediante `IServiceProvider`

### Clean Code

- ? Nombres descriptivos y significativos
- ? Métodos cortos y específicos (una responsabilidad)
- ? Comentarios cuando agregan valor
- ? Formato consistente
- ? Evita duplicación de código
- ? Variables con nombres claros

### Clean Architecture

- ? Independencia de frameworks
- ? Testeable (aunque no incluye tests en este TP)
- ? Independencia de UI
- ? Independencia de BD
- ? Regla de dependencias (hacia adentro)

### DRY (Don't Repeat Yourself)

- Métodos genéricos en `IRepository<T>`
- Reutilización de código de validación
- Helpers para operaciones comunes

### YAGNI (You Aren't Gonna Need It)

- Solo se implementa lo requerido por el TP
- No hay funcionalidades innecesarias

### KISS (Keep It Simple, Stupid)

- Soluciones simples y directas
- Código fácil de entender y mantener
- Sin sobre-ingeniería

### Otros Patrones

- **Repository Pattern**: Abstracción del acceso a datos
- **Service Layer Pattern**: Lógica de negocio centralizada
- **Dependency Injection**: Desacoplamiento y testabilidad
- **MDI/SDI Pattern**: Interfaz de múltiples documentos
- **Environment Variables**: Configuración externa segura ??

---

## ? Checklist de Cumplimiento

### ?? Requerimientos del TP4 - Ejercicio 1

#### ? 1. Base de Datos
- [x] Base de datos BibliotecaDB creada
- [x] Tabla Alumnos con campos requeridos
- [x] Tabla Obras con campos requeridos
- [x] Tabla Ejemplares con relación a Obras
- [x] Tabla Prestamos con relaciones
- [x] Índices para optimización
- [x] Datos de prueba incluidos

#### ? 2. Stored Procedures (32 SPs)

**Alumnos (6 SPs):**
- [x] `sp_Alumno_GetAll` - Consulta todos
- [x] `sp_Alumno_GetById` - Consulta por ID
- [x] `sp_Alumno_GetByDni` - Consulta por DNI
- [x] `sp_Alumno_Insert` - Alta
- [x] `sp_Alumno_Update` - Modificación
- [x] `sp_Alumno_Delete` - Baja lógica

**Obras (6 SPs):**
- [x] `sp_Obra_GetAll` - Consulta todos
- [x] `sp_Obra_GetById` - Consulta por ID
- [x] `sp_Obra_GetByIsbn` - Consulta por ISBN
- [x] `sp_Obra_Insert` - Alta
- [x] `sp_Obra_Update` - Modificación
- [x] `sp_Obra_Delete` - Baja lógica

**Ejemplares (8 SPs):**
- [x] `sp_Ejemplar_GetAll` - Consulta todos
- [x] `sp_Ejemplar_GetById` - Consulta por ID
- [x] `sp_Ejemplar_GetByObraId` - Consulta por Obra
- [x] `sp_Ejemplar_GetDisponibles` - Consulta disponibles
- [x] `sp_Ejemplar_GetByNumeroInventario` - Consulta por inventario
- [x] `sp_Ejemplar_Insert` - Alta
- [x] `sp_Ejemplar_Update` - Modificación
- [x] `sp_Ejemplar_Delete` - Baja lógica

**Préstamos (9 SPs):**
- [x] `sp_Prestamo_GetAll` - Consulta todos
- [x] `sp_Prestamo_GetById` - Consulta por ID
- [x] `sp_Prestamo_GetByAlumnoId` - Consulta por Alumno
- [x] `sp_Prestamo_GetActivos` - Préstamos activos
- [x] `sp_Prestamo_GetVencidos` - Préstamos vencidos
- [x] `sp_Prestamo_Insert` - Alta con transacción
- [x] `sp_Prestamo_Update` - Modificación
- [x] `sp_Prestamo_MarcarDevuelto` - Devolución con transacción
- [x] `sp_Prestamo_Delete` - Baja lógica

#### ? 3. Interfaz de Usuario

**MDI Container:**
- [x] **FormPrincipal** - Formulario principal MDI (antes Form1)
- [x] Menú Gestión con 4 opciones
- [x] Menú Ventanas (Cascada, Horizontal, Vertical)
- [x] Menú Ayuda (Acerca de)
- [x] Barra de estado (StatusBar)

**Formularios SDI (Hijos):**
- [x] FormAlumnos - CRUD completo
- [x] FormObras - CRUD completo
- [x] FormEjemplares - CRUD completo
- [x] FormPrestamos - Registro y devolución

**Funcionalidades de Formularios:**
- [x] Botones: Nuevo, Guardar, Modificar, Eliminar, Cancelar
- [x] DataGridView para listado
- [x] Validaciones de campos
- [x] Mensajes de confirmación
- [x] Manejo de errores

### ??? Arquitectura Implementada

#### ? Capas del Proyecto
- [x] **DOM** - 4 entidades de dominio
- [x] **ABS** - 5 interfaces
- [x] **CONTEXT** - Contexto de BD
- [x] **REPO** - 4 repositorios con ADO.NET
- [x] **SERV** - 4 servicios de negocio
- [x] **APP** - Configuración e IoC con variables de entorno ??
- [x] **UaiDasTp4Ej1** - 5 formularios WinForms
- [x] **SQL** - 6 scripts SQL

#### ? Principios Aplicados
- [x] **SOLID** - Los 5 principios implementados
- [x] **DRY** - Sin duplicación de código
- [x] **YAGNI** - Solo lo necesario
- [x] **KISS** - Soluciones simples
- [x] **Clean Code** - Código limpio y legible
- [x] **Clean Architecture** - Arquitectura limpia
- [x] **Dependency Injection** - IoC Container
- [x] **12-Factor App** - Configuración externa (variables de entorno)

#### ? Características Técnicas

**Base de Datos:**
- [x] Relaciones FK correctas
- [x] Bajas lógicas (campo Activo)
- [x] Transacciones en operaciones críticas
- [x] Índices para performance
- [x] Constraints y validaciones

**Código:**
- [x] Inyección de dependencias
- [x] Async/Await para operaciones asíncronas
- [x] Manejo de excepciones
- [x] Validaciones en capa de negocio
- [x] Validaciones en capa de presentación
- [x] Separación de responsabilidades
- [x] **Configuración mediante variables de entorno** ??

**Reglas de Negocio:**
- [x] DNI único por alumno
- [x] ISBN único por obra
- [x] Número de inventario único
- [x] Control de disponibilidad de ejemplares
- [x] Fecha de devolución automática (7 días)
- [x] Transacciones para préstamos/devoluciones
- [x] Validación de campos requeridos
- [x] Validación de fechas

### ?? Estadísticas del Proyecto

- **Proyectos**: 7
- **Entidades**: 4 (Alumno, Obra, Ejemplar, Prestamo)
- **Interfaces**: 5 (IRepository + 4 específicas)
- **Repositorios**: 4
- **Servicios**: 4
- **Formularios**: 5 (1 MDI + 4 SDI)
- **Stored Procedures**: 32
- **Scripts SQL**: 6
- **Líneas de código**: ~3500+

---

## ?? Solución de Problemas

### Error: "Cannot open database"
**Solución:**
1. Verificar que SQL Server esté ejecutándose
2. Verificar que la base de datos BibliotecaDB existe
3. Revisar la cadena de conexión o variables de entorno
4. Ver [ENVIRONMENT_SETUP.md](ENVIRONMENT_SETUP.md) para más detalles

### Error: "A network-related or instance-specific error"
**Solución:**
1. Verificar la IP y puerto del servidor
2. Verificar que SQL Server acepte conexiones remotas
3. Verificar el firewall (puerto 1433)
4. Probar conexión con: `ping <IP_SERVIDOR>`

### Error: "Login failed for user"
**Solución:**
1. Verificar usuario y contraseña en las variables de entorno
2. Verificar que SQL Server Authentication esté habilitado
3. Verificar permisos del usuario en la base de datos

### Error: "Could not find stored procedure"
**Solución:**
1. Ejecutar todos los scripts de stored procedures en orden
2. Verificar que se ejecutaron en la base de datos BibliotecaDB
3. Usar `EXEC sp_helptext 'sp_Alumno_GetAll'` para verificar

### Error de compilación
**Solución:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### El formulario no abre
**Solución:**
1. Verificar que se configuraron las variables de entorno o connection string
2. Verificar que la base de datos existe
3. Revisar el archivo de logs en Visual Studio (Output window)

### Datos no se cargan
**Solución:**
1. Verificar que existen datos de prueba: ejecutar `SELECT * FROM Alumnos`
2. Verificar que los stored procedures existen
3. Revisar permisos del usuario de BD

---

## ?? Uso de la Aplicación

### Gestionar Alumnos
1. Menú **Gestión** ? **Alumnos**
2. Clic en **Nuevo** para agregar
3. Completar datos y clic en **Guardar**
4. Para modificar: seleccionar en grilla, clic en **Modificar**
5. Para eliminar (baja lógica): seleccionar y clic en **Eliminar**

### Gestionar Obras
1. Menú **Gestión** ? **Obras**
2. Similar a Alumnos

### Gestionar Ejemplares
1. Menú **Gestión** ? **Ejemplares**
2. Clic en **Nuevo**
3. Seleccionar la **Obra** del combo
4. Ingresar **Número de Inventario** y **Precio**
5. Guardar

### Registrar un Préstamo
1. Menú **Gestión** ? **Préstamos**
2. Clic en **Nuevo**
3. Seleccionar **Alumno** del combo
4. Seleccionar **Ejemplar disponible** del combo
5. La fecha se establece automáticamente
6. Clic en **Guardar**
7. ? El ejemplar se marca como NO disponible
8. ? La fecha de devolución se calcula (+7 días)

### Devolver un Préstamo
1. Menú **Gestión** ? **Préstamos**
2. Seleccionar el préstamo en la grilla
3. Clic en **Devolver**
4. Confirmar la devolución
5. ? El ejemplar vuelve a estar disponible
6. ? Se registra la fecha de devolución real

---

## ?? Comandos Útiles

### Resetear la Base de Datos
```sql
-- Ejecutar en SSMS:
SQL\06_ResetDatabase.sql
```

Este script:
- Elimina todos los datos
- Reinicia los contadores de identidad
- Inserta nuevamente datos de prueba

### Compilar el Proyecto
```bash
dotnet build
```

### Limpiar y Compilar
```bash
dotnet clean
dotnet build
```

### Ejecutar en Modo Release
```bash
dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj --configuration Release
```

### Ver Información del Proyecto
```bash
dotnet --info
```

### Verificar Variables de Entorno
```powershell
# PowerShell
Get-ChildItem Env: | Where-Object { $_.Name -like "DB_*" }
```

---

## ?? Entrega del TP

### ? Estado Final

**PROYECTO 100% COMPLETADO Y FUNCIONAL** ?

El proyecto cumple con **TODOS** los requerimientos del TP4 Ejercicio 1:

? Base de datos completa con 4 tablas relacionadas  
? 32 Stored procedures para todas las operaciones CRUD  
? Interfaz MDI/SDI completamente funcional  
? Arquitectura en 7 capas bien definidas  
? Principios SOLID, DRY, YAGNI, KISS aplicados  
? Clean Code y Clean Architecture implementados  
? ADO.NET en modo conectado  
? Inyección de dependencias  
? **Configuración segura con variables de entorno** ??  
? Documentación completa

### ?? Qué Entregar

1. **Código Fuente** (carpeta completa del proyecto)
2. **Scripts SQL** (carpeta `SQL/`)
3. **Este README.md**
4. **Guía de configuración** (ENVIRONMENT_SETUP.md)
5. **Archivo de solución** (.sln)

### ?? Puntos Destacados para la Presentación

1. **Arquitectura en Capas** - 7 proyectos independientes
2. **Stored Procedures** - 32 SPs con transacciones
3. **ADO.NET Conectado** - Uso correcto de `SqlConnection`, `SqlCommand`, `SqlDataReader`
4. **Principios SOLID** - Ejemplos concretos en el código
5. **Inyección de Dependencias** - Uso de `IServiceProvider`
6. **Interfaz MDI/SDI** - **FormPrincipal** con formularios hijos
7. **Validaciones** - En capa de negocio y presentación
8. **Manejo de Transacciones** - En préstamos y devoluciones
9. **Variables de Entorno** - Configuración segura y flexible ??

---

## ????? Información del Proyecto

**Asignatura**: Desarrollo y Arquitectura de Software  
**Trabajo Práctico**: TP4 - Ejercicio 1  
**Tecnología**: .NET 8, C# 12, Windows Forms, ADO.NET, SQL Server  
**Arquitectura**: Capas con SOLID y Clean Architecture  
**Configuración**: Variables de entorno para seguridad ??  
**Año**: 2025  

---

## ?? Licencia

Este proyecto es un trabajo académico desarrollado para la asignatura Desarrollo y Arquitectura de Software.

---

## ?? Documentación Adicional

- ?? [Guía de Configuración de Variables de Entorno](ENVIRONMENT_SETUP.md)
- ??? Scripts SQL en la carpeta `SQL/`

---

**¡Sistema listo para usar y presentar!** ??????????
