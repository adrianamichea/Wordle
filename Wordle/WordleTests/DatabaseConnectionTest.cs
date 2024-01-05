using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using Wordle.Services;

namespace WordleTests
{
    [TestClass]
    public class DatabaseConnectionTest
    {
        [TestMethod]
        public void GetConnection_ReturnsOpenConnection()
        {
            string connectionString = "Data Source=DESKTOP-O9QBF35;Initial Catalog=WordleDB;Integrated Security=True;";
            // Arrange
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();


            // Assert
            Assert.IsNotNull(connection);
            Assert.AreEqual(System.Data.ConnectionState.Open, connection.State);
            connection.Close();
        }
    }
}
