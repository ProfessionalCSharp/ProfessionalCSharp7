using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace AsyncSamples
{
    class Program
    {
        static async Task Main()
        {
            await ReadAsync("Wrox Press");
        }

        public static async Task ReadAsync(string title)
        {
            var connection = new SqlConnection(GetConnectionString());

            string sql = "SELECT [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books] WHERE lower([Title]) LIKE @Title ORDER BY [ReleaseDate]";

            var command = new SqlCommand(sql, connection);
            var titleParameter = new SqlParameter("Title", SqlDbType.NVarChar, 50);
            titleParameter.Value = title;
            command.Parameters.Add(titleParameter);

            await connection.OpenAsync();

            using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
            {
                while (await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0);
                    string bookTitle = reader.GetString(1);
                    string publisher = reader[2].ToString();
                    DateTime? releaseDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);
                    Console.WriteLine($"{id,5}. {bookTitle,-40} {publisher,-15} {releaseDate:d}");
                }
            }
        }

        public static string GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            IConfiguration config = configurationBuilder.Build();
            return config["Data:DefaultConnection:ConnectionString"];
        }
    }
}
