using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Services
{
    public class AuthentificationService
    {
        readonly private string _connectionString = "Data Source=DESKTOP-UHC6S14;Initial Catalog=WordleDB;Integrated Security=True;";

        public bool Authenticate(string username, string password, out string errorMessage)
        {
            errorMessage = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.AuthenticateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Password", password);

                    command.ExecuteNonQuery();

                    int result = (int)command.ExecuteScalar();

                    if (result > 0)
                    {
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
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

    }
}
