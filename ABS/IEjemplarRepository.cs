using DOM;

namespace ABS
{
    public interface IEjemplarRepository : IRepository<Ejemplar>
    {
        IEnumerable<Ejemplar> GetByObraId(int obraId);
        IEnumerable<Ejemplar> GetDisponibles();
        Ejemplar? GetByNumeroInventario(string numeroInventario);
    }
}
