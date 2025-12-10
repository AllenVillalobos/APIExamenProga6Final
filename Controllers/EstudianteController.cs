using APIExamen.Modelos;
using APIExamen.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIExamen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : Controller
    {
        private readonly EstudianteService estudianteService;
        /// <summary>
        /// Constructro de la clase 
        /// Se Inyecta el servicio de estudiantes para lógica y acceder a los datos
        /// </summary>
        public EstudianteController(EstudianteService estudianteService)
        {
            this.estudianteService = estudianteService;
        }

        /// <summary>
        /// Obtiene la lista completa de estudiantes mediante el servicio
        /// </summary>
        [HttpGet("ListarEstudiantes")]
        public async Task<ActionResult<List<Estudiante>>> ListarEstudiantes()
        {
            try
            {
                // Usa al servicio para obtener todos los estudiantes
                List<Estudiante> estudiantes = await estudianteService.ListarEstudiantes();

                // Si existen registros se retornan
                if (estudiantes.Count > 0)
                {
                    return Ok(estudiantes);
                }
                else
                {
                    // No se encontraron estudiantes
                    return NotFound("No se encontraron estudiantes");
                }
            }
            catch (Exception ex)
            {
                // Error inesperado
                return BadRequest("Error en el sistema, a la hora de obtener los estudiantes: " + ex.Message);
            }
        }

        /// <summary>
        /// Busca un estudiante según el ID indicado en la ruta
        /// </summary>
        [HttpGet("BuscarEstudiantes/{id}")]
        public async Task<ActionResult<Estudiante>> BuscarEstudiantes(int id)
        {
            try
            {
                // Se utiliza el service para buscar el estudiante
                Estudiante estudiante = await estudianteService.BuscarEstudiante(id);

                // Si el ID es distinto de 0, significa que existe en la BD
                if (estudiante.EstudianteID != 0)
                {
                    return Ok(estudiante);
                }
                else
                {
                    return NotFound("No se encontró al estudiante");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error en el sistema, a la hora de obtener el estudiante: " + ex.Message);
            }
        }

        /// <summary>
        /// Crea un nuevo estudiante a partir del JSON recibido en el cuerpo de la solicitud
        /// </summary>
        [HttpPost("CrearEstudiante")]
        public async Task<ActionResult<int>> CrearEstudiante([FromBody] Estudiante estudiante)
        {
            try
            {
                // Es utiliza el servicio para crear el estudiante
                int resultado = await estudianteService.CrearEstudiante(estudiante);

                // Si se creó el SP retorna el ID generado
                if (resultado != 0)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound("No se pudo crear el estudiante");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error en el sistema, a la hora de crear el estudiante: " + ex.Message);
            }
        }

        /// <summary>
        /// Modifica un estudiante existente comprobando que el ID de la ruta coincida 
        /// con el enviado en el cuerpo de la solicitud
        /// </summary>
        [HttpPut("ModificarEstudiante/{id}")]
        public async Task<ActionResult<int>> CrearEstudiante([FromBody] Estudiante estudiante, int id)
        {
            // Primero validamos que los IDs coincidan
            if (estudiante.EstudianteID != id)
            {
                return BadRequest("Los ID seleccionados no coinciden entre sí");
            }

            try
            {
                // Solicitud al servicio para actualizar el estudiante
                int resultado = await estudianteService.ActualizarEstudiante(estudiante);

                if (resultado != 0)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound("No se pudo actualizar el estudiante");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error en el sistema, a la hora de actualizar el estudiante: " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina un estudiante según el ID enviado en la ruta
        /// </summary>
        [HttpDelete("EliminarEstudiante/{id}")]
        public async Task<ActionResult<int>> EliminarEstudiante(int id)
        {
            try
            {
                // Solicitud al servicio para eliminar el estudiante
                int resultado = await estudianteService.EliminarEstudiante(id);

                if (resultado != 0)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound("No se pudo eliminar al estudiante");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error en el sistema, a la hora de eliminar al estudiante: " + ex.Message);
            }
        }
    }
}
