using System;
using System.Collections.Generic;

namespace Wrox.ProCSharp.Generics
{
    public static class Algorithms
    {
        public static decimal AccumulateSimple(IEnumerable<Account> source)
        {
            decimal sum = 0;
            foreach (Account a in source)
            {
                sum += a.Balance;
            }
            return sum;
        }

        public static decimal Accumulate<TAccount>(IEnumerable<TAccount> source)
            where TAccount : IAccount
        {
            decimal sum = 0;

            foreach (TAccount a in source)
            {
                sum += a.Balance;
            }
            return sum;
        }

        public static T2 Accumulate<T1, T2>(IEnumerable<T1> source, Func<T1, T2, T2> action)
        {
            // TODO: update to C# 7.1 syntax
            T2 sum = default(T2);
            foreach (T1 item in source)
            {
                sum = action(item, sum);
            }
            return sum;
        }
    }
}
