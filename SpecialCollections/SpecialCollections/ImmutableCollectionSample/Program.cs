using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ImmutableCollectionSample
{
    class Program
    {
        static void Main()
        {
            SimpleArrayDemo();
            ImmutableList<Account> accounts = CreateImmutableList();
            UsingABuilder(accounts);
            LinqDemo();

            Console.ReadLine();
        }

        public static void LinqDemo()
        {
            ImmutableArray<string> arr = ImmutableArray.Create<string>("one", "two", "three", "four", "five");
            var result = arr.Where(s => s.StartsWith("t"));
        }

        public static void UsingABuilder(ImmutableList<Account> immutableAccounts)
        {
            ImmutableList<Account>.Builder builder = immutableAccounts.ToBuilder();
            for (int i = 0; i < builder.Count; i++)
            {
                Account a = builder[i];
                if (a.Amount > 0)
                {
                    builder.Remove(a);
                }
            }

            ImmutableList<Account> overdrawnAccounts = builder.ToImmutable();

            overdrawnAccounts.ForEach(a => Console.WriteLine($"{a.Name} {a.Amount}"));
        }

        public static ImmutableList<Account> CreateImmutableList()
        {
            var accounts = new List<Account>() {
                new Account("Scrooge McDuck", 667377678765m),
                new Account("Donald Duck", -200m),
                new Account("Ludwig von Drake", 20000m)
            };

            ImmutableList<Account> immutableAccounts = accounts.ToImmutableList();

            foreach (var account in immutableAccounts)
            {
                Console.WriteLine($"{account.Name} {account.Amount}");
            }

            immutableAccounts.ForEach(a => Console.WriteLine($"{a.Name} {a.Amount}"));

            return immutableAccounts;
        }

        public static void SimpleArrayDemo()
        {
            ImmutableArray<string> a1 = ImmutableArray.Create<string>();
            ImmutableArray<string> a2 = a1.Add("Williams");
            ImmutableArray<string> a3 =
                a2.Add("Ferrari").Add("Mercedes").Add("Red Bull Racing");
        }
    }
}