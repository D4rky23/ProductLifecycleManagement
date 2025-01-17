using System;
using System.Data.SqlClient;
using ProductLifecycleManagement.Config;    

namespace ProductLifecycleManagement.Services
{
    public static class DatabaseHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(DatabaseConfig.ConnectionString);
        }
    }
}
