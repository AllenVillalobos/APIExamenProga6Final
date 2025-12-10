using APIExamen.Modelos;
using Microsoft.EntityFrameworkCore;

namespace APIExamen.Contexto
{
    public class ContextoBaseDatos : DbContext
    {
        /// <summary>
        /// Constructor del contexto que recibe las opciones configuradas en Program.cs
        /// (cadena de conexión)
        /// </summary>
        public ContextoBaseDatos(DbContextOptions<ContextoBaseDatos> options) : base(options)
        {

        }

        /// <summary>
        /// Representa la tabla "Estudiantes"
        /// </summary>
        public DbSet<Estudiante> Estudiantes { get; set; }


        /// <summary>
        /// Se define la clave primaria 
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Se define la clave primaria de la entidad Estudiante
            modelBuilder.Entity<Estudiante>().HasKey(e => e.EstudianteID);
        }
    }
}
