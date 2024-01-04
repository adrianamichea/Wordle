using System.Data.SqlClient;
using System.Data;
using System;
using Wordle.Models;

namespace Wordle.Services
{
    public class AuthentificationService
    {
        readonly private DatabaseConnection _databaseConnection = DatabaseConnection.Instance;

        private static AuthentificationService _instance;
        private static readonly object LockObject = new object();

        private int? _authenticatedUserId;


        private AuthentificationService()
        {

        }


        public static AuthentificationService Instance
        {
            get
            {
                lock (LockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new AuthentificationService();
                    }
                    return _instance;
                }
            }
        }


        public int? GetAuthenticatedUserId()
        {
            return _authenticatedUserId;
        }

        public void ClearAuthenticatedUser()
        {
            _authenticatedUserId = null;
        }

        public bool Authenticate(string username, string password, out string errorMessage)
        {
            errorMessage = null;
            //Console.WriteLine("Connection State Before: " + connection.State);

            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                Console.WriteLine("Connection State After: " + connection.State);

                using (SqlCommand command = new SqlCommand("dbo.AuthenticateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Password", password);

                    command.ExecuteNonQuery();

                    int result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {
                        _authenticatedUserId= getUserId(username);
                        Console.WriteLine("Authenticated user id din serviciu: " + _authenticatedUserId);
                        return true;
                    }
                    else
                    {
                        errorMessage = "Authentication failed. Invalid username or password.";
                        return false;
                    }
                }
            }
        }

        public bool Register(string username, string password, out string errorMessage)
        {
            errorMessage = null;
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                
                using (SqlCommand command = new SqlCommand("dbo.RegisterUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.ExecuteNonQuery();

                    if (command.ExecuteScalar()== null)
                    {
                        return true;
                    }
                    else
                    {
                        errorMessage = "Registration failed. Invalid username or password.";
                        return false;
                    }
                }
            }
        }

        public int getUserId(string username)
        {
            int userId = 0;
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("dbo.GetUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", username);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = (int)reader["ID"];
                        }
                    }
                }
            }
            return userId;
        }
    }
}
