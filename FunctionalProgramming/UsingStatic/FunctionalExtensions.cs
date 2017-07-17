using System;

namespace UsingStatic
{
    public static class FunctionalExtensions
    {
        public static void Use<T>(this T item, Action<T> action)
            where T : IDisposable
        {
            using (item)
            {
                action(item);
            }
        }

        public static Func<T1, TResult> Compose<T1, T2, TResult>(Func<T1, T2> f1, 
            Func<T2, TResult> f2) =>
            a => f2(f1(a));
    }
}
