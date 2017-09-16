using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

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
                    ExecuteReader(717);
                    break;
                case "-sp":
                    StoredProcedure(251);
                    break;
                default:
                    ShowUsage();
                    break;
            }


            Console.ReadLine();
        }

        private static void StoredProcedure(int entityId)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[uspGetEmployeeManagers]";
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = command.CreateParameter();
                p1.SqlDbType = SqlDbType.Int;
                p1.ParameterName = "@BusinessEntityID";
                p1.Value = entityId;
                command.Parameters.Add(p1);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int recursionLevel = (int)reader["RecursionLevel"];
                        int businessEntityId = (int)reader["BusinessEntityID"];
                        string firstName = (string)reader["FirstName"];
                        string lastName = (string)reader["LastName"];
                        Console.WriteLine($"{recursionLevel} {businessEntityId} {firstName} {lastName}");
                    }
                }
            }
        }

        private static void ExecuteScalar()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT COUNT(*) FROM Production.Product";
                SqlCommand command = connection.CreateCommand();
                command.CommandText = sql;
                connection.Open();
                object count = command.ExecuteScalar();
                Console.WriteLine($"counted {count} product records");
            }
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
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("config.json");

            IConfiguration config = configurationBuilder.Build();
            string connectionString = config["Data:DefaultConnection:ConnectionString"];
            return connectionString;
        }

        public static void CreateCommand()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT BusinessEntityID, FirstName, MiddleName, LastName FROM Person.Person";
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
                string sql = "SELECT BusinessEntityID, FirstName, MiddleName, LastName FROM Person.Person WHERE EmailPromotion = @EmailPromotion";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("EmailPromotion", 1);
                command.Parameters.Add("EmailPromotion", SqlDbType.Int);
                command.Parameters["EmailPromotion"].Value = 1;

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
                string sql = "SELECT BusinessEntityID, FirstName, MiddleName, LastName FROM Person.Person WHERE EmailPromotion = @EmailPromotion";
                var command = new SqlCommand(sql, connection);
                var emailPromotion = new SqlParameter("EmailPromotion", SqlDbType.Int);
                emailPromotion.Value = 1;
                command.Parameters.Add(emailPromotion);

                //SqlCommand command2 = connection.CreateCommand();
                //command2.CommandText = sql;
                //command2.CommandType = CommandType.Text;

                connection.Open();

                // ...

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var s = reader[2];
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
                    string sql = "INSERT INTO [Sales].[SalesTerritory] ([Name], [CountryRegionCode], [Group]) " +
                        "VALUES (@Name, @CountryRegionCode, @Group)";

                    var command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("Name", "Austria");
                    command.Parameters.AddWithValue("CountryRegionCode", "AT");
                    command.Parameters.AddWithValue("Group", "Europe");

                    connection.Open();
                    int records = command.ExecuteNonQuery();
                    Console.WriteLine($"{records} inserted");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ExecuteReader()
        {
            var connection = new SqlConnection(GetConnectionString());

            string sql = "SELECT BusinessEntityID, FirstName, MiddleName, LastName FROM Person.Person WHERE EmailPromotion = @EmailPromotion";
            var command = new SqlCommand(sql, connection);
            var emailPromotion = new SqlParameter("EmailPromotion", SqlDbType.Int);
            emailPromotion.Value = 1;
            command.Parameters.Add(emailPromotion);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string middleName = reader[2].ToString();
                    string lastName = reader.GetString(3);
                    Console.WriteLine($"{id} {firstName:-20} {middleName} {lastName:-20}");
                }
            }
        }

        private static string GetProductInformationSQL() =>
            "SELECT Prod.ProductID, Prod.Name, Prod.StandardCost, Prod.ListPrice, CostHistory.StartDate, CostHistory.EndDate, CostHistory.StandardCost " +
                "FROM Production.ProductCostHistory AS CostHistory  " +
                "INNER JOIN Production.Product AS Prod ON CostHistory.ProductId = Prod.ProductId " +
                  "WHERE Prod.ProductId = @ProductId";


        public static void ExecuteReader(int productId)
        {
            var connection = new SqlConnection(GetConnectionString());

            string sql = GetProductInformationSQL();
            var command = new SqlCommand(sql, connection);
            var productIdParameter = new SqlParameter("ProductId", SqlDbType.Int);
            productIdParameter.Value = productId;
            command.Parameters.Add(productIdParameter);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
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
    }
}
