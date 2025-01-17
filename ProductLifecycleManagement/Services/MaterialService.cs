using System.Collections.Generic;
using System.Data.SqlClient;
using ProductLifecycleManagement.Models;

namespace ProductLifecycleManagement.Services
{
    public class MaterialService
    {
        public List<Material> GetAllMaterials()
        {
            var materials = new List<Material>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Materials", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        materials.Add(new Material
                        {
                            MaterialNumber = reader.GetInt32(0),
                            MaterialDescription = reader.GetString(1),
                            Weight = (float)reader.GetDouble(2),
                            Width = (float)reader.GetDouble(3),
                            Height = (float)reader.GetDouble(4)
                        });
                    }
                }
            }
            return materials;
        }

        public void AddMaterial(Material material)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Materials (Material_Number, Material_Description, Weight, Width, Height) " +
                    "VALUES (@MaterialNumber, @Description, @Weight, @Width, @Height)", connection);
                command.Parameters.AddWithValue("@MaterialNumber", material.MaterialNumber);
                command.Parameters.AddWithValue("@Description", material.MaterialDescription);
                command.Parameters.AddWithValue("@Weight", material.Weight);
                command.Parameters.AddWithValue("@Width", material.Width);
                command.Parameters.AddWithValue("@Height", material.Height);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateMaterial(Material material)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Materials SET Material_Description=@Description, Weight=@Weight, " +
                    "Width=@Width, Height=@Height WHERE Material_Number=@MaterialNumber", connection);
                command.Parameters.AddWithValue("@MaterialNumber", material.MaterialNumber);
                command.Parameters.AddWithValue("@Description", material.MaterialDescription);
                command.Parameters.AddWithValue("@Weight", material.Weight);
                command.Parameters.AddWithValue("@Width", material.Width);
                command.Parameters.AddWithValue("@Height", material.Height);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteMaterial(int materialNumber)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Materials WHERE Material_Number=@MaterialNumber", connection);
                command.Parameters.AddWithValue("@MaterialNumber", materialNumber);
                command.ExecuteNonQuery();
            }
        }
    }
}
