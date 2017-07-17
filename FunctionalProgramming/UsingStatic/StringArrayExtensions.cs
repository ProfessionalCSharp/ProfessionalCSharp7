using System;

namespace UsingStatic
{
    public static class StringArrayExtensions
    {
        public static void ToStrings(this string[] values, out string value1, out string value2)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Length != 2) throw new IndexOutOfRangeException("only arrays with 2 values allowed");
            value1 = values[0];
            value2 = values[1];
        }
    }
}
