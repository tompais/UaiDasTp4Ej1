using DOM;

namespace ABS
{
  public interface IAlumnoRepository : IRepository<Alumno>
    {
        Task<Alumno?> GetByDniAsync(string dni);
    }
}
