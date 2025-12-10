using APIExamen.Modelos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIExamen.Service
{
    public class EstudianteService
    {
        private readonly string connectionString;
        public EstudianteService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("Conexion");
        }
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
                        throw new Exception("Error al crear el estudiante: " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }
        }
        public async Task<int> ActualizarEstudiante(Estudiante estudiante)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spActualizarEstudiante", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
                        throw new Exception("Error al actualizar el estudiante: " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }
        }

        public async Task<int> EliminarEstudiante(int EstudianteID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spEliminarEstudiante", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pEstudianteID", EstudianteID);
                    try
                    {
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
                        throw new Exception("Error al eliminar el estudiante: " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }
        }

        public async Task<List<Estudiante>> ListarEstudiantes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spListarEstudiantes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        await connection.OpenAsync();
                        List<Estudiante> estudiantes = new List<Estudiante>();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Estudiante estudiante = new Estudiante
                                {
                                    EstudianteID =Convert.ToInt32(reader["EstudianteID"]),
                                    Identificacion = reader["Identificacion"].ToString(),
                                    PrimerNombre = reader["PrimerNombre"].ToString(),
                                    PrimerApellido = reader["PrimerApellido"].ToString(),
                                    SegundoNombre = reader["SegundoNombre"].ToString(),
                                    SegundoApellido = reader["SegundoApellido"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                                    Edad =Convert.ToInt32(reader["Edad"]),
                                    Direccion = reader["Direccion"].ToString()
                                };
                                estudiantes.Add(estudiante);
                            }
                            return estudiantes;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al listar los estudiantes: " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }
        }

        public async Task<Estudiante> BuscarEstudiante(int EstudianteID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spBuscarEstudiante", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pEstudianteID", EstudianteID);
                    try
                    {
                        await connection.OpenAsync();
                        Estudiante estudiante = new Estudiante();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
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
                            return estudiante;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al optner el estudiante: " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }
        }
    }
}
