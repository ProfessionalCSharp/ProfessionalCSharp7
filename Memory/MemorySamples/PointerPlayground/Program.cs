using System;

namespace PointerPlayground
{
    class Program
    {
        unsafe static void Main()
        {
            int x = 10;
            short y = -1;
            byte y2 = 4;
            double z = 1.5;
            int* pX = &x;
            short* pY = &y;
            double* pZ = &z;

            Console.WriteLine($"Address of x is 0x{(ulong)&x:X}, size is {sizeof(int)}, value is {x}");
            Console.WriteLine($"Address of y is 0x{(ulong)&y:X}, size is {sizeof(short)}, value is {y}");
            Console.WriteLine($"Address of y2 is 0x{(ulong)&y2:X}, size is {sizeof(byte)}, value is {y2}");
            Console.WriteLine($"Address of z is 0x{(ulong)&z:X}, size is {sizeof(double)}, value is {z}");
            Console.WriteLine($"Address of pX=&x is 0x{(ulong)&pX:X}, size is {sizeof(int*)}, value is 0x{(ulong)pX:X}");
            Console.WriteLine($"Address of pY=&y is 0x{(ulong)&pY:X}, size is {sizeof(short*)}, value is 0x{(ulong)pY:X}");
            Console.WriteLine($"Address of pZ=&z is 0x{(ulong) &pZ:X}, size is {sizeof(double*)}, value is 0x{(ulong)pZ:X}");
            *pX = 20;
            Console.WriteLine($"After setting *pX, x = {x}");
            Console.WriteLine($"*pX = {*pX}");

            pZ = (double*)pX;
            Console.WriteLine($"x treated as a double = {*pZ}");

            Console.ReadLine();
        }
    }
}