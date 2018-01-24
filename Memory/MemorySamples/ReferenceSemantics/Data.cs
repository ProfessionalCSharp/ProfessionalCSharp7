using System;

namespace ReferenceSemantics
{
    public class Data
    {
        public Data(int anumber) => _anumber = anumber;

        private int _anumber;

        public ref int GetNumber() => ref _anumber;

        public ref readonly int GetReadonlyNumber() => ref _anumber;

        public void Show() => Console.WriteLine($"Data: {_anumber}"); 

        public void InNumber(in int anumber)  // pass by reference, but readonly
        {
            // anumber = 11; readonly - cannot be changed!
        }
    }
}
