using DOM;

namespace ABS
{
    public interface IObraRepository : IRepository<Obra>
    {
        Obra? GetByIsbn(string isbn);
    }
}
