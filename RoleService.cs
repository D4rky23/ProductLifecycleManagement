using System.Collections.Generic;
using System.Data.SqlClient;
using ProductLifecycleManagement.Models;

namespace ProductLifecycleManagement.Services
{
    public class RoleService
    {
        public List<Role> GetAllRoles()
        {
            var roles = new List<Role>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Roles", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            Id = reader.GetInt32(0),
                            RoleName = reader.GetString(1)
                        });
                    }
                }
            }
            return roles;
        }

        public void AddRole(Role role)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Roles (Role_Name) VALUES (@RoleName)", connection);
                command.Parameters.AddWithValue("@RoleName", role.RoleName);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateRole(Role role)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Roles SET Role_Name=@RoleName WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", role.Id);
                command.Parameters.AddWithValue("@RoleName", role.RoleName);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteRole(int roleId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Roles WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", roleId);
                command.ExecuteNonQuery();
            }
        }
    }
}
