using DOM;

namespace ABS
{
    public interface IPrestamoRepository : IRepository<Prestamo>
    {
        IEnumerable<Prestamo> GetByAlumnoId(int alumnoId);
        IEnumerable<Prestamo> GetPrestamosActivos();
        IEnumerable<Prestamo> GetPrestamosVencidos();
        bool MarcarComoDevuelto(int id, DateTime fechaDevolucion);
    }
}
