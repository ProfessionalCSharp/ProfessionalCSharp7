using System;
using System.Transactions;

namespace TransactionScopeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample1();
        }

        private static void Sample1()
        {
            using (var scope = new TransactionScope())
            {
                scope.Complete();
            }
        }
    }
}
