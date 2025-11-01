using DOM;

namespace ABS
{
    public interface IEjemplarRepository : IRepository<Ejemplar>
    {
   Task<IEnumerable<Ejemplar>> GetByObraIdAsync(int obraId);
        Task<IEnumerable<Ejemplar>> GetDisponiblesAsync();
        Task<Ejemplar?> GetByNumeroInventarioAsync(string numeroInventario);
    }
}
