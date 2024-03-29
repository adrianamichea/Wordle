﻿using System;
using System.Data.SqlClient;

namespace Wordle.Services
{
    public sealed class DatabaseConnection
    {
        private static readonly Lazy<DatabaseConnection> lazyInstance = new Lazy<DatabaseConnection>(() => new DatabaseConnection());

        private SqlConnection connection;
        string connectionString = "Data Source=DESKTOP-O9QBF35;Initial Catalog=WordleDB;Integrated Security=True;";

        private DatabaseConnection()
        {
           // string connectionString = "Data Source=DESKTOP-O9QBF35;Initial Catalog=WordleDB;Integrated Security=True;";
           // string connectionString = "Data Source=DESKTOP-UHC6S14;Initial Catalog=WordleDB;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
        }

        public static DatabaseConnection Instance => lazyInstance.Value;

        public SqlConnection GetConnection()
        {
            SqlConnection newConnection = new SqlConnection(connectionString);

            if (newConnection.State != System.Data.ConnectionState.Open)
            {
                newConnection.Open();
            }

            return newConnection;
        }

    }
}
