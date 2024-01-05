using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Models;
using System.Windows;

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
                            string[] attemptsFromDb = ((string)reader["Attempts"]).Split(',');
                            gameEntity.Attempts = new string[6];
                            Array.Copy(attemptsFromDb, gameEntity.Attempts, Math.Min(attemptsFromDb.Length, gameEntity.Attempts.Length));
                            string[] codesFromDb = ((string)reader["Codes"]).Split(',');
                            gameEntity.Codes = new string[6];
                            Array.Copy(codesFromDb, gameEntity.Codes, Math.Min(codesFromDb.Length, gameEntity.Codes.Length));

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
                        MessageBox.Show(errorMessage);
                        return false;
                    }

                }
            }
    }
}
