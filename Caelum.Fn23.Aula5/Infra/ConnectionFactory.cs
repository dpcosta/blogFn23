using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Caelum.Fn23.Curso.Infra
{
    public class ConnectionFactory
    {
        public static IDbConnection CriaConexaoAberta()
        {
            //o que o sistema deve fazer caso ocorra algum erro na conexão?
            var connString = ConfigurationManager.ConnectionStrings["blog"].ConnectionString;
            var cnx = new SqlConnection(connString);
            cnx.Open();
            return cnx;
        }
    }
}