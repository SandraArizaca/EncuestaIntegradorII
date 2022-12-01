using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace encuesta.Contexto
{
    public class AccesoDatos
    {
        private string CadenaConexion;

        public AccesoDatos()
        {
            this.CadenaConexion = ConfigurationManager.ConnectionStrings["CTX_Encuesta"].ConnectionString;
        }

        public string ejecutarComandoWithParameters(string nombreSP, string Parameter = "", int tiempo = 0)
        {
            string result = "";
            using (SqlConnection sqlConnection = new SqlConnection(this.CadenaConexion))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(nombreSP, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@parametros", Parameter);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (tiempo != 0) sqlCommand.CommandTimeout = tiempo;
                    object obj = sqlCommand.ExecuteScalar();
                    bool flag = obj != null;
                    if (flag)
                    {
                        result = obj.ToString();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }
    }
}