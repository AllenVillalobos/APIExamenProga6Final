namespace APIExamen.Modelos
{

    /// <summary>
    /// Representa la tabla estudiantes de la base de datos
    /// </summary>
    public class Estudiante
    {
        // Identificador único del estudiante (clave primaria)
        public int EstudianteID { get; set; }

        // Número de identificación o documento del estudiante
        public string? Identificacion { get; set; }

        // Primer nombre del estudiante
        public string? PrimerNombre { get; set; }

        // Primer apellido del estudiante
        public string? PrimerApellido { get; set; }

        // Segundo nombre del estudiante (opcional)
        public string? SegundoNombre { get; set; }

        // Segundo apellido del estudiante (opcional)
        public string? SegundoApellido { get; set; }

        // Fecha de nacimiento del estudiante. Puede ser nula si no está registrada
        public DateTime? FechaNacimiento { get; set; }

        // Edad del estudiante en años. Puede almacenarse o calcularse según la lógica de negocio
        public int? Edad { get; set; }

        // Dirección de residencia del estudiante
        public string? Direccion { get; set; }

        // Fecha y hora en que se creó el registro del estudiante
        public DateTime? FechaCreacion { get; set; }

        // Fecha y hora de la última modificación del registro del estudiante
        public DateTime? FechaModificacion { get; set; }
    }
}
