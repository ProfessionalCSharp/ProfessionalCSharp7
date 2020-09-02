using System;

namespace TraitsSample
{
    public interface IAdd<T>
        where T : struct
    {
        T Add(T x, T y);
    }

    public interface ISubtract<T>
        where T : struct
    {
        T Subtract(T x, T y);
    }

    public interface IIntAdd : IAdd<int>
    {
        int IAdd<int>.Add(int x, int y) => x + y;
    }

    public interface IIntSubtract : ISubtract<int>
    {
        int ISubtract<int>.Subtract(int x, int y) => x - y;
    }

    public class Calculator : IIntAdd, IIntSubtract
    {
        // no implementation!!!
    }

    class Program
    {
        static void Main()
        {
            var calc = new Calculator();
            if (calc is IIntSubtract sub)
            {
                Console.WriteLine(sub.Subtract(48, 6));
            }

            CalcWithAdd(calc);
            CalcWithSubtract(calc);            
        }

        static void CalcWithAdd(IIntAdd add)
        {
            Console.WriteLine(add.Add(38, 4));
        }

        static void CalcWithSubtract(IIntSubtract subtract)
        {
            Console.WriteLine(subtract.Subtract(46, 4));
        }
    }
}
