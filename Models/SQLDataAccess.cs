using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace VehicleManagement.Models
{
    public static class SQLDataAccess
    {
            public static string GetConnectionString(string connectionName = "Data Source=OAKSQL04; Initial Catalog = loadingApp; User Id=loadingApp.service; Password=3M55ATk2TMAA; Pooling=False;Connect Timeout = 30")
            {
                return connectionName;
            }

            public static List<T> LoadData<T>(string sql)
            {
                using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
                {
                    return cnn.Query<T>(sql).ToList();
                }
            }

            public static int SaveData<T>(string sql, T data)
            {
                using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
                {
                    return cnn.Execute(sql, data);
                }
            }
        }
}
