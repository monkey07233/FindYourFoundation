using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FindYourFoundation.Repositoires
{
    public class DataAccessLayer
    {
        private static readonly string ServerName = ConfigurationManager.ConnectionStrings["FYF"].ConnectionString;

        public IEnumerable<T> Query<T>(string sql,object param = null)
        {
            var sqlConnection = new SqlConnection(ServerName);
            using(var conn = sqlConnection)
            {
                return conn.Query<T>(sql, param);
            }
        }
        public void Execute(string sql,object param = null)
        {
            var sqlConnection = new SqlConnection(ServerName);
            using(var conn = sqlConnection)
            {
                conn.Execute(sql, param);
            }
        }
    }
}