using System.Collections.Generic;

namespace Wrox.ProCSharp.Arrays
{
    public class MusicTitles
    {
        private string[] _names = {
              "Tubular Bells", "Hergest Ridge",
              "Ommadawn", "Platinum" };

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return _names[i];
            }
        }

        public IEnumerable<string> Reverse()
        {
            for (int i = 3; i >= 0; i--)
            {
                yield return _names[i];
            }
        }

        public IEnumerable<string> Subset(int index, int length)
        {
            for (int i = index; i < index + length; i++)
            {
                yield return _names[i];
            }
        }
    }
}
