using System;
using System.Linq;

namespace BitVectorSample
{
    public static class BinaryExtensions
    {
        public static string AddSeparators(this string number) =>
            number.Length <= 4 ? number :
                string.Join("_",
                    Enumerable.Range(0, number.Length / 4)
                        .Select(i => number.Substring(startIndex: i * 4, length: 4)).ToArray());

        public static string ToBinaryString(this int number) =>
            Convert.ToString(number, toBase: 2).AddSeparators();
    }
}
