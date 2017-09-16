using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AsyncSamples
{
    class Program
    {
        static async Task Main()
        {
            await ReadAsync(714);
        }

        public static async Task ReadAsync(int productId)
        {
            var connection = new SqlConnection(GetConnectionString());

            string sql = "SELECT Prod.ProductID, Prod.Name, Prod.StandardCost, Prod.ListPrice, CostHistory.StartDate, CostHistory.EndDate, CostHistory.StandardCost " +
                "FROM Production.ProductCostHistory AS CostHistory  " +
                "INNER JOIN Production.Product AS Prod ON CostHistory.ProductId = Prod.ProductId " +
                  "WHERE Prod.ProductId = @ProductId";
            var command = new SqlCommand(sql, connection);
            var productIdParameter = new SqlParameter("ProductId", SqlDbType.Int);
            productIdParameter.Value = productId;
            command.Parameters.Add(productIdParameter);

            await connection.OpenAsync();

            using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
            {
                while (await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    DateTime from = reader.GetDateTime(4);
                    DateTime? to = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                    decimal standardPrice = reader.GetDecimal(6);
                    Console.WriteLine($"{id} {name} from: {from:d} to: {to:d}; price: {standardPrice}");
                }
            }
        }

        public static string GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("config.json");

            IConfiguration config = configurationBuilder.Build();
            return config["Data:DefaultConnection:ConnectionString"];
        }
    }
}
