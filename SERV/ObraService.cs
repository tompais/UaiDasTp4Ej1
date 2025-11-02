using ABS;
using DOM;

namespace SERV
{
    public class ObraService(IObraRepository repository)
    {
        public IEnumerable<Obra> GetAllObras() =>
  repository.GetAll();

        public Obra? GetObraById(int id)
 {
     if (id <= 0)
       {
 throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
      }

return repository.GetById(id);
        }

   public Obra? GetObraByIsbn(string isbn)
        {
   if (string.IsNullOrWhiteSpace(isbn))
      {
      throw new ArgumentException("El ISBN no puede estar vacío", nameof(isbn));
     }

          return repository.GetByIsbn(isbn);
        }

        public int CreateObra(Obra obra)
  {
    ValidateObra(obra);

     // Verificar que no exista otra obra con el mismo ISBN
     var existente = repository.GetByIsbn(obra.Isbn);
     if (existente != null)
       {
   throw new InvalidOperationException($"Ya existe una obra con el ISBN {obra.Isbn}");
     }

  return repository.Add(obra);
        }

    public bool UpdateObra(Obra obra)
        {
      ValidateObra(obra);

        if (obra.Id <= 0)
      {
    throw new ArgumentException("El ID debe ser mayor a cero", nameof(obra));
       }

 // Verificar que no exista otra obra con el mismo ISBN
  var existente = repository.GetByIsbn(obra.Isbn);
if (existente != null && existente.Id != obra.Id)
       {
    throw new InvalidOperationException($"Ya existe otra obra con el ISBN {obra.Isbn}");
  }

      return repository.Update(obra);
 }

 public bool DeleteObra(int id)
 {
            if (id <= 0)
        {
       throw new ArgumentException("El ID debe ser mayor a cero", nameof(id));
     }

      return repository.Delete(id);
 }

   private static void ValidateObra(Obra obra)
    {
    if (obra == null)
      {
  throw new ArgumentNullException(nameof(obra));
  }

       if (string.IsNullOrWhiteSpace(obra.Titulo))
{
        throw new ArgumentException("El título es requerido", nameof(obra));
     }

            if (string.IsNullOrWhiteSpace(obra.Autor))
       {
             throw new ArgumentException("El autor es requerido", nameof(obra));
            }

     if (string.IsNullOrWhiteSpace(obra.Isbn))
      {
    throw new ArgumentException("El ISBN es requerido", nameof(obra));
     }

    if (obra.AnioPublicacion < 1000 || obra.AnioPublicacion > DateTime.Now.Year)
{
        throw new ArgumentException("El año de publicación no es válido", nameof(obra));
  }
        }
    }
}
