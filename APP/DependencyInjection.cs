using ABS;
using CONTEXT;
using Microsoft.Extensions.DependencyInjection;
using REPO;
using SERV;

namespace APP
{
    public static class DependencyInjection
    {
        public static IServiceProvider ConfigureServices(string connectionString)
        {
            var services = new ServiceCollection();

            // DatabaseContext
            services.AddSingleton(new DatabaseContext(connectionString));

            // Repositories
            services.AddScoped<IAlumnoRepository, AlumnoRepository>();
            services.AddScoped<IObraRepository, ObraRepository>();
            services.AddScoped<IEjemplarRepository, EjemplarRepository>();
            services.AddScoped<IPrestamoRepository, PrestamoRepository>();

            // Services
            services.AddScoped<AlumnoService>();
            services.AddScoped<ObraService>();
            services.AddScoped<EjemplarService>();
            services.AddScoped<PrestamoService>();

            return services.BuildServiceProvider();
        }
    }
}
