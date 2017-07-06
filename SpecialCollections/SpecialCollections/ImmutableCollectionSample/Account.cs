using System;

namespace ImmutableCollectionSample
{
    public class Account
    {
        public Account(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }
        public string Name { get; }
        public decimal Amount { get; }
    }
}