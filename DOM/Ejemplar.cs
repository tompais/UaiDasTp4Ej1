namespace DOM
{
    public class Ejemplar
    {
        public int Id { get; init; }
        public int ObraId { get; set; }
        public string NumeroInventario { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool Disponible { get; set; } = true;
        public bool Activo { get; set; } = true;

        // Navegación
        public Obra? Obra { get; set; }
    }
}
