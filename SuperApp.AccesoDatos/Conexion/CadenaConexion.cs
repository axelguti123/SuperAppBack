using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.AccesoDatos.Conexion
{
    internal sealed class CadenaConexion
    {
        private readonly static CadenaConexion _instance = new CadenaConexion();
        private static readonly string _conexion = "Server=MSI\\MSSQLSERVER01;Database=dbObra;User Id=Axel;Password=1234;Max Pool Size=5;Min Pool Size=2;Pooling=true;";
        public static CadenaConexion Instance
        {
            get
            {
                return _instance;
            }
        }

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_conexion);
        }
        private CadenaConexion()
        {

        }
    }
}
