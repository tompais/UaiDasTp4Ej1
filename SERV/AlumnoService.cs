using ABS;
using DOM;

namespace SERV
{
    public class AlumnoService(IAlumnoRepository repository)
    {
        public IEnumerable<Alumno> GetAllAlumnos() =>
          repository.GetAll();

        public Alumno? GetAlumnoById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
            }

            return repository.GetById(id);
        }

        public Alumno? GetAlumnoByDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new ArgumentException("El DNI no puede estar vacío", nameof(dni));
            }

            return repository.GetByDni(dni);
        }

        public int CreateAlumno(Alumno alumno)
        {
            ValidateAlumno(alumno);

            // Verificar que no exista otro alumno con el mismo DNI
            var existente = repository.GetByDni(alumno.Dni);
            if (existente != null)
            {
                throw new InvalidOperationException($"Ya existe un alumno con el DNI {alumno.Dni}");
            }

            return repository.Add(alumno);
        }

        public bool UpdateAlumno(Alumno alumno)
        {
            ValidateAlumno(alumno);

            if (alumno.Id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(alumno));
            }

            // Verificar que no exista otro alumno con el mismo DNI
            var existente = repository.GetByDni(alumno.Dni);
            if (existente != null && existente.Id != alumno.Id)
            {
                throw new InvalidOperationException($"Ya existe otro alumno con el DNI {alumno.Dni}");
            }

            return repository.Update(alumno);
        }

        public bool DeleteAlumno(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
            }

            return repository.Delete(id);
        }

        private static void ValidateAlumno(Alumno alumno)
        {
            if (alumno == null)
            {
                throw new ArgumentNullException(nameof(alumno));
            }

            if (string.IsNullOrWhiteSpace(alumno.Nombre))
            {
                throw new ArgumentException("El nombre es requerido", nameof(alumno.Nombre));
            }

            if (string.IsNullOrWhiteSpace(alumno.Apellido))
            {
                throw new ArgumentException("El apellido es requerido", nameof(alumno.Apellido));
            }

            if (string.IsNullOrWhiteSpace(alumno.Dni))
            {
                throw new ArgumentException("El DNI es requerido", nameof(alumno.Dni));
            }

            if (string.IsNullOrWhiteSpace(alumno.Email))
            {
                throw new ArgumentException("El email es requerido", nameof(alumno.Email));
            }

            if (alumno.FechaNacimiento >= DateTime.Today)
            {
                throw new ArgumentException("La fecha de nacimiento debe ser anterior a hoy", nameof(alumno.FechaNacimiento));
            }
        }
    }
}
