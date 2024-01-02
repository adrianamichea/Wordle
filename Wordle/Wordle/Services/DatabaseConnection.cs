using System;
using System.Data.SqlClient;

namespace Wordle.Services
{
    public sealed class DatabaseConnection
    {
        private static readonly Lazy<DatabaseConnection> lazyInstance = new Lazy<DatabaseConnection>(() => new DatabaseConnection());

        private SqlConnection connection;

        private DatabaseConnection()
        {
            string connectionString = "Data Source=DESKTOP-UHC6S14;Initial Catalog=WordleDB;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
        }

        public static DatabaseConnection Instance => lazyInstance.Value;

        public SqlConnection GetConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
