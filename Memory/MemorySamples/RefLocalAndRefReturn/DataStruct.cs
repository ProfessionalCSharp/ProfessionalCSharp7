using System;
using System.Collections.Generic;
using System.Text;

namespace RefLocalAndRefReturn
{
  

    public struct DataStruct
    {
        public DataStruct(int[] arr)
        {
            _data = arr;
        }
        private int[] _data;

        public ref int[] GetEmbedded1(ref int[] d) => ref d;

        public ref int GetEmbedded2(ref int[] d, int index) => ref d[index];
    }
}
