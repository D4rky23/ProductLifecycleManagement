using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProductLifecycleManagement.Models;

namespace ProductLifecycleManagement.Services
{
    public class ProductService
    {
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Products", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            EstimatedHeight = (float)reader.GetDouble(3),
                            EstimatedWidth = (float)reader.GetDouble(4),
                            EstimatedWeight = (float)reader.GetDouble(5),
                            BOMId = reader.GetInt32(6)
                        });
                    }
                }
            }
            return products;
        }

        public void AddProduct(Product product)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Products (Name, Description, Estimated_Height, Estimated_Width, Estimated_Weight, BOM_Id) " +
                    "VALUES (@Name, @Description, @Height, @Width, @Weight, @BOMId)", connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Height", product.EstimatedHeight);
                command.Parameters.AddWithValue("@Width", product.EstimatedWidth);
                command.Parameters.AddWithValue("@Weight", product.EstimatedWeight);
                command.Parameters.AddWithValue("@BOMId", product.BOMId);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE Products SET Name=@Name, Description=@Description, Estimated_Height=@Height, " +
                    "Estimated_Width=@Width, Estimated_Weight=@Weight, BOM_Id=@BOMId WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Height", product.EstimatedHeight);
                command.Parameters.AddWithValue("@Width", product.EstimatedWidth);
                command.Parameters.AddWithValue("@Weight", product.EstimatedWeight);
                command.Parameters.AddWithValue("@BOMId", product.BOMId);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Products WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", productId);
                command.ExecuteNonQuery();
            }
        }
    }
}
