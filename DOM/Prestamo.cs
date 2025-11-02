namespace DOM
{
    public class Prestamo
    {
        public int Id { get; init; }
        public int AlumnoId { get; set; }
        public int EjemplarId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionPrevista { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public bool Devuelto { get; set; } = false;
        public bool Activo { get; set; } = true;

        // Navegación
        public Alumno? Alumno { get; set; }
        public Ejemplar? Ejemplar { get; set; }
    }
}
