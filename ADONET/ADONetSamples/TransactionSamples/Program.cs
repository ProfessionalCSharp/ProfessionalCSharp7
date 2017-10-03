using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace TransactionSamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TransactionSampleAsync();
        }

        private static async Task TransactionSampleAsync()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync();
                SqlTransaction tx = connection.BeginTransaction();

                try
                {
                    string sql = "INSERT INTO [ProCSharp].[Books] ([Title], [Publisher], [Isbn], [ReleaseDate]) " +
                        "VALUES (@Title, @Publisher, @Isbn, @ReleaseDate);" +
                        "SELECT SCOPE_IDENTITY()";
                    var command = new SqlCommand
                    {
                        CommandText = sql,
                        Connection = connection,
                        Transaction = tx
                    };

                    var p1 = new SqlParameter("Title", SqlDbType.NVarChar, 50);
                    var p2 = new SqlParameter("Publisher", SqlDbType.NVarChar, 50);
                    var p3 = new SqlParameter("Isbn", SqlDbType.NVarChar, 20);
                    var p4 = new SqlParameter("ReleaseDate", SqlDbType.Date);
                    command.Parameters.AddRange(new SqlParameter[] { p1, p2, p3, p4 });
                
                    command.Parameters["Title"].Value = "Professional C# 8 and .NET Core 3.0";
                    command.Parameters["Publisher"].Value = "Wrox Press";
                    command.Parameters["Isbn"].Value = "42-08154711";
                    command.Parameters["ReleaseDate"].Value = new DateTime(2020, 9, 2);

                    object id = await command.ExecuteScalarAsync();
                    Console.WriteLine($"record added with id: {id}");

                    command.Parameters["Title"].Value = "Professional C# 9 and .NET Core 4.0";
                    command.Parameters["Publisher"].Value = "Wrox Press";
                    command.Parameters["Isbn"].Value = "42-08154711";
                    command.Parameters["ReleaseDate"].Value = new DateTime(2022, 11, 2);

                    id = await command.ExecuteScalarAsync();
                    Console.WriteLine($"record added with id: {id}");
                   // throw new Exception("abort");

                    tx.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error {ex.Message}, rolling back");
                    tx.Rollback();
                }
            }
        }

        public static string GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            IConfiguration config = configurationBuilder.Build();
            string connectionString = config["Data:DefaultConnection:ConnectionString"];
            return connectionString;
        }
    }
}
