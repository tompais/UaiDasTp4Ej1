using ABS;
using DOM;

namespace SERV
{
    public class AlumnoService
    {
        private readonly IAlumnoRepository _repository;

        public AlumnoService(IAlumnoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Alumno>> GetAllAlumnosAsync()
        {
   return await _repository.GetAllAsync();
    }

        public async Task<Alumno?> GetAlumnoByIdAsync(int id)
        {
         if (id <= 0)
  throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

  return await _repository.GetByIdAsync(id);
     }

        public async Task<Alumno?> GetAlumnoByDniAsync(string dni)
        {
  if (string.IsNullOrWhiteSpace(dni))
   throw new ArgumentException("El DNI no puede estar vacío", nameof(dni));

            return await _repository.GetByDniAsync(dni);
        }

        public async Task<int> CreateAlumnoAsync(Alumno alumno)
     {
  ValidateAlumno(alumno);

     // Verificar que no exista otro alumno con el mismo DNI
            var existente = await _repository.GetByDniAsync(alumno.Dni);
            if (existente != null)
    throw new InvalidOperationException($"Ya existe un alumno con el DNI {alumno.Dni}");

    return await _repository.AddAsync(alumno);
        }

        public async Task<bool> UpdateAlumnoAsync(Alumno alumno)
        {
    ValidateAlumno(alumno);

            if (alumno.Id <= 0)
   throw new ArgumentException("El ID debe ser mayor a cero", nameof(alumno.Id));

  // Verificar que no exista otro alumno con el mismo DNI
     var existente = await _repository.GetByDniAsync(alumno.Dni);
       if (existente != null && existente.Id != alumno.Id)
       throw new InvalidOperationException($"Ya existe otro alumno con el DNI {alumno.Dni}");

     return await _repository.UpdateAsync(alumno);
    }

        public async Task<bool> DeleteAlumnoAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

            return await _repository.DeleteAsync(id);
 }

        private static void ValidateAlumno(Alumno alumno)
        {
   if (alumno == null)
    throw new ArgumentNullException(nameof(alumno));

 if (string.IsNullOrWhiteSpace(alumno.Nombre))
           throw new ArgumentException("El nombre es requerido", nameof(alumno.Nombre));

      if (string.IsNullOrWhiteSpace(alumno.Apellido))
      throw new ArgumentException("El apellido es requerido", nameof(alumno.Apellido));

    if (string.IsNullOrWhiteSpace(alumno.Dni))
throw new ArgumentException("El DNI es requerido", nameof(alumno.Dni));

            if (string.IsNullOrWhiteSpace(alumno.Email))
         throw new ArgumentException("El email es requerido", nameof(alumno.Email));

            if (alumno.FechaNacimiento >= DateTime.Today)
      throw new ArgumentException("La fecha de nacimiento debe ser anterior a hoy", nameof(alumno.FechaNacimiento));
        }
    }
}
