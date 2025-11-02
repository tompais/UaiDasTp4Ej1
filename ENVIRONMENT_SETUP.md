# ?? Configuración de Variables de Entorno

## Descripción

El sistema utiliza **variables de entorno** para configurar la cadena de conexión a la base de datos. Esto permite mayor flexibilidad y seguridad, especialmente en diferentes entornos (desarrollo, pruebas, producción).

---

## ?? Variables de Entorno Disponibles

### Para SQL Server Authentication (Usuario y Contraseña):

| Variable | Descripción | Ejemplo |
|----------|-------------|---------|
| `DB_SERVER` | Dirección IP o nombre del servidor SQL Server con puerto (opcional) | `192.168.1.100` o `192.168.1.100,1433` |
| `DB_DATABASE` | Nombre de la base de datos | `BibliotecaDB` |
| `DB_USER` | Usuario de SQL Server | `sa` o `BibliotecaUser` |
| `DB_PASSWORD` | Contraseña del usuario | `MiPassword123!` |

### Para Windows Authentication (Integrated Security):

| Variable | Descripción | Ejemplo |
|----------|-------------|---------|
| `DB_SERVER` | Dirección IP o nombre del servidor SQL Server | `localhost` o `192.168.1.100` |
| `DB_DATABASE` | Nombre de la base de datos | `BibliotecaDB` |
| `DB_USE_INTEGRATED_SECURITY` | Usar autenticación de Windows | `true` |

---

## ?? Cómo Configurar las Variables de Entorno

### **Opción 1: Variables de Sistema (Windows) - Recomendado para desarrollo**

#### Configuración Permanente:

1. Presionar `Win + R` y escribir: `sysdm.cpl`
2. Ir a la pestaña **"Opciones avanzadas"**
3. Clic en **"Variables de entorno"**
4. En **"Variables del sistema"** (o "Variables de usuario"), clic en **"Nueva"**
5. Agregar cada variable:

**Para SQL Server Authentication:**
```
Nombre: DB_SERVER
Valor: 192.168.1.100,1433

Nombre: DB_DATABASE
Valor: BibliotecaDB

Nombre: DB_USER
Valor: sa

Nombre: DB_PASSWORD
Valor: TuPasswordSeguro123!
```

6. Clic en **Aceptar** y **reiniciar Visual Studio** o el IDE

---

### **Opción 2: PowerShell (Temporal - solo para la sesión actual)**

#### SQL Server Authentication:

```powershell
# Configurar variables de entorno temporalmente
$env:DB_SERVER = "192.168.1.100,1433"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USER = "sa"
$env:DB_PASSWORD = "TuPasswordSeguro123!"

# Ejecutar la aplicación desde PowerShell
dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

#### Windows Authentication:

```powershell
$env:DB_SERVER = "localhost"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"

dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

---

### **Opción 3: CMD/Símbolo del Sistema (Temporal)**

#### SQL Server Authentication:

```cmd
set DB_SERVER=192.168.1.100,1433
set DB_DATABASE=BibliotecaDB
set DB_USER=sa
set DB_PASSWORD=TuPasswordSeguro123!

dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

---

### **Opción 4: Archivo launchSettings.json (Visual Studio)**

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

---

### **Opción 5: Archivo .env (Desarrollo Local) - NO INCLUIR EN GIT**

Crear un archivo `.env` en la raíz del proyecto:

```env
DB_SERVER=192.168.1.100,1433
DB_DATABASE=BibliotecaDB
DB_USER=sa
DB_PASSWORD=TuPasswordSeguro123!
```

**?? IMPORTANTE:** Agregar `.env` al archivo `.gitignore` para no subir contraseñas al repositorio:

```gitignore
# Variables de entorno
.env
*.env
```

Luego, cargar las variables antes de ejecutar (usando PowerShell):

```powershell
Get-Content .env | ForEach-Object {
    if ($_ -match '^([^=]+)=(.*)$') {
      [Environment]::SetEnvironmentVariable($matches[1], $matches[2])
    }
}

dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

---

## ?? Verificar Variables de Entorno

