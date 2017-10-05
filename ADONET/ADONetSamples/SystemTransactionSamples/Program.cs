using System;
using System.Linq;
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
                case "-p":
                    await TransactionPromotionsAsync();
                    break;
                case "-d":
                    DependentTransactions();
                    break;
                case "-a":
                    AmbientTransactions();
                    break;
                case "-n":
                    NestedScopes();
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        public static void ShowUsage()
        {
            Console.WriteLine("SystemTransactionSamples command");
            Console.WriteLine("\t-c\tCommittable Transactions");
            Console.WriteLine("\t-p\tPromotable Transactions");
            Console.WriteLine("\t-d\tDepdendent Transactions");
            Console.WriteLine("\t-a\tAmbient Transactions");
            Console.WriteLine("\t-n\tNested Transactions");
        }

        static void NestedScopes()
        {
            using (var scope = new TransactionScope())
            {
                Transaction.Current.TransactionCompleted += (sender, e) =>
                    DisplayTransactionInformation("TX completed", e.Transaction.TransactionInformation);

                DisplayTransactionInformation("Ambient TX created", Transaction.Current.TransactionInformation);

                var b = new Book
                {
                    Title = "Dogs in The House",
                    Publisher = "Pet Show",
                    Isbn = RandomIsbn(),
                    ReleaseDate = new DateTime(2020, 11, 24)
                };
                var data = new BookData();
                data.AddBook(b);

                using (var scope2 = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Transaction.Current.TransactionCompleted += (sender, e) =>
                        DisplayTransactionInformation("Inner TX completed", e.Transaction.TransactionInformation);

                    DisplayTransactionInformation("Inner TX scope", Transaction.Current.TransactionInformation);

                    var b1 = new Book
                    {
                        Title = "Dogs and Cats in The House",
                        Publisher = "Pet Show",
                        Isbn = RandomIsbn(),
                        ReleaseDate = new DateTime(2021, 11, 24)
                    };
                    var data1 = new BookData();
                    data1.AddBook(b1);

                    scope2.Complete();
                }

                scope.Complete();
            }
        }

        static void AmbientTransactions()
        {
            using (var scope = new TransactionScope())
            {
                Transaction.Current.TransactionCompleted += (sender, e) => 
                    DisplayTransactionInformation("TX completed", e.Transaction.TransactionInformation);

                DisplayTransactionInformation("Ambient TX created", Transaction.Current.TransactionInformation);

                var b = new Book
                {
                    Title = "Cats in The House",
                    Publisher = "Pet Show",
                    Isbn = RandomIsbn(),
                    ReleaseDate = new DateTime(2019, 11, 24)
                };
                var data = new BookData();
                data.AddBook(b);

                if (!AbortTx())
                {
                    scope.Complete();
                }
                else
                {
                    Console.WriteLine("transaction abort by the user");
                }

            }  // scope.Dispose();
        }

        static void DependentTransactions()
        {
            async Task UsingDependentTransactionAsync(DependentTransaction dtx)
            {
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
                    throw new ApplicationException("transaction abort by the user");
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
                    Isbn =RandomIsbn(),
                    ReleaseDate = new DateTime(2018, 11, 24)
                };
                var data = new BookData();
                await data.AddBookAsync(b, tx);

                if (AbortTx())
                {
                    throw new ApplicationException("transaction abort by the user");
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
