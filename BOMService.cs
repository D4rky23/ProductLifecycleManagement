using System.Collections.Generic;
using System.Data.SqlClient;
using ProductLifecycleManagement.Models;

namespace ProductLifecycleManagement.Services
{
    public class BOMService
    {
        public List<BOM> GetAllBOMs()
        {
            var boms = new List<BOM>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM BOM", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        boms.Add(new BOM
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return boms;
        }

        public void AddBOM(BOM bom)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO BOM (Name) VALUES (@Name)", connection);
                command.Parameters.AddWithValue("@Name", bom.Name);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateBOM(BOM bom)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE BOM SET Name=@Name WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", bom.Id);
                command.Parameters.AddWithValue("@Name", bom.Name);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteBOM(int bomId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM BOM WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", bomId);
                command.ExecuteNonQuery();
            }
        }
    }
}
