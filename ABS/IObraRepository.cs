using DOM;

namespace ABS
{
    public interface IObraRepository : IRepository<Obra>
    {
        Task<Obra?> GetByIsbnAsync(string isbn);
    }
}
