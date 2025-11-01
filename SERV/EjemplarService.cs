using ABS;
using DOM;

namespace SERV
{
    public class EjemplarService
    {
        private readonly IEjemplarRepository _ejemplarRepository;
        private readonly IObraRepository _obraRepository;

        public EjemplarService(IEjemplarRepository ejemplarRepository, IObraRepository obraRepository)
        {
    _ejemplarRepository = ejemplarRepository ?? throw new ArgumentNullException(nameof(ejemplarRepository));
         _obraRepository = obraRepository ?? throw new ArgumentNullException(nameof(obraRepository));
        }

        public async Task<IEnumerable<Ejemplar>> GetAllEjemplaresAsync()
        {
    return await _ejemplarRepository.GetAllAsync();
        }

        public async Task<Ejemplar?> GetEjemplarByIdAsync(int id)
        {
         if (id <= 0)
       throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

    return await _ejemplarRepository.GetByIdAsync(id);
   }

        public async Task<IEnumerable<Ejemplar>> GetEjemplaresByObraIdAsync(int obraId)
        {
if (obraId <= 0)
     throw new ArgumentException("El ID de obra debe ser mayor a cero", nameof(obraId));

      return await _ejemplarRepository.GetByObraIdAsync(obraId);
}

        public async Task<IEnumerable<Ejemplar>> GetEjemplaresDisponiblesAsync()
   {
       return await _ejemplarRepository.GetDisponiblesAsync();
     }

        public async Task<Ejemplar?> GetEjemplarByNumeroInventarioAsync(string numeroInventario)
        {
if (string.IsNullOrWhiteSpace(numeroInventario))
          throw new ArgumentException("El número de inventario no puede estar vacío", nameof(numeroInventario));

   return await _ejemplarRepository.GetByNumeroInventarioAsync(numeroInventario);
 }

        public async Task<int> CreateEjemplarAsync(Ejemplar ejemplar)
   {
   await ValidateEjemplarAsync(ejemplar);

      // Verificar que no exista otro ejemplar con el mismo número de inventario
       var existente = await _ejemplarRepository.GetByNumeroInventarioAsync(ejemplar.NumeroInventario);
    if (existente != null)
  throw new InvalidOperationException($"Ya existe un ejemplar con el número de inventario {ejemplar.NumeroInventario}");

     return await _ejemplarRepository.AddAsync(ejemplar);
  }

public async Task<bool> UpdateEjemplarAsync(Ejemplar ejemplar)
        {
      await ValidateEjemplarAsync(ejemplar);

  if (ejemplar.Id <= 0)
       throw new ArgumentException("El ID debe ser mayor a cero", nameof(ejemplar.Id));

      // Verificar que no exista otro ejemplar con el mismo número de inventario
       var existente = await _ejemplarRepository.GetByNumeroInventarioAsync(ejemplar.NumeroInventario);
   if (existente != null && existente.Id != ejemplar.Id)
throw new InvalidOperationException($"Ya existe otro ejemplar con el número de inventario {ejemplar.NumeroInventario}");

      return await _ejemplarRepository.UpdateAsync(ejemplar);
    }

     public async Task<bool> DeleteEjemplarAsync(int id)
        {
      if (id <= 0)
       throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

  return await _ejemplarRepository.DeleteAsync(id);
      }

        private async Task ValidateEjemplarAsync(Ejemplar ejemplar)
        {
       if (ejemplar == null)
       throw new ArgumentNullException(nameof(ejemplar));

          if (ejemplar.ObraId <= 0)
   throw new ArgumentException("El ID de obra debe ser mayor a cero", nameof(ejemplar.ObraId));

       // Verificar que la obra existe
            var obra = await _obraRepository.GetByIdAsync(ejemplar.ObraId);
       if (obra == null)
    throw new InvalidOperationException($"No existe una obra con ID {ejemplar.ObraId}");

       if (string.IsNullOrWhiteSpace(ejemplar.NumeroInventario))
           throw new ArgumentException("El número de inventario es requerido", nameof(ejemplar.NumeroInventario));

            if (ejemplar.Precio <= 0)
   throw new ArgumentException("El precio debe ser mayor a cero", nameof(ejemplar.Precio));
   }
    }
}
