using System.Collections.Generic;
using System.Data.SqlClient;
using ProductLifecycleManagement.Models;

namespace ProductLifecycleManagement.Services
{
    public class UserRoleService
    {
        public List<UserRole> GetAllUserRoles()
        {
            var userRoles = new List<UserRole>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM UserRoles", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userRoles.Add(new UserRole
                        {
                            UserId = reader.GetInt32(0),
                            RoleId = reader.GetInt32(1)
                        });
                    }
                }
            }
            return userRoles;
        }

        public void AddUserRole(UserRole userRole)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO UserRoles (User_Id, Role_Id) VALUES (@UserId, @RoleId)", connection);
                command.Parameters.AddWithValue("@UserId", userRole.UserId);
                command.Parameters.AddWithValue("@RoleId", userRole.RoleId);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUserRole(int userId, int roleId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM UserRoles WHERE User_Id=@UserId AND Role_Id=@RoleId", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@RoleId", roleId);
                command.ExecuteNonQuery();
            }
        }
    }
}
