using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Models;

namespace Wordle.Services
{
    public class GameService
    {
        readonly private DatabaseConnection _databaseConnection = DatabaseConnection.Instance;

        public GameEntity getLastGamePlayedByUser(int userId, out string errorMessage)
        {
            errorMessage = null;
            Console.WriteLine("Am ajuns la getLastGamePlayedByUser");
            using (SqlConnection connection = _databaseConnection.GetConnection())
            {
                Console.WriteLine("Am trecut de get connection?");

                using (SqlCommand command = new SqlCommand("dbo.LastGameByUserID", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userId);

                    command.ExecuteNonQuery();


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            GameEntity gameEntity = new GameEntity();
                            gameEntity.UserID = (int)reader["UserID"];
                            gameEntity.SecretWord = (string)reader["SecretWord"];
                            gameEntity.Attempts = ((string)reader["Attempts"]).Split(',');
                            gameEntity.Codes = ((string)reader["Codes"]).Split(',');
                            errorMessage = null;

                            
                            return gameEntity;
                        }
                        else
                        {
                            errorMessage = "No game found for this user.";
                            return null;
                        }
                    }
                }

            }

        }

        public bool updateLastGamePlayedByUser(GameEntity gameEntity, out string errorMessage)
        {
            errorMessage = null;

            using (SqlConnection connection = _databaseConnection.GetConnection())
            using (SqlCommand command = new SqlCommand("dbo.UpdateLastGame", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", gameEntity.UserID);
                command.Parameters.AddWithValue("@SecretWord", gameEntity.SecretWord);
                command.Parameters.AddWithValue("@Attempts", string.Join(",", gameEntity.Attempts));
                command.Parameters.AddWithValue("@Codes", string.Join(",", gameEntity.Codes));
                command.ExecuteNonQuery();

                if (command.ExecuteScalar() == null)
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
