using DOM;

namespace ABS
{
    public interface IPrestamoRepository : IRepository<Prestamo>
    {
        Task<IEnumerable<Prestamo>> GetByAlumnoIdAsync(int alumnoId);
        Task<IEnumerable<Prestamo>> GetPrestamosActivosAsync();
   Task<IEnumerable<Prestamo>> GetPrestamosVencidosAsync();
        Task<bool> MarcarComoDevueltoAsync(int id, DateTime fechaDevolucion);
    }
}
