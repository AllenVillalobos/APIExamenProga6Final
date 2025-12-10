using APIExamen.Modelos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIExamen.Service
{
    public class EstudianteService
    {
        private readonly string connectionString;

        /// <summary>
        /// Construntor de la clase, usando la cadena de conexión contenida el el archivo appsettings.json
        /// </summary>
        public EstudianteService(IConfiguration configuration)
        {
            // Guardar la cadena de conexión para uso en los métodos.
            this.connectionString = configuration.GetConnectionString("Conexion");
        }

        /// <summary>
        /// Crea un nuevo estudiante en la base de datos mediante el procedimiento almacenado "spCrearEstudiante"
        /// Devuelve el identificador generado o 0 si no se creó
        /// </summary>
        public async Task<int> CrearEstudiante(Estudiante estudiante)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spCrearEstudiante", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pIdentificacion", estudiante.Identificacion);
                    command.Parameters.AddWithValue("@pPrimerNombre", estudiante.PrimerNombre);
                    command.Parameters.AddWithValue("@pPrimerApellido", estudiante.PrimerApellido);
                    command.Parameters.AddWithValue("@pSegundoNombre", estudiante.SegundoNombre);
                    command.Parameters.AddWithValue("@pSegundoApellido", estudiante.SegundoApellido);
                    command.Parameters.AddWithValue("@pFechaNacimiento", estudiante.FechaNacimiento);
                    command.Parameters.AddWithValue("@pDireccion", estudiante.Direccion);
                    try
                    {
                        // Abrir conexión y ejecutar comando que retorna un valor
                        await connection.OpenAsync();
                        int resultado = Convert.ToInt32(await command.ExecuteScalarAsync());
                        // Si el procedimiento devuelve un ID válido, retornarlo; de lo contrario retornar 0
                        if (resultado != 0)
                        {
                            return resultado;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Lanza una excepción con contexto
                        throw new Exception("Error al crear el estudiante: " + ex.Message);
                    }
                    finally
                    {
                        // Asegurar cierre de la conexión aunque ocurra una excepción
                        await connection.CloseAsync();
                    }
                }
            }
        }

        /// <summary>
        /// Actualiza los datos de un estudiante existente mediante "spActualizarEstudiante".
        /// Devuelve el número de filas afectadas o 0 si no se actualizó.
        /// </summary>
        public async Task<int> ActualizarEstudiante(Estudiante estudiante)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spActualizarEstudiante", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //Campos a actualizar.
                    command.Parameters.AddWithValue("@pEstudianteID", estudiante.EstudianteID);
                    command.Parameters.AddWithValue("@pIdentificacion", estudiante.Identificacion);
                    command.Parameters.AddWithValue("@pPrimerNombre", estudiante.PrimerNombre);
                    command.Parameters.AddWithValue("@pPrimerApellido", estudiante.PrimerApellido);
                    command.Parameters.AddWithValue("@pSegundoNombre", estudiante.SegundoNombre);
                    command.Parameters.AddWithValue("@pSegundoApellido", estudiante.SegundoApellido);
                    command.Parameters.AddWithValue("@pFechaNacimiento", estudiante.FechaNacimiento);
                    command.Parameters.AddWithValue("@pDireccion", estudiante.Direccion);
                    try
                    {
                        // Ejecutar y convertir el resultado esperado por el procedimiento 
                        await connection.OpenAsync();
                        int resultado = Convert.ToInt32(await command.ExecuteScalarAsync());
                        if (resultado != 0)
                        {
                            return resultado;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Lanza una excepción con contexto
                        throw new Exception("Error al actualizar el estudiante: " + ex.Message);
                    }
                    finally
                    {
                        // Asegurar cierre de la conexión aunque ocurra una excepción
                        await connection.CloseAsync();
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un estudiante mediante el procedimiento almacenado "spEliminarEstudiante"
        /// Devuelve el resultado que indique el procedimiento: un número distinto de 0 si se eliminó,
        /// o 0 si no se realizó la eliminación
        /// </summary>
        public async Task<int> EliminarEstudiante(int EstudianteID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spEliminarEstudiante", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro requerido por el procedimiento almacenado
                    command.Parameters.AddWithValue("@pEstudianteID", EstudianteID);

                    try
                    {
                        // Abrir conexión y ejecutar comando que retorna un escalar
                        await connection.OpenAsync();
                        int resultado = Convert.ToInt32(await command.ExecuteScalarAsync());

                        // Si el SP devuelve un valor distinto de 0, se considera éxito
                        if (resultado != 0)
                        {
                            return resultado;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Lanza una excepción con contexto
                        throw new Exception("Error al eliminar el estudiante: " + ex.Message);
                    }
                    finally
                    {
                        // Garantiza cerrar la conexión incluso si ocurre una excepción
                        await connection.CloseAsync();
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista completa de estudiantes mediante el procedimiento almacenado "spListarEstudiantes"
        /// Devuelve una colección de objetos Estudiante con los datos leídos del SqlDataReader
        /// </summary>
        public async Task<List<Estudiante>> ListarEstudiantes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spListarEstudiantes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        // Abrir la conexión antes de ejecutar el lector
                        await connection.OpenAsync();

                        List<Estudiante> estudiantes = new List<Estudiante>();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Leer cada registro retornado por el procedimiento almacenado
                            while (await reader.ReadAsync())
                            {
                                Estudiante estudiante = new Estudiante
                                {
                                    EstudianteID = Convert.ToInt32(reader["EstudianteID"]),
                                    Identificacion = reader["Identificacion"].ToString(),
                                    PrimerNombre = reader["PrimerNombre"].ToString(),
                                    PrimerApellido = reader["PrimerApellido"].ToString(),
                                    SegundoNombre = reader["SegundoNombre"].ToString(),
                                    SegundoApellido = reader["SegundoApellido"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                                    Edad = Convert.ToInt32(reader["Edad"]),
                                    Direccion = reader["Direccion"].ToString()
                                };

                                // Agregar estudiante a la lista
                                estudiantes.Add(estudiante);
                            }

                            // Retorna la lista
                            return estudiantes;
                        }
                    }
                    catch (Exception ex)
                    {
                        // En caso de fallo se envía un mensaje
                        throw new Exception("Error al listar los estudiantes: " + ex.Message);
                    }
                    finally
                    {
                        // Cierre garantizado de la conexión
                        await connection.CloseAsync();
                    }
                }
            }
        }


        /// <summary>
        /// Busca y devuelve un estudiante específico mediante el procedimiento almacenado "spBuscarEstudiante"
        /// </summary>
        public async Task<Estudiante> BuscarEstudiante(int EstudianteID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spBuscarEstudiante", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro requerido para identificar al estudiante
                    command.Parameters.AddWithValue("@pEstudianteID", EstudianteID);

                    try
                    {
                        // Abrir conexión y ejecutar lector de datos
                        await connection.OpenAsync();
                        Estudiante estudiante = new Estudiante();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Mapear los valores obtenidos del lector en el objeto Estudiante
                                estudiante = new Estudiante
                                {
                                    EstudianteID = Convert.ToInt32(reader["EstudianteID"]),
                                    Identificacion = reader["Identificacion"].ToString(),
                                    PrimerNombre = reader["PrimerNombre"].ToString(),
                                    PrimerApellido = reader["PrimerApellido"].ToString(),
                                    SegundoNombre = reader["SegundoNombre"].ToString(),
                                    SegundoApellido = reader["SegundoApellido"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                                    Edad = Convert.ToInt32(reader["Edad"]),
                                    Direccion = reader["Direccion"].ToString()
                                };
                            }

                            // Retorna el estudiante encontrado o un objeto vacío si no existía
                            return estudiante;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Retornar error con contexto adicional
                        throw new Exception("Error al optner el estudiante: " + ex.Message);
                    }
                    finally
                    {
                        // Asegurar cierre de conexión
                        await connection.CloseAsync();
                    }
                }
            }
        }

    }
}
