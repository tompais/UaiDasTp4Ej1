namespace DOM
{
    public class Obra
    {
        public int Id { get; init; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Editorial { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public string Genero { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
