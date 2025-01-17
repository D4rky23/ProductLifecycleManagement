using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ProductLifecycleManagement.Models;
using System.Security.Cryptography;


namespace ProductLifecycleManagement.Services
{
    public class UserService
    {
        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Users", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Name = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Password = reader.GetString(4) // Adăugat
                        });
                    }
                }
            }
            return users;
        }


        public bool EmailExists(string email)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@Email", connection);
                command.Parameters.AddWithValue("@Email", email);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public int AddUser(User user)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Users (Email, Name, Phone_Number, Password) OUTPUT INSERTED.Id VALUES (@Email, @Name, @PhoneNumber, @Password)", connection);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@Password", user.Password);

                // Obține ID-ul utilizatorului nou creat
                int userId = (int)command.ExecuteScalar();
                return userId;
            }
        }




        public void UpdateUser(User user)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Users SET Email=@Email, Name=@Name, Phone_Number=@PhoneNumber, Password=@Password WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@Password", user.Password); // Adăugat
                command.ExecuteNonQuery();
            }
        }


        public void DeleteUser(int userId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Users WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", userId);
                command.ExecuteNonQuery();
            }
        }

        public User AuthenticateUser(string email, string password)
        {
            string hashedPassword = HashPassword(password); // Hash password before checking
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT Id, Email, Name, Phone_Number, Password FROM Users WHERE Email=@Email AND Password=@Password", connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Name = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Password = reader.GetString(4)
                        };
                    }
                }
            }
            return null;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



        public bool IsAdmin(int userId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT COUNT(*) FROM UserRoles WHERE User_Id=@UserId AND Role_Id=" +
                    "(SELECT Id FROM Roles WHERE Role_Name='Admin')", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
