using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;

namespace SystemTransactionSamples
{
    public class BookData
    {
        public async Task AddBookAsync(Book book, Transaction tx)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                string sql = "INSERT INTO [ProCSharp].[Books] ([Title], [Publisher], [Isbn], [ReleaseDate]) " +
                    "VALUES (@Title, @Publisher, @Isbn, @ReleaseDate)";

                await connection.OpenAsync();
                if (tx != null)
                {
                    connection.EnlistTransaction(tx);
                }
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("Title", book.Title);
                command.Parameters.AddWithValue("Publisher", book.Publisher);
                command.Parameters.AddWithValue("Isbn", book.Isbn);
                command.Parameters.AddWithValue("ReleaseDate", book.ReleaseDate);

                await command.ExecuteNonQueryAsync();
            }
        }

        public void AddBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                string sql = "INSERT INTO [ProCSharp].[Books] ([Title], [Publisher], [Isbn], [ReleaseDate]) " +
                    "VALUES (@Title, @Publisher, @Isbn, @ReleaseDate)";

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("Title", book.Title);
                command.Parameters.AddWithValue("Publisher", book.Publisher);
                command.Parameters.AddWithValue("Isbn", book.Isbn);
                command.Parameters.AddWithValue("ReleaseDate", book.ReleaseDate);

                command.ExecuteNonQuery();
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
