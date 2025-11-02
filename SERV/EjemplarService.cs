using ABS;
using DOM;

namespace SERV
{
    public class EjemplarService(IEjemplarRepository ejemplarRepository, IObraRepository obraRepository)
    {
        public IEnumerable<Ejemplar> GetAllEjemplares() =>
            ejemplarRepository.GetAll();

        public Ejemplar? GetEjemplarById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
            }

            return ejemplarRepository.GetById(id);
        }

        public IEnumerable<Ejemplar> GetEjemplaresByObraId(int obraId)
        {
            if (obraId <= 0)
            {
                throw new ArgumentException("El ID de obra debe ser mayor a cero", nameof(obraId));
            }

            return ejemplarRepository.GetByObraId(obraId);
        }

        public IEnumerable<Ejemplar> GetEjemplaresDisponibles() =>
            ejemplarRepository.GetDisponibles();

        public Ejemplar? GetEjemplarByNumeroInventario(string numeroInventario)
        {
            if (string.IsNullOrWhiteSpace(numeroInventario))
            {
                throw new ArgumentException("El número de inventario no puede estar vacío", nameof(numeroInventario));
            }

            return ejemplarRepository.GetByNumeroInventario(numeroInventario);
        }

        public int CreateEjemplar(Ejemplar ejemplar)
        {
            ValidateEjemplar(ejemplar);

            // Verificar que no exista otro ejemplar con el mismo número de inventario
            var existente = ejemplarRepository.GetByNumeroInventario(ejemplar.NumeroInventario);
            if (existente != null)
            {
                throw new InvalidOperationException($"Ya existe un ejemplar con el número de inventario {ejemplar.NumeroInventario}");
            }

            return ejemplarRepository.Add(ejemplar);
        }

        public bool UpdateEjemplar(Ejemplar ejemplar)
        {
            ValidateEjemplar(ejemplar);

            if (ejemplar.Id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(ejemplar));
            }

            // Verificar que no exista otro ejemplar con el mismo número de inventario
            var existente = ejemplarRepository.GetByNumeroInventario(ejemplar.NumeroInventario);
            if (existente != null && existente.Id != ejemplar.Id)
            {
                throw new InvalidOperationException($"Ya existe otro ejemplar con el número de inventario {ejemplar.NumeroInventario}");
            }

            return ejemplarRepository.Update(ejemplar);
        }

        public bool DeleteEjemplar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
            }

            return ejemplarRepository.Delete(id);
        }

        private void ValidateEjemplar(Ejemplar ejemplar)
        {
            if (ejemplar == null)
            {
                throw new ArgumentNullException(nameof(ejemplar));
            }

            if (ejemplar.ObraId <= 0)
            {
                throw new ArgumentException("El ID de obra debe ser mayor a cero", nameof(ejemplar));
            }

            // Verificar que la obra existe
            var obra = obraRepository.GetById(ejemplar.ObraId);
            if (obra == null)
            {
                throw new InvalidOperationException($"No existe una obra con ID {ejemplar.ObraId}");
            }

            if (string.IsNullOrWhiteSpace(ejemplar.NumeroInventario))
            {
                throw new ArgumentException("El número de inventario es requerido", nameof(ejemplar));
            }

            if (ejemplar.Precio <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor a cero", nameof(ejemplar));
            }
        }
    }
}
