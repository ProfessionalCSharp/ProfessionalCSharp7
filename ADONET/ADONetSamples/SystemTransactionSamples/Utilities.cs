using System;
using System.Linq;
using System.Transactions;

namespace SystemTransactionSamples
{
    public class Utilities
    {
        public static bool AbortTx()
        {
            Console.WriteLine("Abort the transaction (y/n)?");
            return Console.ReadLine().ToLower().Equals("y");
        }

        public static void DisplayTransactionInformation(string title, TransactionInformation info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            Console.WriteLine(title);
            Console.WriteLine($"Creation time: {info.CreationTime:T}");
            Console.WriteLine($"Status: {info.Status}");
            Console.WriteLine($"Local Id: {info.LocalIdentifier}");
            Console.WriteLine($"Distributed Id: {info.DistributedIdentifier}");
            Console.WriteLine();
        }

        public static string RandomIsbn() =>
            string.Join("", Guid.NewGuid().ToString().ToCharArray().Take(15));
    }
}
