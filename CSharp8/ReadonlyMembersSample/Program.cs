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
        private readonly int _data1;
        private int _data2;
        public SomeData(int data1, int data2, int data3, int data4, int data5)
        {
            _data1 = data1;
            _data2 = data2;
            _data3 = data3;
            Data4 = data4;
            Data5 = data5;
        }

        private int _data3;
        public int Data3
        {
            readonly get => _data3;
            set => _data3 = value;
        }
        public int Data4 { get; set; }

        public int Data5 { get; }
        private void PrivateMethod() // not declared readonly
        {
            Console.WriteLine("PrivateMethod");
        }
        public readonly int GetData1() => _data1;
        public readonly int GetData2() => _data2;

        public readonly int GetData3() => Data3;

        public readonly int GetData4() => Data4;

        public readonly int GetData5() => Data5;

        public readonly void DontChangeState()
        {
            Console.WriteLine("DontChangeState");
            // PrivateMethod(); - cannot be invoked because this method is not readonly
        }
    }

    class Program
    {
        static void Main()
        {
            var sd = new SomeData(11, 12, 13, 14, 15);
            int x = sd.GetData4();
        }
    }
}
