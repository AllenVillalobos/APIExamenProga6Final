using APIExamen.Modelos;
using Microsoft.EntityFrameworkCore;

namespace APIExamen.Contexto
{
    public class ContextoBaseDatos : DbContext
    {
        public ContextoBaseDatos(DbContextOptions<ContextoBaseDatos> options) : base(options)
        {

        }
        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>().HasKey(e => e.EstudianteID);
        }
    }
}
