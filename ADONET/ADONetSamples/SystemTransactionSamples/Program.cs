using System;
using System.Threading.Tasks;
using System.Transactions;
using static SystemTransactionSamples.Utilities;

namespace SystemTransactionSamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            switch (args[0])
            {
                case "-c":
                    await CommittableTransactionAsync();
                    break;
                default:
                    ShowUsage();
                    break;
            }
            // AmbientTransactions();
        }

        public static void ShowUsage()
        {
            Console.WriteLine("SystemTransactionSamples command");
            Console.WriteLine("\t-c\tCommittable Transactions");

            Console.WriteLine("\t-c\tUse Configuration File");
            Console.WriteLine("\t-i\tConnection Information");
            Console.WriteLine("\t-t\tTransactions");
        }

        static void AmbientTransactions()
        {
            using (var scope = new TransactionScope())
            {
                Transaction.Current.TransactionCompleted += (sender, e) => 
                    DisplayTransactionInformation("TX completed", e.Transaction.TransactionInformation);

             
                // scope.Complete();
            }
        }



        static async Task CommittableTransactionAsync()
        {
            var tx = new CommittableTransaction();
            DisplayTransactionInformation("TX created", tx.TransactionInformation);

            try
            {
                var b = new Book
                {
                    Title = "A Dog in The House",
                    Publisher = "Pet Show",
                    Isbn = "4711"               
                };
                var data = new BookData();
                await data.AddBookAsync(b, tx);

                if (AbortTx())
                {
                    throw new ApplicationException("transaction abort");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                tx.Rollback();
            }

            DisplayTransactionInformation("TX completed", tx.TransactionInformation);
        }
    }
}
