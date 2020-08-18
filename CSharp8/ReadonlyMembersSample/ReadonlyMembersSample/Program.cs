using System;

namespace ReadonlyMembersSample
{
    public readonly struct ReadonlyData
    {
        public int X { get; }  // read-write struct not allowed
        private readonly int a;
    }

    public struct SomeData
    {
        private readonly int _data4;
        private int _data5;
        public SomeData(int data1, int data2, int data3, int data4, int data5)
        {
            Data1 = data1;
            _data2 = data2;
            Data3 = data3;
            _data4 = 11;
            _data5 = data5;
        }

        public int Data1 { get; set; }
        private int _data2;
        public int Data2
        {
            readonly get => _data2;
            set => _data2 = value;
        }
        public int Data3 { get; }
        private void PrivateMethod() // not declared readonly
        {
            Console.WriteLine("PrivateMethod");
        }

        public readonly int GetData1() => Data1;

        public readonly int GetData2() => Data2;

        public readonly int GetData3() => Data3;
        public readonly int GetData4() => _data4;
        public readonly int GetData5() => _data5;

        public readonly void DontChangeState()
        {
            Console.WriteLine("DontChangeState");
            // PrivateMethod(); - cannot be invoked because this method is not readonly
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sd = new SomeData(11, 12, 13, 14, 15);
            int x = sd.GetData2();
        }
    }
}
