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
                            EstimatedHeight = (float)reader.GetDecimal(3),
                            EstimatedWidth = (float)reader.GetDecimal(4),
                            EstimatedWeight = (float)reader.GetDecimal(5),
                            BOMId = reader.GetInt32(6)
                        });
                    }
                }
            }
            return products;
        }

        public void AddProduct(Product product)
        {
            if (!IsBOMIdValid(product.BOMId))
            {
                throw new Exception("Invalid BOM Id.");
            }

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
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex) when (ex.Message.Contains("FK__Products__BOM_Id"))
                {
                    throw new Exception("There is already a product with the same BOM Id.", ex);
                }
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

        public bool IsBOMIdValid(int bomId)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                // Verificăm dacă BOM ID există în tabela BOM
                string query = "SELECT COUNT(*) FROM dbo.BOM WHERE Id = @BOMId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BOMId", bomId);
                    int bomCount = (int)command.ExecuteScalar();

                    // Dacă BOM ID nu există în tabelul BOM, returnăm false
                    if (bomCount == 0)
                    {
                        return false;
                    }
                }

                // Verificăm dacă BOM ID este deja asociat cu un produs
                query = "SELECT COUNT(*) FROM dbo.Products WHERE BOM_Id = @BOMId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BOMId", bomId);
                    int productCount = (int)command.ExecuteScalar();

                    // Dacă BOM ID nu este asociat cu un produs, este valid
                    return productCount == 0;
                }
            }
        }

    }
}
