using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CommandSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            switch (args[0])
            {
                case "-nq":
                    ExecuteNonQuery();
                    break;
                case "-s":
                    ExecuteScalar();
                    break;
                case "-r":
                    ExecuteReader("Professional C#");
                    break;
                case "-sp":
                    StoredProcedure("Wrox Press");
                    break;
                default:
                    ShowUsage();
                    break;
            }

            Console.ReadLine();
        }

        public static void ShowUsage()
        {
            Console.WriteLine("ConnectionSamples command");
            Console.WriteLine("\t-nq\tExecute Non-Query");
            Console.WriteLine("\t-s\tExecute Scalar");
            Console.WriteLine("\t-r\tExecute Reader");
            Console.WriteLine("\t-sp\tStored Procedure");
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

        private static void StoredProcedure(string publisher)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[ProCSharp].[GetBooksByPublisher]";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter p1 = command.CreateParameter();
                p1.SqlDbType = SqlDbType.NVarChar;
                p1.ParameterName = "@Publisher";
                p1.Value = publisher;
                command.Parameters.Add(p1);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string title = (string)reader["Title"];
                        string pub = (string)reader["Publisher"];
                        DateTime releaseDate = (DateTime)reader["ReleaseDate"];
                        Console.WriteLine($"{title} - {pub}; {releaseDate:d}");
                    }
                }
            }
        }

        private static void ExecuteScalar()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT COUNT(*) FROM [ProCSharp].[Books]";
                SqlCommand command = connection.CreateCommand();
                command.CommandText = sql;
                connection.Open();
                object count = command.ExecuteScalar();
                Console.WriteLine($"counted {count} book records");
            }
        }

        public static void CreateCommand()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books]";
                var command = new SqlCommand(sql, connection);

                //SqlCommand command2 = connection.CreateCommand();
                //command2.CommandText = sql;
                //command2.CommandType = CommandType.Text;

                connection.Open();

                // ...

                SqlDataReader reader = command.ExecuteReader();
            }
        }

        public static void CreateCommandWithParameters()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books] WHERE lower([Title]) LIKE @TitleStart";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("TitleStart", "Professional C#");
                command.Parameters.Add("TitleStart", SqlDbType.NVarChar, 50);
                command.Parameters["TitleStart"].Value = "Professional C#";

                //SqlCommand command2 = connection.CreateCommand();
                //command2.CommandText = sql;
                //command2.CommandType = CommandType.Text;

                connection.Open();

                // ...

                SqlDataReader reader = command.ExecuteReader();
            }
        }

        public static void ExecuteCommand()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books] WHERE lower([Title]) LIKE @TitleStart ORDER BY [ReleaseDate]";
                var command = new SqlCommand(sql, connection);
                var parameter = new SqlParameter("TitleStart", SqlDbType.NVarChar, 50)
                {
                    Value = "Professional C#"
                };
                command.Parameters.Add(parameter);

                //SqlCommand command2 = connection.CreateCommand();
                //command2.CommandText = sql;
                //command2.CommandType = CommandType.Text;

                connection.Open();

                // ...

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var s = reader[0];
                        Console.WriteLine(s);
                        //Console.WriteLine($"{reader.GetString(1)} {reader.GetString(2) ?? String.Empty}");
                    }
                }
            }
        }

        public static void ExecuteNonQuery()
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    string sql = "INSERT INTO [ProCSharp].[Books] ([Title], [Publisher], [Isbn], [ReleaseDate]) " +
                        "VALUES (@Title, @Publisher, @Isbn, @ReleaseDate)";

                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("Title", "Professional C# 7 and .NET Core 2.0");
                    command.Parameters.AddWithValue("Publisher", "Wrox Press");
                    command.Parameters.AddWithValue("Isbn", " 978-1119449270");
                    command.Parameters.AddWithValue("ReleaseDate", new DateTime(2018, 4, 2));

                    connection.Open();
                    int records = command.ExecuteNonQuery();
                    Console.WriteLine($"{records} record(s) inserted");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ExecuteReader(string titleParameter)
        {
            var connection = new SqlConnection(GetConnectionString());

            string sql = "SELECT [Id], [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books] WHERE lower([Title]) LIKE @TitleStart ORDER BY [ReleaseDate]";
            var command = new SqlCommand(sql, connection);
            var parameter = new SqlParameter("TitleStart", SqlDbType.NVarChar, 50)
            {
                Value = $"{titleParameter}%"
                
            };
            command.Parameters.Add(parameter);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string publisher = reader[2].ToString();
                    DateTime releaseDate = reader.GetDateTime(3);
                    Console.WriteLine($"{title,-40} {publisher,-15} {releaseDate:d}");
                }
            }
        }

        //public static void ExecuteReader(int productId)
        //{
        //    string GetProductInformationSQL() =>
        //       "SELECT Prod.ProductID, Prod.Name, Prod.StandardCost, Prod.ListPrice, CostHistory.StartDate, CostHistory.EndDate, CostHistory.StandardCost " +
        //           "FROM Production.ProductCostHistory AS CostHistory  " +
        //           "INNER JOIN Production.Product AS Prod ON CostHistory.ProductId = Prod.ProductId " +
        //             "WHERE Prod.ProductId = @ProductId";

        //    var connection = new SqlConnection(GetConnectionString());

        //    string sql = GetProductInformationSQL();
        //    var command = new SqlCommand(sql, connection);
        //    var productIdParameter = new SqlParameter("ProductId", SqlDbType.Int);
        //    productIdParameter.Value = productId;
        //    command.Parameters.Add(productIdParameter);

        //    connection.Open();

        //    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
        //    {
        //        while (reader.Read())
        //        {
        //            int id = reader.GetInt32(0);
        //            string name = reader.GetString(1);
        //            DateTime from = reader.GetDateTime(4);
        //            DateTime? to = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
        //            decimal standardPrice = reader.GetDecimal(6);
        //            Console.WriteLine($"{id} {name} from: {from:d} to: {to:d}; price: {standardPrice}");
        //        }
        //    }
        //}
    }
}
