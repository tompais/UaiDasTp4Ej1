using DOM;

namespace ABS
{
    public interface IAlumnoRepository : IRepository<Alumno>
    {
        Alumno? GetByDni(string dni);
    }
}
