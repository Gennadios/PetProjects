using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PizzaSushiBot
{
    [Obsolete]
    static class DBManager
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["PizzaSushiDB"].ConnectionString;
        private static SqlConnection _sqlConnection;
        private static SqlDataReader _sqlDataReader;
        private static SqlCommand _sqlCommand;

        internal static string ConnectionString { get => ConnectionString; }
        internal static SqlConnection SQLConnection { get => _sqlConnection; }
        internal static SqlDataReader SQLDataReader { get => _sqlDataReader;  set => _sqlDataReader = value; }
        internal static SqlCommand SQLCommand { get => _sqlCommand; set => _sqlCommand = value; }

        internal static void OpenDataBaseConnection()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();
        }

        internal static void CloseDataBaseConnection()
        {
            if (_sqlConnection.State == ConnectionState.Open)
                _sqlConnection.Close();

            _sqlDataReader?.Close();
        }
    }
}
