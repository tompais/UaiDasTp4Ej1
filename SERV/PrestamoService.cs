using ABS;
using DOM;

namespace SERV
{
    public class PrestamoService(
   IPrestamoRepository prestamoRepository,
  IAlumnoRepository alumnoRepository,
   IEjemplarRepository ejemplarRepository)
    {
        public IEnumerable<Prestamo> GetAllPrestamos() =>
  prestamoRepository.GetAll();

  public Prestamo? GetPrestamoById(int id)
  {
            if (id <= 0)
          {
         throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
       }

            return prestamoRepository.GetById(id);
        }

        public IEnumerable<Prestamo> GetPrestamosByAlumnoId(int alumnoId)
        {
 if (alumnoId <= 0)
            {
             throw new ArgumentException("El ID de alumno debe ser mayor a cero", nameof(alumnoId));
   }

     return prestamoRepository.GetByAlumnoId(alumnoId);
   }

        public IEnumerable<Prestamo> GetPrestamosActivos() =>
     prestamoRepository.GetPrestamosActivos();

        public IEnumerable<Prestamo> GetPrestamosVencidos() =>
      prestamoRepository.GetPrestamosVencidos();

public int CreatePrestamo(int alumnoId, int ejemplarId)
    {
       // Validar alumno
            if (alumnoId <= 0)
      {
  throw new ArgumentException("El ID de alumno debe ser mayor a cero", nameof(alumnoId));
            }

        var alumno = alumnoRepository.GetById(alumnoId) ?? throw new InvalidOperationException($"No existe un alumno con ID {alumnoId}");
       if (!alumno.Activo)
 {
throw new InvalidOperationException("El alumno no está activo");
      }

        // Validar ejemplar
          if (ejemplarId <= 0)
            {
 throw new ArgumentException("El ID de ejemplar debe ser mayor a cero", nameof(ejemplarId));
     }

 var ejemplar = ejemplarRepository.GetById(ejemplarId) ?? throw new InvalidOperationException($"No existe un ejemplar con ID {ejemplarId}");
         if (!ejemplar.Disponible)
     {
           throw new InvalidOperationException("El ejemplar no está disponible para préstamo");
       }

            if (!ejemplar.Activo)
    {
   throw new InvalidOperationException("El ejemplar no está activo");
}

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

            var prestamoId = prestamoRepository.Add(prestamo);

   // Marcar ejemplar como no disponible
ejemplar.Disponible = false;
            ejemplarRepository.Update(ejemplar);

      return prestamoId;
        }

public bool DevolverPrestamo(int prestamoId)
     {
            if (prestamoId <= 0)
      {
           throw new ArgumentException("El ID del préstamo debe ser mayor a cero", nameof(prestamoId));
  }

            var prestamo = prestamoRepository.GetById(prestamoId) ?? throw new InvalidOperationException($"No existe un préstamo con ID {prestamoId}");
            if (prestamo.Devuelto)
            {
     throw new InvalidOperationException("El préstamo ya fue devuelto");
    }

            // Marcar préstamo como devuelto
       var fechaDevolucion = DateTime.Now;
         var resultado = prestamoRepository.MarcarComoDevuelto(prestamoId, fechaDevolucion);

            if (resultado)
 {
        // Marcar ejemplar como disponible
    var ejemplar = ejemplarRepository.GetById(prestamo.EjemplarId);
           if (ejemplar != null)
  {
               ejemplar.Disponible = true;
        ejemplarRepository.Update(ejemplar);
                }
            }

            return resultado;
     }

        public bool DeletePrestamo(int id)
 {
            if (id <= 0)
     {
         throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
  }

 return prestamoRepository.Delete(id);
        }
    }
}
