namespace APIExamen.Modelos
{
    public class Estudiante
    {
        public int EstudianteID { get; set; }

        public string? Identificacion { get; set; }

        public string? PrimerNombre { get; set; }

        public string? PrimerApellido { get; set; }

        public string? SegundoNombre { get; set; }

        public string? SegundoApellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public int? Edad { get; set; }

        public string? Direccion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
