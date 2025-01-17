using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProductLifecycleManagement.Models;

namespace ProductLifecycleManagement.Services
{
    public class ProductStageHistoryService
    {
        public List<ProductStageHistory> GetAllHistories()
        {
            var histories = new List<ProductStageHistory>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM ProductStageHistory", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        histories.Add(new ProductStageHistory
                        {
                            StageId = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            StartOfStage = reader.GetDateTime(2),
                            UserId = reader.GetInt32(3)
                        });
                    }
                }
            }
            return histories;
        }

        public void AddHistory(ProductStageHistory history)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO ProductStageHistory (Stage_Id, Product_Id, Start_Of_Stage, User_Id) " +
                    "VALUES (@StageId, @ProductId, @StartOfStage, @UserId)", connection);
                command.Parameters.AddWithValue("@StageId", history.StageId);
                command.Parameters.AddWithValue("@ProductId", history.ProductId);
                command.Parameters.AddWithValue("@StartOfStage", history.StartOfStage);
                command.Parameters.AddWithValue("@UserId", history.UserId);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteHistory(int stageId, int productId, DateTime startOfStage)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "DELETE FROM ProductStageHistory WHERE Stage_Id=@StageId AND Product_Id=@ProductId AND Start_Of_Stage=@StartOfStage",
                    connection);
                command.Parameters.AddWithValue("@StageId", stageId);
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@StartOfStage", startOfStage);
                command.ExecuteNonQuery();
            }
        }
    }
}
