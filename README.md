# Sistema de Gesti�n de Biblioteca

## Trabajo Pr�ctico 4 - Ejercicio 1
### Desarrollo y Arquitectura de Software

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-green)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019+-red)](https://www.microsoft.com/sql-server/)

---

## Tabla de Contenidos

1. [Descripci�n](#-descripci�n)
2. [Inicio R�pido](#-inicio-r�pido)
3. [Configuraci�n de Variables de Entorno](#-configuraci�n-de-variables-de-entorno)
4. [Arquitectura](#?-arquitectura-del-proyecto)
5. [Modelo de Datos](#?-modelo-de-datos)
6. [Instalaci�n Completa](#-instalaci�n-completa)
7. [Funcionalidades](#-funcionalidades)
8. [Tecnolog�as](#-tecnolog�as-utilizadas)
9. [Principios y Patrones](#-principios-y-patrones-aplicados)
10. [Checklist de Cumplimiento](#-checklist-de-cumplimiento)

---

## Descripci�n

Sistema de gesti�n de biblioteca desarrollado con **.NET 8**, **Windows Forms (MDI/SDI)**, **ADO.NET en modo conectado**, **SQL Server** y **Stored Procedures**, siguiendo una **arquitectura en capas** y aplicando principios **SOLID**, **Clean Code** y **Clean Architecture**.

El sistema permite gestionar:
- **Alumnos** (datos personales)
- **Obras** (libros y publicaciones)
- **Ejemplares** (copias f�sicas de obras)
- **Pr�stamos** (registro y devoluciones)

---

## Inicio R�pido

### Prerrequisitos
- .NET 8 SDK
- SQL Server (LocalDB, Express, Developer o Enterprise)
- Visual Studio 2022 (recomendado) o VS Code

### Configuraci�n en 4 Pasos

#### **Paso 1: Crear la Base de Datos**

Abrir **SQL Server Management Studio (SSMS)** y ejecutar los scripts en orden desde la carpeta `UaiDasTp4Ej1\SQL\`:

```sql
-- Ejecutar en orden:
UaiDasTp4Ej1\SQL\01_CreateDatabase.sql -- Crea BD y tablas con datos de prueba
UaiDasTp4Ej1\SQL\02_SP_Alumnos.sql -- Stored Procedures de Alumnos
UaiDasTp4Ej1\SQL\03_SP_Obras.sql-- Stored Procedures de Obras
UaiDasTp4Ej1\SQL\04_SP_Ejemplares.sql -- Stored Procedures de Ejemplares
UaiDasTp4Ej1\SQL\05_SP_Prestamos.sql -- Stored Procedures de Pr�stamos
```

 **Ver documentaci�n completa:** [UaiDasTp4Ej1/SQL/README.md](UaiDasTp4Ej1/SQL/README.md)

#### **Paso 2: Configurar Conexi�n a la Base de Datos**

**Opci�n A: Variables de Entorno (Recomendado)**

Para **SQL Server Authentication** (usuario y contrase�a):

```powershell
# PowerShell
$env:DB_SERVER = "192.168.1.100,1433" # Tu IP y puerto
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USER = "sa" # Tu usuario
$env:DB_PASSWORD = "TuPassword123!" # Tu contrase�a
```

Para **Windows Authentication**:

```powershell
$env:DB_SERVER = "localhost"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"
```

**Opci�n B: Editar directamente en c�digo**

Editar `APP\Configuration.cs` (m�todo `GetDefaultConnectionString`):

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

### ? Verificaci�n

1. La aplicaci�n debe abrir un formulario principal MDI
2. El men� "Gesti�n" debe estar visible
3. Al hacer clic en "Alumnos" debe abrir el formulario de gesti�n
4. La grilla debe mostrar 3 alumnos de prueba

---

## Configuraci�n de Variables de Entorno

El sistema soporta configuraci�n mediante **variables de entorno** para mayor seguridad y flexibilidad.

### Variables Disponibles

| Variable | Descripci�n | Ejemplo |
|----------|-------------|---------|
| `DB_SERVER` | Servidor SQL Server con puerto (opcional) | `192.168.1.100,1433` |
| `DB_DATABASE` | Nombre de la base de datos | `BibliotecaDB` |
| `DB_USER` | Usuario (SQL Server Auth) | `sa` |
| `DB_PASSWORD` | Contrase�a (SQL Server Auth) | `MiPassword123!` |
| `DB_USE_INTEGRATED_SECURITY` | Usar Windows Auth | `true` |

### C�mo Configurar las Variables de Entorno

#### **Opci�n 1: Variables de Sistema (Windows) - Recomendado para desarrollo**

1. Presionar `Win + R` y escribir: `sysdm.cpl`
2. Ir a la pesta�a **"Opciones avanzadas"**
3. Clic en **"Variables de entorno"**
4. En **"Variables del sistema"**, clic en **"Nueva"**
5. Agregar cada variable (DB_SERVER, DB_DATABASE, DB_USER, DB_PASSWORD)
6. Clic en **Aceptar** y **reiniciar Visual Studio** o el IDE

#### **Opci�n 2: PowerShell (Temporal - solo para la sesi�n actual)**

Para **SQL Server Authentication**:
```powershell
$env:DB_SERVER = "192.168.1.100,1433"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USER = "sa"
$env:DB_PASSWORD = "TuPasswordSeguro123!"

dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

Para **Windows Authentication**:
```powershell
$env:DB_SERVER = "localhost"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"

dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

#### **Opci�n 3: CMD/S�mbolo del Sistema (Temporal)**

```cmd
set DB_SERVER=192.168.1.100,1433
set DB_DATABASE=BibliotecaDB
set DB_USER=sa
set DB_PASSWORD=TuPasswordSeguro123!

dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

#### **Opci�n 4: Archivo launchSettings.json (Visual Studio)**

Editar el archivo `UaiDasTp4Ej1/Properties/launchSettings.json`:

```json
{
 "profiles": {
 "UaiDasTp4Ej1": {
 "commandName": "Project",
 "environmentVariables": {
 "DB_SERVER": "192.168.1.100,1433",
 "DB_DATABASE": "BibliotecaDB",
 "DB_USER": "sa",
 "DB_PASSWORD": "TuPasswordSeguro123!"
 }
 }
 }
}
```

#### **Opci�n 5: Archivo .env (NO INCLUIR EN GIT)**

Crear un archivo `.env` en la ra�z del proyecto:

```env
DB_SERVER=192.168.1.100,1433
DB_DATABASE=BibliotecaDB
DB_USER=sa
DB_PASSWORD=TuPasswordSeguro123!
```

**IMPORTANTE:** Agregar `.env` al archivo `.gitignore` para no subir contrase�as al repositorio.

### Verificar Variables de Entorno

**PowerShell:**
```powershell
# Ver todas las variables DB_*
Get-ChildItem Env: | Where-Object { $_.Name -like "DB_*" }

# Ver una variable espec�fica
$env:DB_SERVER
$env:DB_DATABASE
```

**CMD:**
```cmd
set DB_SERVER
set DB_DATABASE
```

### Ejemplos de Configuraci�n

**Ejemplo 1: SQL Server Local con SQL Authentication**
```powershell
$env:DB_SERVER = "localhost"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USER = "sa"
$env:DB_PASSWORD = "Admin123!"
```

**Connection String resultante:**
```
Server=localhost;Database=BibliotecaDB;User Id=sa;Password=Admin123!;TrustServerCertificate=True;
```

**Ejemplo 2: SQL Server Remoto con Puerto Espec�fico**
```powershell
$env:DB_SERVER = "192.168.1.50,1433"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USER = "biblioteca_user"
$env:DB_PASSWORD = "MiPassword2025!"
```

**Connection String resultante:**
```
Server=192.168.1.50,1433;Database=BibliotecaDB;User Id=biblioteca_user;Password=MiPassword2025!;TrustServerCertificate=True;
```

**Ejemplo 3: SQL Server con Windows Authentication**
```powershell
$env:DB_SERVER = "DESKTOP-PC\SQLEXPRESS"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"
```

**Connection String resultante:**
```
Server=DESKTOP-PC\SQLEXPRESS;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;
```

**Ejemplo 4: LocalDB**
```powershell
$env:DB_SERVER = "(localdb)\mssqllocaldb"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"
```

**Connection String resultante:**
```
Server=(localdb)\mssqllocaldb;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;
```

### Valor Por Defecto

Si **NO** se configuran las variables de entorno, la aplicaci�n usar� autom�ticamente:

```
Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;
```

Esto es �til para desarrollo r�pido sin configuraci�n adicional.

### Mejores Pr�cticas de Seguridad

**? Hacer:**
- Usar variables de entorno para datos sensibles (contrase�as, IPs)
- Agregar `.env` al `.gitignore`
- Usar variables de sistema en entornos de producci�n
- Documentar qu� variables son necesarias
- Usar contrase�as fuertes

**? NO Hacer:**
- Nunca subir contrase�as al repositorio Git
- No compartir archivos `.env` con contrase�as reales
- No hardcodear contrase�as en el c�digo
- No dejar contrase�as por defecto en producci�n

---

## Arquitectura del Proyecto

### Estructura de Capas

```
UaiDasTp4Ej1/
│
├── DOM/ # Dominio - Entidades del negocio
│ ├── Alumno.cs
│ ├── Obra.cs
│ ├── Ejemplar.cs
│ ├── Prestamo.cs
│
├── ABS/ # Abstracci�n - Interfaces
│ ├── IRepository.cs
│ ├── IAlumnoRepository.cs
│ ├── IObraRepository.cs
│ ├── IEjemplarRepository.cs
│ ├── IPrestamoRepository.cs
│
├── CONTEXT/ # Contexto - Conexi�n a BD
│ ├── DatabaseContext.cs
│
├── REPO/ # Repositorios - Acceso a datos (ADO.NET)
│ ├── AlumnoRepository.cs
│ ├── ObraRepository.cs
│ ├── EjemplarRepository.cs
│ ├── PrestamoRepository.cs
│
├── SERV/ # Servicios - L�gica de negocio
│ ├── AlumnoService.cs
│ ├── ObraService.cs
│ ├── EjemplarService.cs
│ ├── PrestamoService.cs
│
├── APP/ # Aplicaci�n - Configuraci�n e IoC
│ ├── Configuration.cs # Con soporte de variables de entorno
│ ├── DependencyInjection.cs
│
├── UaiDasTp4Ej1/ # Presentaci�n - Windows Forms (MDI/SDI)
│ ├── FormPrincipal.cs # MDI Container (antes Form1)
│ ├── FormAlumnos.cs # SDI - Gesti�n de Alumnos
│ ├── FormObras.cs # SDI - Gesti�n de Obras
│ ├── FormEjemplares.cs# SDI - Gesti�n de Ejemplares
│ ├── FormPrestamos.cs # SDI - Gesti�n de Pr�stamos
│
├── SQL/ # Scripts de Base de Datos
 ├── 01_CreateDatabase.sql
 ├── 02_SP_Alumnos.sql
 ├── 03_SP_Obras.sql
 ├── 04_SP_Ejemplares.sql
 ├── 05_SP_Prestamos.sql
 ├── README.md
```

### Flujo de Dependencias

```
Presentaci�n (WinForms)
│
 Servicios (SERV)
│
 Repositorios (REPO)
│
 Contexto (CONTEXT)
│
 Base de Datos (SQL Server)
```

---

## Modelo de Datos

### Diagrama de Tablas

```
┌─────────────────────┐      ┌─────────────────────┐
│      ALUMNOS        │      │       OBRAS         │
├─────────────────────┤      ├─────────────────────┤
│ Id (PK)             │      │ Id (PK)             │
│ Nombre              │      │ Titulo              │
│ Apellido            │      │ Autor               │
│ DNI (UNIQUE)        │      │ Editorial           │
│ Email               │      │ ISBN (UNIQUE)       │
│ Telefono            │      │ AnioPublicacion     │
│ FechaNacimiento     │      │ Genero              │
│ Activo              │      │ Activo              │
└─────────────────────┘      └─────────────────────┘
        │                             │
        │                             │
        │        ┌────────────────────┴────────────────────┐
        │        │           EJEMPLARES                    │
        │        ├─────────────────────────────────────────┤
        │        │ Id (PK)                                 │
        │        │ ObraId (FK) ────────────────────────────┘
        │        │ NumeroInventario (UNIQUE)               │
        │        │ Precio                                  │
        │        │ Disponible                              │
        │        │ Activo                                  │
        │        └─────────────────────────────────────────┘
        │                     │
        │                     │
        └─────────┬───────────┘
                  │
        ┌─────────┴───────────────────┐
        │       PRESTAMOS             │
        ├─────────────────────────────┤
        │ Id (PK)                     │
        │ AlumnoId (FK) ──────────────┘
        │ EjemplarId (FK) ────────────┘
        │ FechaPrestamo               │
        │ FechaDevolucionPrevista     │
        │ FechaDevolucionReal         │
        │ Devuelto                    │
        │ Activo                      │
        └─────────────────────────────┘
```

### Descripci�n de Tablas

#### **Alumnos**
- `Id`: Identificador �nico (PK, IDENTITY)
- `Nombre`, `Apellido`: Datos personales
- `Dni`: Documento �nico (UNIQUE)
- `Email`, `Telefono`: Contacto
- `FechaNacimiento`: Fecha de nacimiento
- `Activo`: Para bajas l�gicas

#### **Obras**
- `Id`: Identificador �nico (PK, IDENTITY)
- `Titulo`, `Autor`, `Editorial`: Datos del libro
- `Isbn`: C�digo �nico (UNIQUE)
- `AnioPublicacion`: A�o de publicaci�n
- `Genero`: Categor�a del libro
- `Activo`: Para bajas l�gicas

#### **Ejemplares**
- `Id`: Identificador �nico (PK, IDENTITY)
- `ObraId`: Relaci�n con Obras (FK)
- `NumeroInventario`: C�digo de inventario �nico (UNIQUE)
- `Precio`: Valor del ejemplar
- `Disponible`: Estado de disponibilidad
- `Activo`: Para bajas l�gicas

#### **Prestamos**
- `Id`: Identificador �nico (PK, IDENTITY)
- `AlumnoId`: Relaci�n con Alumnos (FK)
- `EjemplarId`: Relaci�n con Ejemplares (FK)
- `FechaPrestamo`: Fecha del pr�stamo
- `FechaDevolucionPrevista`: Fecha esperada (+7 d�as)
- `FechaDevolucionReal`: Fecha real de devoluci�n
- `Devuelto`: Indica si fue devuelto
- `Activo`: Para bajas l�gicas

---

## Instalaci�n Completa

### 1. Clonar o Descargar el Proyecto

```bash
git clone <url-del-repositorio>
cd UaiDasTp4Ej1
```

### 2. Configurar SQL Server

#### Opci�n A: Script Completo (Recomendado)

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

 **Ver gu�a detallada:** [UaiDasTp4Ej1/SQL/README.md](UaiDasTp4Ej1/SQL/README.md)

#### Opci�n B: Script Individual

Abrir cada archivo SQL en SSMS y ejecutar con `F5`.

### 3. Configurar Connection String

**Opci�n A: Variables de Entorno (Recomendado)**

Ver la secci�n [Configuraci�n de Variables de Entorno](#-configuraci�n-de-variables-de-entorno) para detalles completos.

**Opci�n B: Editar el c�digo**

Editar `APP\Configuration.cs` seg�n tu configuraci�n de SQL Server.

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

La base de datos incluye autom�ticamente:

**3 Alumnos:**
- Juan P�rez (DNI: 12345678)
- Mar�a Gonz�lez (DNI: 23456789)
- Carlos Rodr�guez (DNI: 34567890)

**5 Obras:**
- Cien a�os de soledad
- El principito
- 1984
- Don Quijote de la Mancha
- Rayuela

**7 Ejemplares** disponibles para pr�stamo

---

## Funcionalidades

### Gesti�n de Alumnos
- ? Alta de alumnos con validaci�n de DNI �nico
- ? Baja l�gica de alumnos
- ? Modificaci�n de datos personales
- ? Consulta de todos los alumnos
- ? B�squeda por DNI
- ? Validaciones: email, tel�fono, fecha de nacimiento

### Gesti�n de Obras
- ? Alta de obras literarias
- ? Baja l�gica de obras
- ? Modificaci�n de datos
- ? Consulta de todas las obras
- ? B�squeda por ISBN
- ? Validaci�n de ISBN �nico

### Gesti�n de Ejemplares
- ? Alta de ejemplares por obra
- ? Baja l�gica de ejemplares
- ? Modificaci�n de datos
- ? Control de disponibilidad
- ? Consulta por obra
- ? N�mero de inventario �nico
- ? Gesti�n de precios

### Gesti�n de Pr�stamos
- ? Registro de pr�stamos
- ? Devoluci�n de libros
- ? C�lculo autom�tico de fecha de devoluci�n (+7 d�as)
- ? Control de ejemplares disponibles
- ? Historial de pr�stamos por alumno
- ? Consulta de pr�stamos activos
- ? Consulta de pr�stamos vencidos
- ? Transacciones para integridad de datos

### Interfaz de Usuario
- ? Formulario principal MDI con men� (**FormPrincipal**)
- ? Men� Gesti�n (Alumnos, Obras, Ejemplares, Pr�stamos)
- ? Men� Ventanas (Cascada, Horizontal, Vertical)
- ? Men� Ayuda (Acerca de)
- ? Formularios SDI para cada entidad
- ? DataGridView para visualizaci�n de datos
- ? Botones de acci�n: Nuevo, Guardar, Modificar, Eliminar, Cancelar
- ? Validaciones en tiempo real
- ? Mensajes de confirmaci�n
- ? Manejo de errores con mensajes descriptivos

---

## Tecnolog�as Utilizadas

| Tecnolog�a | Versi�n | Prop�sito |
|------------|---------|-----------|
| .NET | 8.0 | Framework principal |
| C# | 12.0 | Lenguaje de programaci�n |
| Windows Forms | .NET 8 | Interfaz de usuario MDI/SDI |
| ADO.NET | .NET 8 | Acceso a datos en modo conectado |
| SQL Server | 2019+ | Base de datos relacional |
| Stored Procedures | T-SQL | Operaciones de BD |
| Microsoft.Data.SqlClient | 6.1.2 | Proveedor de datos para SQL Server |
| Microsoft.Extensions.DependencyInjection | 9.0.10 | Contenedor de IoC |
| **Variables de Entorno** | - | **Configuraci�n segura de conexi�n** |

---

## Principios y Patrones Aplicados

### SOLID

#### **S - Single Responsibility Principle**
- Cada clase tiene una �nica responsabilidad
- Repositorios: solo acceso a datos
- Servicios: solo l�gica de negocio
- Formularios: solo presentaci�n

#### **O - Open/Closed Principle**
- Uso de interfaces permite extensi�n sin modificaci�n
- Nuevos repositorios pueden agregarse sin cambiar c�digo existente

#### **L - Liskov Substitution Principle**
- Las implementaciones pueden sustituir a sus interfaces
- `AlumnoRepository` puede reemplazarse por cualquier `IAlumnoRepository`

#### **I - Interface Segregation Principle**
- Interfaces espec�ficas por entidad
- `IAlumnoRepository`, `IObraRepository`, etc.

#### **D - Dependency Inversion Principle**
- Dependencias hacia abstracciones (interfaces)
- Inyecci�n de dependencias mediante `IServiceProvider`

### Clean Code

- ? Nombres descriptivos y significativos
- ? M�todos cortos y espec�ficos (una responsabilidad)
- ? Comentarios cuando agregan valor
- ? Formato consistente
- ? Evita duplicaci�n de c�digo
- ? Variables con nombres claros

### Clean Architecture

- ? Independencia de frameworks
- ? Testeable (aunque no incluye tests en este TP)
- ? Independencia de UI
- ? Independencia de BD
- ? Regla de dependencias (hacia adentro)

### DRY (Don't Repeat Yourself)

- M�todos gen�ricos en `IRepository<T>`
- Reutilizaci�n de c�digo de validaci�n
- Helpers para operaciones comunes

### YAGNI (You Aren't Gonna Need It)

- Solo se implementa lo requerido por el TP
- No hay funcionalidades innecesarias

### KISS (Keep It Simple, Stupid)

- Soluciones simples y directas
- C�digo f�cil de entender y mantener
- Sin sobre-ingenier�a

### Otros Patrones

- **Repository Pattern**: Abstracci�n del acceso a datos
- **Service Layer Pattern**: L�gica de negocio centralizada
- **Dependency Injection**: Desacoplamiento y testabilidad
- **MDI/SDI Pattern**: Interfaz de m�ltiples documentos
- **Environment Variables**: Configuraci�n externa segura

---

## ? Checklist de Cumplimiento

### Requerimientos del TP4 - Ejercicio 1

#### ? 1. Base de Datos
- [x] Base de datos BibliotecaDB creada
- [x] Tabla Alumnos con campos requeridos
- [x] Tabla Obras con campos requeridos
- [x] Tabla Ejemplares con relaci�n a Obras
- [x] Tabla Prestamos con relaciones
- [x] �ndices para optimizaci�n
- [x] Datos de prueba incluidos

#### ? 2. Stored Procedures (32 SPs)

**Alumnos (6 SPs):**
- [x] `sp_Alumno_GetAll` - Consulta todos
- [x] `sp_Alumno_GetById` - Consulta por ID
- [x] `sp_Alumno_GetByDni` - Consulta por DNI
- [x] `sp_Alumno_Insert` - Alta
- [x] `sp_Alumno_Update` - Modificaci�n
- [x] `sp_Alumno_Delete` - Baja l�gica

**Obras (6 SPs):**
- [x] `sp_Obra_GetAll` - Consulta todos
- [x] `sp_Obra_GetById` - Consulta por ID
- [x] `sp_Obra_GetByIsbn` - Consulta por ISBN
- [x] `sp_Obra_Insert` - Alta
- [x] `sp_Obra_Update` - Modificaci�n
- [x] `sp_Obra_Delete` - Baja l�gica

**Ejemplares (8 SPs):**
- [x] `sp_Ejemplar_GetAll` - Consulta todos
- [x] `sp_Ejemplar_GetById` - Consulta por ID
- [x] `sp_Ejemplar_GetByObraId` - Consulta por Obra
- [x] `sp_Ejemplar_GetDisponibles` - Consulta disponibles
- [x] `sp_Ejemplar_GetByNumeroInventario` - Consulta por inventario
- [x] `sp_Ejemplar_Insert` - Alta
- [x] `sp_Ejemplar_Update` - Modificaci�n
- [x] `sp_Ejemplar_Delete` - Baja l�gica

**Pr�stamos (9 SPs):**
- [x] `sp_Prestamo_GetAll` - Consulta todos
- [x] `sp_Prestamo_GetById` - Consulta por ID
- [x] `sp_Prestamo_GetByAlumnoId` - Consulta por Alumno
- [x] `sp_Prestamo_GetActivos` - Pr�stamos activos
- [x] `sp_Prestamo_GetVencidos` - Pr�stamos vencidos
- [x] `sp_Prestamo_Insert` - Alta con transacci�n
- [x] `sp_Prestamo_Update` - Modificaci�n
- [x] `sp_Prestamo_MarcarDevuelto` - Devoluci�n con transacci�n
- [x] `sp_Prestamo_Delete` - Baja l�gica

#### ? 3. Interfaz de Usuario

**MDI Container:**
- [x] **FormPrincipal** - Formulario principal MDI (antes Form1)
- [x] Men� Gesti�n con 4 opciones
- [x] Men� Ventanas (Cascada, Horizontal, Vertical)
- [x] Men� Ayuda (Acerca de)
- [x] Barra de estado (StatusBar)

**Formularios SDI (Hijos):**
- [x] FormAlumnos - CRUD completo
- [x] FormObras - CRUD completo
- [x] FormEjemplares - CRUD completo
- [x] FormPrestamos - Registro y devoluci�n

**Funcionalidades de Formularios:**
- [x] Botones: Nuevo, Guardar, Modificar, Eliminar, Cancelar
- [x] DataGridView para listado
- [x] Validaciones de campos
- [x] Mensajes de confirmaci�n
- [x] Manejo de errores

### Arquitectura Implementada

#### ? Capas del Proyecto
- [x] **DOM** - 4 entidades de dominio
- [x] **ABS** - 5 interfaces
- [x] **CONTEXT** - Contexto de BD
- [x] **REPO** - 4 repositorios con ADO.NET
- [x] **SERV** - 4 servicios de negocio
- [x] **APP** - Configuraci�n e IoC con variables de entorno
- [x] **UaiDasTp4Ej1** - 5 formularios WinForms
- [x] **SQL** - 6 scripts SQL

#### ? Principios Aplicados
- [x] **SOLID** - Los 5 principios implementados
- [x] **DRY** - Sin duplicaci�n de c�digo
- [x] **YAGNI** - Solo lo necesario
- [x] **KISS** - Soluciones simples
- [x] **Clean Code** - C�digo limpio y legible
- [x] **Clean Architecture** - Arquitectura limpia
- [x] **Dependency Injection** - IoC Container
- [x] **12-Factor App** - Configuraci�n externa (variables de entorno)

#### ? Caracter�sticas T�cnicas

**Base de Datos:**
- [x] Relaciones FK correctas
- [x] Bajas l�gicas (campo Activo)
- [x] Transacciones en operaciones cr�ticas
- [x] �ndices para performance
- [x] Constraints y validaciones

**C�digo:**
- [x] Inyecci�n de dependencias
- [x] Async/Await para operaciones as�ncronas
- [x] Manejo de excepciones
- [x] Validaciones en capa de negocio
- [x] Validaciones en capa de presentaci�n
- [x] Separaci�n de responsabilidades
- [x] **Configuraci�n mediante variables de entorno**

**Reglas de Negocio:**
- [x] DNI �nico por alumno
- [x] ISBN �nico por obra
- [x] N�mero de inventario �nico
- [x] Control de disponibilidad de ejemplares
- [x] Fecha de devoluci�n autom�tica (7 d�as)
- [x] Transacciones para pr�stamos/devoluciones
- [x] Validaci�n de campos requeridos
- [x] Validaci�n de fechas

### Estad�sticas del Proyecto

- **Proyectos**: 7
- **Entidades**: 4 (Alumno, Obra, Ejemplar, Prestamo)
- **Interfaces**: 5 (IRepository + 4 espec�ficas)
- **Repositorios**: 4
- **Servicios**: 4
- **Formularios**: 5 (1 MDI + 4 SDI)
- **Stored Procedures**: 32
- **Scripts SQL**: 6
- **L�neas de c�digo**: ~3500+

---

## Soluci�n de Problemas

### Error: "Cannot open database"
**Soluci�n:**
1. Verificar que SQL Server est� ejecut�ndose
2. Verificar que la base de datos BibliotecaDB existe
3. Revisar la cadena de conexi�n o variables de entorno
4. Ver secci�n [Configuraci�n de Variables de Entorno](#-configuraci�n-de-variables-de-entorno) para m�s detalles

### Error: "A network-related or instance-specific error"
**Soluci�n:**
1. Verificar la IP y puerto del servidor
2. Verificar que SQL Server acepte conexiones remotas
3. Verificar el firewall (puerto 1433)
4. Probar conexi�n con: `ping <IP_SERVIDOR>`

### Error: "Login failed for user"
**Soluci�n:**
1. Verificar usuario y contrase�a en las variables de entorno
2. Verificar que SQL Server Authentication est� habilitado
3. Verificar permisos del usuario en la base de datos

### Error: "Could not find stored procedure"
**Soluci�n:**
1. Ejecutar todos los scripts de stored procedures en orden
2. Verificar que se ejecutaron en la base de datos BibliotecaDB
3. Usar `EXEC sp_helptext 'sp_Alumno_GetAll'` para verificar

### Error de compilaci�n
**Soluci�n:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### El formulario no abre
**Soluci�n:**
1. Verificar que se configuraron las variables de entorno o connection string
2. Verificar que la base de datos existe
3. Revisar el archivo de logs en Visual Studio (Output window)

### Datos no se cargan
**Soluci�n:**
1. Verificar que existen datos de prueba: ejecutar `SELECT * FROM Alumnos`
2. Verificar que los stored procedures existen
3. Revisar permisos del usuario de BD

---

## Uso de la Aplicaci�n

### Gestionar Alumnos
1. Men� **Gesti�n** ? **Alumnos**
2. Clic en **Nuevo** para agregar
3. Completar datos y clic en **Guardar**
4. Para modificar: seleccionar en grilla, clic en **Modificar**
5. Para eliminar (baja l�gica): seleccionar y clic en **Eliminar**

### Gestionar Obras
1. Men� **Gesti�n** ? **Obras**
2. Similar a Alumnos

### Gestionar Ejemplares
1. Men� **Gesti�n** ? **Ejemplares**
2. Clic en **Nuevo**
3. Seleccionar la **Obra** del combo
4. Ingresar **N�mero de Inventario** y **Precio**
5. Guardar

### Registrar un Pr�stamo
1. Men� **Gesti�n** ? **Pr�stamos**
2. Clic en **Nuevo**
3. Seleccionar **Alumno** del combo
4. Seleccionar **Ejemplar disponible** del combo
5. La fecha se establece autom�ticamente
6. Clic en **Guardar**
7. ? El ejemplar se marca como NO disponible
8. ? La fecha de devoluci�n se calcula (+7 d�as)

### Devolver un Pr�stamo
1. Men� **Gesti�n** ? **Pr�stamos**
2. Seleccionar el pr�stamo en la grilla
3. Clic en **Devolver**
4. Confirmar la devoluci�n
5. ? El ejemplar vuelve a estar disponible
6. ? Se registra la fecha de devoluci�n real

---

## Comandos �tiles

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

### Ver Informaci�n del Proyecto
```bash
dotnet --info
```

### Verificar Variables de Entorno
```powershell
# PowerShell
Get-ChildItem Env: | Where-Object { $_.Name -like "DB_*" }
```

---

## Entrega del TP

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
? Inyecci�n de dependencias 
? **Configuraci�n segura con variables de entorno** 
? Documentaci�n completa

### Qu� Entregar

1. **C�digo Fuente** (carpeta completa del proyecto)
2. **Scripts SQL** (carpeta `SQL/`)
3. **Este README.md** (incluye toda la documentaci�n)
4. **Archivo de soluci�n** (.sln)

### Puntos Destacados para la Presentaci�n

1. **Arquitectura en Capas** - 7 proyectos independientes
2. **Stored Procedures** - 32 SPs con transacciones
3. **ADO.NET Conectado** - Uso correcto de `SqlConnection`, `SqlCommand`, `SqlDataReader`
4. **Principios SOLID** - Ejemplos concretos en el c�digo
5. **Inyecci�n de Dependencias** - Uso de `IServiceProvider`
6. **Interfaz MDI/SDI** - **FormPrincipal** con formularios hijos
7. **Validaciones** - En capa de negocio y presentaci�n
8. **Manejo de Transacciones** - En pr�stamos y devoluciones
9. **Variables de Entorno** - Configuraci�n segura y flexible

---

## Informaci�n del Proyecto

**Asignatura**: Desarrollo y Arquitectura de Software 
**Trabajo Pr�ctico**: TP4 - Ejercicio 1 
**Tecnolog�a**: .NET 8, C# 12, Windows Forms, ADO.NET, SQL Server 
**Arquitectura**: Capas con SOLID y Clean Architecture 
**Configuraci�n**: Variables de entorno para seguridad 
**A�o**: 2025 

---

## Licencia

Este proyecto es un trabajo acad�mico desarrollado para la asignatura Desarrollo y Arquitectura de Software.

---

## Documentaci�n Adicional

- Scripts SQL documentados en la carpeta `SQL/` - ver [README de SQL](UaiDasTp4Ej1/SQL/README.md)

---

**�Sistema listo para usar y presentar!** 
