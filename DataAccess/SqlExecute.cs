using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChampService.DataAccess
{
    public class SqlExecute
    {
        SqlConnection con;
        public SqlExecute()
        {
            string conString = ConfigurationManager.ConnectionStrings["SQLServerDB"].ConnectionString;
            con = new SqlConnection(conString);
        }

        public SqlDataReader runCommand(string querystring)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();

            //con.Close();
            return reader;
        }

        public int executeNonQuery(string querystring)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            int affectedRows = cmd.ExecuteNonQuery();
            return affectedRows;
        }
    }
}