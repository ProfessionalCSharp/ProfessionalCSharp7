using System;

namespace ReferenceSemantics
{
    public class Container
    {
        public Container(int[] data) => _data = data;
        private int[] _data;

        public ref int[] GetData() => ref _data;

        public ref int GetItem(int index) => ref _data[index];

        public void ShowAll()
        {
            Console.WriteLine(string.Join(", ", _data));
            Console.WriteLine();
        }
    }
}
