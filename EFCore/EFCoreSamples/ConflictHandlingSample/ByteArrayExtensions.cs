using System.Text;

namespace ConflictHandlingSample
{
    public static class ByteArrayExtension
    {
        public static string StringOutput(this byte[] data)
        {
            var sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append($"{b}.");
            }
            return sb.ToString();
        }
    }
}
