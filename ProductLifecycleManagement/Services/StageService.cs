using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProductLifecycleManagement.Models;

namespace ProductLifecycleManagement.Services
{
    public class StageService
    {
        public List<Stage> GetAllStages()
        {
            var stages = new List<Stage>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Stages", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stages.Add(new Stage
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2)
                        });
                    }
                }
            }
            return stages;
        }

        public void AddStage(Stage stage)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Stages (Name, Description) VALUES (@Name, @Description)", connection);
                command.Parameters.AddWithValue("@Name", stage.Name);
                command.Parameters.AddWithValue("@Description", stage.Description);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateStage(Stage stage)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Stages SET Name=@Name, Description=@Description WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", stage.Id);
                command.Parameters.AddWithValue("@Name", stage.Name);
                command.Parameters.AddWithValue("@Description", stage.Description);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteStage(int stageId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Stages WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", stageId);
                command.ExecuteNonQuery();
            }
        }
    }
}
