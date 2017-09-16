using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
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
                    string sql = "INSERT INTO Sales.CreditCard (CardType, CardNumber, ExpMonth, ExpYear)" +
                        "VALUES (@CardType, @CardNumber, @ExpMonth, @ExpYear); " +
                        "SELECT SCOPE_IDENTITY()";
                    var command = new SqlCommand
                    {
                        CommandText = sql,
                        Connection = connection,
                        Transaction = tx
                    };

                    var p1 = new SqlParameter("CardType", SqlDbType.NVarChar, 50);
                    var p2 = new SqlParameter("CardNumber", SqlDbType.NVarChar, 25);
                    var p3 = new SqlParameter("ExpMonth", SqlDbType.TinyInt);
                    var p4 = new SqlParameter("ExpYear", SqlDbType.SmallInt);
                    command.Parameters.AddRange(new SqlParameter[] { p1, p2, p3, p4 });
                
                    command.Parameters["CardType"].Value = "MegaWoosh";
                    command.Parameters["CardNumber"].Value = "08154711128";
                    command.Parameters["ExpMonth"].Value = 4;
                    command.Parameters["ExpYear"].Value = 2019;

                    object id = await command.ExecuteScalarAsync();
                    Console.WriteLine($"record added with id: {id}");

                    command.Parameters["CardType"].Value = "NeverLimits";
                    command.Parameters["CardNumber"].Value = "987654321011";
                    command.Parameters["ExpMonth"].Value = 12;
                    command.Parameters["ExpYear"].Value = 2025;

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
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("config.json");

            IConfiguration config = configurationBuilder.Build();
            string connectionString = config["Data:DefaultConnection:ConnectionString"];
            return connectionString;
        }
    }
}
