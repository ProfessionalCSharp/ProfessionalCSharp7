using System;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace ConnectionSamples
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
                case "-o":
                    OpenConnection();
                    break;
                case "-c":
                    ConnectionUsingConfig();
                    break;
                case "-i":
                    ConnectionInformation();
                    break;
                case "-t":
                    Transactions();
                    break;
                default:
                    ShowUsage();
                    break;
            }
            Console.WriteLine("completed");

            Console.ReadLine();
        }

        public static void ShowUsage()
        {
            Console.WriteLine("ConnectionSamples command");
            Console.WriteLine("\t-o\tOpen Connection");
            Console.WriteLine("\t-c\tUse Configuration File");
            Console.WriteLine("\t-i\tConnection Information");
            Console.WriteLine("\t-t\tTransactions");
        }

        public static void OpenConnection()
        {
            string connectionString = @"server=(localdb)\MSSQLLocalDB;" +
                            "integrated security=SSPI;" +
                            "database=Books";
            var connection = new SqlConnection(connectionString);

            connection.Open();

            // Do something useful
            Console.WriteLine("connection opened");

            connection.Close();
        }

        public static void ConnectionUsingConfig()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");
            IConfiguration config = configurationBuilder.Build();
         
            string connectionString = config["Data:DefaultConnection:ConnectionString"];
            Console.WriteLine(connectionString);
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

        public static void ConnectionInformation()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.InfoMessage += (sender, e) =>
                {
                    Console.WriteLine($"warning or info: {e.Message}");
                };
                connection.StateChange += (sender, e) =>
                {
                    Console.WriteLine($"current state: {e.CurrentState}, before: {e.OriginalState}");
                };

                try
                {
                    connection.StatisticsEnabled = true;
                    connection.FireInfoMessageEventOnUserErrors = true;
                    connection.Open();

                    Console.WriteLine("connection opened");

                    // Do something useful
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT Titl, Publisher FROM [ProCSharp].[Books]";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetString(0)} {reader.GetString(1)}");
                    }
                    IDictionary statistics = connection.RetrieveStatistics();
                    ShowStatistics(statistics);
                    connection.ResetStatistics();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void ShowStatistics(IDictionary statistics)
        {
            Console.WriteLine("Statistics");
            foreach (var key in statistics.Keys)
            {
                Console.WriteLine($"{key}, value: {statistics[key]}");
            }
            Console.WriteLine();
        }

        public static void Transactions()
        {
            string connectionString = GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction tx = connection.BeginTransaction();
                tx.Save("one");

                tx.Commit();
            }
        }
    }
}
