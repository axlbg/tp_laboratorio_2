using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Entidades
{
    public static class PaqueteDAO
    {
        private static SqlCommand _comando;
        private static SqlConnection _conexion;

        public static bool Insertar(Paquete p)
        {
            bool retorno = false;
            string sql = "INSERT INTO correo-sp-2017 (direccionEntrega,trackingID,alumno) VALUES('";
            sql += p.DireccionEntrega + "'," + p.TrackingID + "','" + "GuzmanAxel')";

            try
            {
                PaqueteDAO._comando.CommandText = sql;
                PaqueteDAO._conexion.Open();
                PaqueteDAO._comando.ExecuteNonQuery();
                retorno = true;
            }
            catch(Exception e)
            {
                retorno = false;
            }
            finally
            {
                if(retorno)
                {
                    PaqueteDAO._conexion.Close();
                }
            }
            return retorno;
        }

        static PaqueteDAO()
        {
            PaqueteDAO._conexion = new SqlConnection(Properties.Settings.Default.Conexion);
            PaqueteDAO._comando = new SqlCommand();
            PaqueteDAO._comando.CommandType = System.Data.CommandType.Text;
            PaqueteDAO._comando.Connection = PaqueteDAO._conexion;
        }
    }
}
