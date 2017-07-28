using System;

namespace RefLocalAndRefReturn
{
    public class Data
    {
        public Data(int anumber) => _anumber = anumber;

        private int _anumber;

        public ref int GetNumber() => ref _anumber;

        public void Show() => Console.WriteLine($"Data: {_anumber}");

        
    }
}
