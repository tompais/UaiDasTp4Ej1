using ABS;
using DOM;

namespace SERV
{
    public class ObraService
    {
        private readonly IObraRepository _repository;

        public ObraService(IObraRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
 }

        public async Task<IEnumerable<Obra>> GetAllObrasAsync()
   {
 return await _repository.GetAllAsync();
}

        public async Task<Obra?> GetObraByIdAsync(int id)
        {
     if (id <= 0)
    throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

     return await _repository.GetByIdAsync(id);
 }

        public async Task<Obra?> GetObraByIsbnAsync(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("El ISBN no puede estar vacío", nameof(isbn));

       return await _repository.GetByIsbnAsync(isbn);
     }

        public async Task<int> CreateObraAsync(Obra obra)
        {
      ValidateObra(obra);

            // Verificar que no exista otra obra con el mismo ISBN
     var existente = await _repository.GetByIsbnAsync(obra.Isbn);
       if (existente != null)
    throw new InvalidOperationException($"Ya existe una obra con el ISBN {obra.Isbn}");

         return await _repository.AddAsync(obra);
 }

        public async Task<bool> UpdateObraAsync(Obra obra)
        {
            ValidateObra(obra);

     if (obra.Id <= 0)
  throw new ArgumentException("El ID debe ser mayor a cero", nameof(obra.Id));

        // Verificar que no exista otra obra con el mismo ISBN
    var existente = await _repository.GetByIsbnAsync(obra.Isbn);
            if (existente != null && existente.Id != obra.Id)
         throw new InvalidOperationException($"Ya existe otra obra con el ISBN {obra.Isbn}");

            return await _repository.UpdateAsync(obra);
  }

        public async Task<bool> DeleteObraAsync(int id)
     {
  if (id <= 0)
    throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));

     return await _repository.DeleteAsync(id);
        }

    private static void ValidateObra(Obra obra)
  {
       if (obra == null)
           throw new ArgumentNullException(nameof(obra));

            if (string.IsNullOrWhiteSpace(obra.Titulo))
    throw new ArgumentException("El título es requerido", nameof(obra.Titulo));

            if (string.IsNullOrWhiteSpace(obra.Autor))
    throw new ArgumentException("El autor es requerido", nameof(obra.Autor));

            if (string.IsNullOrWhiteSpace(obra.Isbn))
                throw new ArgumentException("El ISBN es requerido", nameof(obra.Isbn));

          if (obra.AnioPublicacion < 1000 || obra.AnioPublicacion > DateTime.Now.Year)
      throw new ArgumentException("El año de publicación no es válido", nameof(obra.AnioPublicacion));
        }
    }
}
