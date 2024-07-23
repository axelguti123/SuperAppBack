using SuperApp.AccesoDatos.Conexion;
using SuperApp.AccesoDatos.Excepciones;
using SuperApp.AccesoDatos.Interfaz;
using SuperApp.AccesoDatos.Utilidades;
using SupperApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace SuperApp.AccesoDatos.DAO
{
    internal class UsuarioDAO: IUsuario
    {


        public async Task<Response> Create(Usuario data)
        {
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_C_USUARIO", cmd =>
            {
                cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", data.Nombre);
                cmd.Parameters.AddWithValue("@apellido", data.Apellido);
                cmd.Parameters.AddWithValue("@fechaNacimiento", data.FechaNacimiento);
                cmd.Parameters.AddWithValue("@nombreUsuario", data.Nombre_de_usuario);
                cmd.Parameters.AddWithValue("@contraseña", data.Contraseña);
                cmd.Parameters.AddWithValue("@activo", data.IsActivo);
            });
        }

        public async Task<Response> Delete(int id)
        {
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_D_USUARIO", cmd =>
            {
                cmd.Parameters.AddWithValue("@idUsuario", id);
            });
            
        }

        public async Task<Response<Usuario>> Find(int id)
        {
            return await DataBaseHelpers.ExecuteReaderAsync<Usuario>("SP_F_USUARIO", cmd =>
            {
                cmd.Parameters.AddWithValue("@idUsuario", id);
            }, reader =>
            {
                if (reader.Read())
                {
                    return new Usuario()
                    {


                        IDUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                        IDEspecialidad = reader.GetInt32(reader.GetOrdinal("idEspecialidad")),
                        Especialidads = new Especialidad()
                        {
                            NombreEspecialidad = reader.GetString(reader.GetOrdinal("nombreEspecialidad"))
                        },
                        Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                        IsActivo = reader.GetBoolean(reader.GetOrdinal("activo"))


                    };
                }
                throw new UsuarioNoEncontradoException("No se pudo encontrar al usuario");

            });
        }

        public async Task<Response<IEnumerable<Usuario>>> GetAll()
        {
            return await DataBaseHelpers.ExecuteReaderAsync("SP_R_USUARIOS", null, reader =>
            {
                var list = new List<Usuario>();
                while (reader.Read())
                {
                    list.Add(new Usuario()
                    {
                        IDUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                        IDEspecialidad = reader.GetInt32(reader.GetOrdinal("idEspecialidad")),
                        Especialidads = new Especialidad()
                        {
                            NombreEspecialidad = reader.GetString(reader.GetOrdinal("nombreEspecialidad"))
                        },
                        Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                        IsActivo = reader.GetBoolean(reader.GetOrdinal("activo"))
                    });
                }
                return list.AsEnumerable();
            });
        }

        public async Task<Response> Update(Usuario data)
        {
            return await DataBaseHelpers.ExecuteNonQueryAsync("SP_U_USUARIO", cmd =>
            {
                cmd.Parameters.AddWithValue("@idUsuario", data.IDUsuario);
                cmd.Parameters.AddWithValue("@idEspecialidad", data.IDEspecialidad);
                cmd.Parameters.AddWithValue("@nombre", data.Nombre);
                cmd.Parameters.AddWithValue("@apellido", data.Apellido);
                cmd.Parameters.AddWithValue("@activo", data.IsActivo);
            });
        }
    }
}
