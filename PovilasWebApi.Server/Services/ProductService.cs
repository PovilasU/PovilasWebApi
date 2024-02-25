using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PovilasWebApi.Server.Models;

namespace PovilasWebApi.Server.Services
{
    public class ProductService
    {
        private readonly string _connectionString;

        public ProductService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();

            using var connection = new MySqlConnection(_connectionString);
            using var command = new MySqlCommand("SELECT Id, Name, Price FROM Product", connection);

            try
            {
                connection.Open();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    };
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return products;
        }
    }
}