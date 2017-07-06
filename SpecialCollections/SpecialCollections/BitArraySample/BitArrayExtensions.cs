using System.Collections;
using System.Text;

namespace BitArraySample
{
    public static class BitArrayExtensions
    {
        public static string GetBitsFormat(this BitArray bits)
        {
            var sb = new StringBuilder();
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                sb.Append(bits[i] ? 1 : 0);
                if (i != 0 && i % 4 == 0)
                {
                    sb.Append("_");
                }
            }

            return sb.ToString();
        }
    }
}
