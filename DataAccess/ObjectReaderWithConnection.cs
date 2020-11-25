using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChampService.DataAccess
{
   public  abstract class ObjectReaderWithConnection<T> : ObjectReaderBase<T>
    {
        private static string m_connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLServerDB"].ConnectionString;
        //private static string m_connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\work\careofhomes\champ-webservice\ChampService\App_Data\SampleDB.mdf;Integrated Security=True";

        protected override System.Data.IDbConnection GetConnection()
        {
            // update to get your connection here

            IDbConnection connection = new SqlConnection(m_connectionString);
            return connection;
        }
    }
}