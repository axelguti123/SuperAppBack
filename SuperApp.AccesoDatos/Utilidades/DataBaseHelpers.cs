using SuperApp.AccesoDatos.Conexion;
using SuperApp.AccesoDatos.Excepciones;
using SupperApp.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using Enums;

namespace SuperApp.AccesoDatos.Utilidades
{
    internal static class DataBaseHelpers
    {
        private static readonly Dictionary<Type,string> _excepcion = new()
        {
            {typeof(UsuarioNoEncontradoException),"Error. U suario no encontrado" },
            {typeof(EspecialidadNoEncontradaException),"Error. Especialidad no encontrada" }
        };
        private static string GetExcepcionMessage(Exception ex)
        {
            return _excepcion.TryGetValue(ex.GetType(), out var message) ? message :"Error desconocido: "+ ex.Message;
        }
        public static async Task<Response> ExecuteNonQueryAsync(string storedProcedure, Action<SqlCommand> action, Func<int, Response> handleReturnValue = null)
        {
            var response = new Response();
            using var connection = CadenaConexion.ObtenerConexion();

            try
            {
                await connection.OpenAsync().ConfigureAwait(false);
                using var cmd = new SqlCommand(storedProcedure, connection) { CommandType = CommandType.StoredProcedure };
                action?.Invoke(cmd);
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

                response.Status = Status.Success.ToString();
                response.Message = "Operacion Realizada con Exito. ";
            }catch(SqlException ex)
            {
                response.Status = Status.Error.ToString();
                response.Message = GetExcepcionMessage(ex);
            }
            catch (Exception ex)
            {
                response.Status = Status.Error.ToString();
                response.Message =GetExcepcionMessage(ex);
            }
            finally
            {
                await connection.CloseAsync().ConfigureAwait(false);
            }
            return response;
        }

        public static async Task<Response<TEntity>> ExecuteReaderAsync<TEntity>(string storedProcedure, Action<SqlCommand> action, Func<SqlDataReader, TEntity> read)
        {
            var response = new Response<TEntity>();
            using var connection = CadenaConexion.ObtenerConexion();

            try
            {
                await connection.OpenAsync().ConfigureAwait(false);
                using var cmd = new SqlCommand(storedProcedure, connection) { CommandType = CommandType.StoredProcedure };
                action?.Invoke(cmd);
                using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                if (reader.HasRows)
                {
                    response.Data = read(reader);
                    response.Status = Status.Success.ToString();
                    response.Message = "Operacion realizada con exito";
                }
                else
                {
                    response.Status = Status.Information.ToString();
                    response.Message = "Lista vacia";
                }
            }
            catch (SqlException ex)
            {
                response.Status = Status.Error.ToString();
                response.Message = GetExcepcionMessage(ex);
            }
            finally
            {
                await connection.CloseAsync().ConfigureAwait(false);
            }
            return response;
        }
    }
     
}
