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
        public EstudianteController(EstudianteService estudianteService)
        {
            this.estudianteService = estudianteService;
        }
        [HttpGet("ListarEstudiantes")]
        public async Task<ActionResult<List<Estudiante>>> ListarEstudiantes()
        {
            try
            {
                List<Estudiante> estudiantes = await estudianteService.ListarEstudiantes();
                if (estudiantes.Count > 0)
                {
                    return Ok(estudiantes);
                }
                else
                {
                    return NotFound("No se encontraron estudiantes");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error en el sistema, a la hora de optner los estudiantes: "+ex.Message);
            }
        }

        [HttpGet("BuscarEstudiantes/{id}")]
        public async Task<ActionResult<Estudiante>> BuscarEstudiantes(int id)
        {
            try
            {
                Estudiante estudiante = await estudianteService.BuscarEstudiante(id);
                if (estudiante.EstudianteID != 0)
                {
                    return Ok(estudiante);
                }
                else
                {
                    return NotFound("No se encontro al estudiante");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error en el sistema, a la hora de optner el estudiante: " + ex.Message);
            }
        }

        [HttpPost("CrearEstudiante")]
        public async Task<ActionResult<int>> CrearEstudiante([FromBody] Estudiante estudiante)
        {
            try
            {
                int resultado = await estudianteService.CrearEstudiante(estudiante);
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
        [HttpPut("ModificarEstudiante/{id}")]
        public async Task<ActionResult<int>> CrearEstudiante([FromBody] Estudiante estudiante, int id)
        {
            if (estudiante.EstudianteID != id)
            {
                return BadRequest("Los ID seleccionados no coinciden entre sí");
            }
            try
            {
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

        [HttpDelete("EliminarEstudiante/{id}")]
        public async Task<ActionResult<int>> EliminarEstudiante(int id)
        {
            try
            {
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
