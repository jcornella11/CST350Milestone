using CST350Milestone.Models;
using System.Data.SqlClient;

namespace CST350Milestone.Services
{
    public class GameDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Test; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Shows all Saved Games
        public List<GameModel> AllGames() 
        {
            List<GameModel> foundGames = new List<GameModel>();

            string sqlStatement = "SELECT * FROM dbo.games";

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundGames.Add(new GameModel((int)reader[0], (DateTime)reader[1],
                            (string)reader[2], (string)reader[3], (string)reader[4]));
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return foundGames;
        }

        //Displays Content of a Single Game Specified by Id
        public GameModel GetGameById(int id) 
        {
            GameModel foundGame = null;

            string sqlStatement = "SELECT * FROM dbo.games WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                SqlCommand command = new SqlCommand(@sqlStatement, connection);

                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundGame = new GameModel((int)reader[0], (DateTime)reader[1],
                            (string)reader[2], (string)reader[3], (string)reader[4]);
                    }
                }
                catch (Exception ex) 
                {
                    Console.Write(ex.Message);
                }
            }
            return foundGame;
        }

        //Delete a Game from the Database Specified by Id
        public void Delete(int id) 
        {
            string sqlStatement = "DELETE FROM dbo.games WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                SqlCommand command = new SqlCommand(@sqlStatement, connection);

                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) 
                {
                    Console.Write(ex.Message);
                }
            }
        }


        //Inserts a New Game Into the Database
        public void Insert(GameModel game) 
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                string query = "INSERT INTO dbo.games (DateTime, firstName, lastName, gameData) VALUES (@DateTime, @firstName, @lastName, @gameData)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DateTime", game.gameTime);
                command.Parameters.AddWithValue("@firstName", game.firstName);
                command.Parameters.AddWithValue("@lastName", game.lastName);
                command.Parameters.AddWithValue("@gameData", game.gameData);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           
        }
    }


}
