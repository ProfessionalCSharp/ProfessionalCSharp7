using System;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq;
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
                case "-p":
                    await TransactionPromotionsAsync();
                    break;
                case "-d":
                    DependentTransactions();
                    break;
                case "-a":
                    AmbientTransactions();
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
            Console.WriteLine("\t-p\tPromotable Transactions");
            Console.WriteLine("\t-d\tDepdendent Transactions");
            Console.WriteLine("\t-a\tAmbient Transactions");
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

        static void DependentTransactions()
        {
            async Task UsingDependentTransactionAsync(object obj)
            {
                var dtx = obj as DependentTransaction;
                dtx.TransactionCompleted += (sender, e) => 
                    DisplayTransactionInformation("Depdendent TX completed", e.Transaction.TransactionInformation);

                DisplayTransactionInformation("Dependent Tx", dtx.TransactionInformation);

                await Task.Delay(2000);

                dtx.Complete();
                DisplayTransactionInformation("Dependent Tx send complete", dtx.TransactionInformation);
            }

            var tx = new CommittableTransaction();
            DisplayTransactionInformation("Root Tx created", tx.TransactionInformation);

            try
            {
                DependentTransaction depTx = tx.DependentClone(DependentCloneOption.BlockCommitUntilComplete);
                Task t1 = Task.Run(() => UsingDependentTransactionAsync(depTx));

                if (AbortTx())
                {
                    throw new ApplicationException("transaction abort by the user");
                }

                tx.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tx.Rollback();
            }

            DisplayTransactionInformation("TX completed", tx.TransactionInformation);
        }

        static async Task TransactionPromotionsAsync()
        {
            var tx = new CommittableTransaction();
            DisplayTransactionInformation("TX created", tx.TransactionInformation);

            try
            {
                var b = new Book
                {
                    Title = "A Cat in The House",
                    Publisher = "Pet Show",
                    Isbn = string.Join("", Guid.NewGuid().ToString().ToCharArray().Take(15)),
                    ReleaseDate = new DateTime(2019, 11, 24)
                };
                var data = new BookData();
                await data.AddBookAsync(b, tx);

                DisplayTransactionInformation("First Connection", tx.TransactionInformation);

                var b2 = new Book
                {
                    Title = "A Rabbit in The House",
                    Publisher = "Pet Show",
                    Isbn = string.Join("", Guid.NewGuid().ToString().ToCharArray().Take(15)),
                    ReleaseDate = new DateTime(2020, 11, 24)
                };
                var data2 = new BookData();
                await data2.AddBookAsync(b2, tx);

                DisplayTransactionInformation("Second Connection", tx.TransactionInformation);

                if (AbortTx())
                {
                    throw new ApplicationException("transaction abort");
                }
                tx.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                tx.Rollback();
            }

            DisplayTransactionInformation("TX completed", tx.TransactionInformation);
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
                    Isbn = string.Join("", Guid.NewGuid().ToString().ToCharArray().Take(15)),
                    ReleaseDate = new DateTime(2018, 11, 24)
                };
                var data = new BookData();
                await data.AddBookAsync(b, tx);

                if (AbortTx())
                {
                    throw new ApplicationException("transaction abort");
                }
                tx.Commit();
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
