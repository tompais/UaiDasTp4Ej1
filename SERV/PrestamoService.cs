using ABS;
using DOM;

namespace SERV
{
    public class PrestamoService
    {
    private readonly IPrestamoRepository _prestamoRepository;
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly IEjemplarRepository _ejemplarRepository;

        public PrestamoService(
   IPrestamoRepository prestamoRepository,
      IAlumnoRepository alumnoRepository,
   IEjemplarRepository ejemplarRepository)
        {
   _prestamoRepository = prestamoRepository ?? throw new ArgumentNullException(nameof(prestamoRepository));
            _alumnoRepository = alumnoRepository ?? throw new ArgumentNullException(nameof(alumnoRepository));
       _ejemplarRepository = ejemplarRepository ?? throw new ArgumentNullException(nameof(ejemplarRepository));
  }

     public async Task<IEnumerable<Prestamo>> GetAllPrestamosAsync()
   {
     return await _prestamoRepository.GetAllAsync();
        }

 public async Task<Prestamo?> GetPrestamoByIdAsync(int id)
 {
     if (id <= 0)
      throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

return await _prestamoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Prestamo>> GetPrestamosByAlumnoIdAsync(int alumnoId)
        {
       if (alumnoId <= 0)
 throw new ArgumentException("El ID de alumno debe ser mayor a cero", nameof(alumnoId));

  return await _prestamoRepository.GetByAlumnoIdAsync(alumnoId);
     }

        public async Task<IEnumerable<Prestamo>> GetPrestamosActivosAsync()
        {
   return await _prestamoRepository.GetPrestamosActivosAsync();
    }

        public async Task<IEnumerable<Prestamo>> GetPrestamosVencidosAsync()
  {
       return await _prestamoRepository.GetPrestamosVencidosAsync();
        }

     public async Task<int> CreatePrestamoAsync(int alumnoId, int ejemplarId)
{
       // Validar alumno
     if (alumnoId <= 0)
      throw new ArgumentException("El ID de alumno debe ser mayor a cero", nameof(alumnoId));

            var alumno = await _alumnoRepository.GetByIdAsync(alumnoId);
 if (alumno == null)
      throw new InvalidOperationException($"No existe un alumno con ID {alumnoId}");

       if (!alumno.Activo)
    throw new InvalidOperationException("El alumno no está activo");

    // Validar ejemplar
            if (ejemplarId <= 0)
       throw new ArgumentException("El ID de ejemplar debe ser mayor a cero", nameof(ejemplarId));

 var ejemplar = await _ejemplarRepository.GetByIdAsync(ejemplarId);
         if (ejemplar == null)
           throw new InvalidOperationException($"No existe un ejemplar con ID {ejemplarId}");

 if (!ejemplar.Disponible)
     throw new InvalidOperationException("El ejemplar no está disponible para préstamo");

   if (!ejemplar.Activo)
        throw new InvalidOperationException("El ejemplar no está activo");

  // Crear préstamo
  var prestamo = new Prestamo
       {
   AlumnoId = alumnoId,
       EjemplarId = ejemplarId,
       FechaPrestamo = DateTime.Now,
       FechaDevolucionPrevista = DateTime.Now.AddDays(7),
   Devuelto = false,
       Activo = true
 };

       var prestamoId = await _prestamoRepository.AddAsync(prestamo);

   // Marcar ejemplar como no disponible
ejemplar.Disponible = false;
       await _ejemplarRepository.UpdateAsync(ejemplar);

            return prestamoId;
      }

        public async Task<bool> DevolverPrestamoAsync(int prestamoId)
   {
        if (prestamoId <= 0)
    throw new ArgumentException("El ID del préstamo debe ser mayor a cero", nameof(prestamoId));

     var prestamo = await _prestamoRepository.GetByIdAsync(prestamoId);
        if (prestamo == null)
      throw new InvalidOperationException($"No existe un préstamo con ID {prestamoId}");

       if (prestamo.Devuelto)
      throw new InvalidOperationException("El préstamo ya fue devuelto");

   // Marcar préstamo como devuelto
     var fechaDevolucion = DateTime.Now;
       var resultado = await _prestamoRepository.MarcarComoDevueltoAsync(prestamoId, fechaDevolucion);

       if (resultado)
     {
    // Marcar ejemplar como disponible
    var ejemplar = await _ejemplarRepository.GetByIdAsync(prestamo.EjemplarId);
    if (ejemplar != null)
           {
ejemplar.Disponible = true;
        await _ejemplarRepository.UpdateAsync(ejemplar);
         }
            }

        return resultado;
        }

public async Task<bool> DeletePrestamoAsync(int id)
 {
  if (id <= 0)
     throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

    return await _prestamoRepository.DeleteAsync(id);
}
    }
}
