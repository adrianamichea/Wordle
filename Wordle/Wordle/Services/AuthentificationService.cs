using System.Data.SqlClient;
using System.Data;


namespace Wordle.Services
{
    public class AuthentificationService
    {
        readonly private DatabaseConnection _databaseConnection = DatabaseConnection.Instance;

        public bool Authenticate(string username, string password, out string errorMessage)
        {
            errorMessage = null;

            using (SqlConnection connection = _databaseConnection.GetConnection())
            {

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

    }
}
