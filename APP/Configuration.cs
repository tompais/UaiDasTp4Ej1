namespace APP
{
    public static class Configuration
    {
        // Cadena de conexión configurada desde variables de entorno o valor por defecto
        public static string ConnectionString { get; set; }

        static Configuration()
        {
            // Intentar construir la connection string desde variables de entorno
            var server = Environment.GetEnvironmentVariable("DB_SERVER");
            var database = Environment.GetEnvironmentVariable("DB_DATABASE");
            var userId = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var useIntegratedSecurity = Environment.GetEnvironmentVariable("DB_USE_INTEGRATED_SECURITY");

            // Si todas las variables están configuradas, construir la connection string
            if (!string.IsNullOrWhiteSpace(server) && !string.IsNullOrWhiteSpace(database))
            {
                if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(password))
                {
                    // SQL Server Authentication
                    ConnectionString = $"Server={server};Database={database};User Id={userId};Password={password};TrustServerCertificate=True;";
                }
                else if (!string.IsNullOrWhiteSpace(useIntegratedSecurity) &&
                         bool.TryParse(useIntegratedSecurity, out bool useIntegrated) && useIntegrated)
                {
                    // Windows Authentication
                    ConnectionString = $"Server={server};Database={database};Trusted_Connection=True;TrustServerCertificate=True;";
                }
                else
                {
                    // Valor por defecto si no se especifica el tipo de autenticación
                    ConnectionString = GetDefaultConnectionString();
                }
            }
            else
            {
                // Si no hay variables de entorno configuradas, usar valor por defecto
                ConnectionString = GetDefaultConnectionString();
            }
        }

        private static string GetDefaultConnectionString() =>
            "Server=localhost;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Permite establecer manualmente la connection string si es necesario
        /// </summary>
        public static void SetConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("La connection string no puede estar vacía", nameof(connectionString));
            }

            ConnectionString = connectionString;
        }
    }
}