### PowerShell:
```powershell
# Ver todas las variables
Get-ChildItem Env: | Where-Object { $_.Name -like "DB_*" }

# Ver una variable específica
$env:DB_SERVER
$env:DB_DATABASE
```

### CMD:
```cmd
set DB_SERVER
set DB_DATABASE
```

---

## ?? Ejemplos de Configuración

### Ejemplo 1: SQL Server Local con SQL Authentication

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

---

### Ejemplo 2: SQL Server Remoto con Puerto Específico

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

---

### Ejemplo 3: SQL Server con Windows Authentication

```powershell
$env:DB_SERVER = "DESKTOP-PC\SQLEXPRESS"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"
```

**Connection String resultante:**
```
Server=DESKTOP-PC\SQLEXPRESS;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;
```

---

### Ejemplo 4: LocalDB

```powershell
$env:DB_SERVER = "(localdb)\mssqllocaldb"
$env:DB_DATABASE = "BibliotecaDB"
$env:DB_USE_INTEGRATED_SECURITY = "true"
```

**Connection String resultante:**
```
Server=(localdb)\mssqllocaldb;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;
```

---

## ?? Valor Por Defecto

Si **NO** se configuran las variables de entorno, la aplicación usará automáticamente:

```
Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;
```

Esto es útil para desarrollo rápido sin configuración adicional.

---

## ?? Mejores Prácticas de Seguridad

### ? **Hacer:**
- Usar variables de entorno para datos sensibles (contraseñas, IPs)
- Agregar `.env` al `.gitignore`
- Usar variables de sistema en entornos de producción
- Documentar qué variables son necesarias
- Usar contraseñas fuertes

### ? **NO Hacer:**
- Nunca subir contraseñas al repositorio Git
- No compartir archivos `.env` con contraseñas reales
- No hardcodear contraseñas en el código
- No dejar contraseñas por defecto en producción

---

## ?? Probar la Configuración

Ejecutar desde la terminal (después de configurar las variables):

```bash
dotnet run --project UaiDasTp4Ej1/UaiDasTp4Ej1.csproj
```

Si las variables están bien configuradas:
- ? La aplicación se conectará a tu servidor SQL
- ? El formulario principal se abrirá correctamente
- ? Podrás ver los datos en las grillas

Si hay un error:
- ? Verificar que SQL Server esté en ejecución
- ? Verificar que la IP/puerto sean correctos
- ? Verificar usuario y contraseña
- ? Verificar que la base de datos BibliotecaDB existe

---

## ?? Troubleshooting

### Error: "A network-related or instance-specific error occurred"

**Solución:**
1. Verificar que SQL Server esté ejecutándose
2. Verificar la IP y el puerto: `ping 192.168.1.100`
3. Verificar que el puerto 1433 esté abierto en el firewall
4. Verificar que SQL Server acepte conexiones remotas

### Error: "Login failed for user"

**Solución:**
1. Verificar el usuario y contraseña
2. Asegurarse de que el usuario tenga permisos en la base de datos
3. Verificar que SQL Server Authentication esté habilitado

### Error: "Cannot open database"

**Solución:**
1. Verificar que la base de datos BibliotecaDB exista
2. Ejecutar los scripts SQL para crear la BD
3. Verificar permisos del usuario en la base de datos

---

## ?? Para el Profesor / Evaluación

Si necesitas evaluar el proyecto con una configuración específica:

1. Configurar las variables de entorno antes de abrir Visual Studio
2. O usar el método de `launchSettings.json`
3. La aplicación se conectará automáticamente con tus credenciales

**No es necesario modificar el código fuente** para cambiar la conexión. ??

---

## ?? Referencias

- [Variables de Entorno en Windows](https://docs.microsoft.com/windows/deployment/usmt/usmt-recognized-environment-variables)
- [Connection Strings en .NET](https://docs.microsoft.com/dotnet/framework/data/adonet/connection-strings)
- [SQL Server Authentication](https://docs.microsoft.com/sql/relational-databases/security/choose-an-authentication-mode)

---

**¡Configuración completada!** ????
