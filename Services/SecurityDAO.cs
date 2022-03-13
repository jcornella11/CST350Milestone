using CST350Milestone.Models;
using System.Data.SqlClient;

namespace CST350Milestone.Services
{
    public class SecurityDAO
    {

        string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Login;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool InsertNewUser(UserModel user)
        {
            bool success = false;

            // Generate Insert Statement
            string sqlStatement = "INSERT INTO USERS (FIRST_NAME, LAST_NAME, SEX, AGE, STATE, EMAIL_ADDRESS, USERNAME, PASSWORD) VALUES(@FIRST_NAME, @LAST_NAME, @SEX, @AGE, @STATE, @EMAIL_ADDRESS, @USERNAME, @PASSWORD);";

            // Attempt connection
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Generate command
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                // Add command parameters
                command.Parameters.Add("@FIRST_NAME", System.Data.SqlDbType.VarChar, 10).Value = user.FirstName.ToUpper();
                command.Parameters.Add("@LAST_NAME", System.Data.SqlDbType.VarChar, 10).Value = user.LastName.ToUpper();
                command.Parameters.Add("@SEX", System.Data.SqlDbType.VarChar, 6).Value = user.Sex.ToUpper();
                command.Parameters.Add("@AGE", System.Data.SqlDbType.Int).Value = user.Age;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 12).Value = user.State.ToUpper();
                command.Parameters.Add("@EMAIL_ADDRESS", System.Data.SqlDbType.VarChar, 40).Value = user.Email.ToUpper();
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 10).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 20).Value = user.Password;

                // Attempt insert
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return success;
        }

        public bool FindUserByUsernameAndPassword(UserModel user)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.users WHERE USERNAME = @USERNAME and PASSWORD = @PASSWORD";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 10).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 20).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
                connection.Close();
            }
            return success;
        }

        public UserModel ReturnUser(UserModel user)
        {
            string sqlStatement = "SELECT * FROM dbo.users WHERE USERNAME = @USERNAME";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 10).Value = user.Username;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.FirstName = reader.GetString(1);
                            user.LastName = reader.GetString(2);
                            user.Sex = reader.GetString(3);
                            user.Age = reader.GetInt32(4);
                            user.State = reader.GetString(5);
                            user.Email = reader.GetString(6);
                            user.Username = reader.GetString(7);
                            user.Password = reader.GetString(8);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
                connection.Close();
            }

            return user;
        }

    }
}
