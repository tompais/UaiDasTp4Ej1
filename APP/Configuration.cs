namespace APP
{
    public static class Configuration
    {
        // Cadena de conexión por defecto
        // IMPORTANTE: Modificar según tu configuración de SQL Server
        public static string ConnectionString { get; set; } = 
      "Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // También puedes usar autenticación SQL:
        // "Server=localhost;Database=BibliotecaDB;User Id=sa;Password=tuPassword;TrustServerCertificate=True;";
  }
}
