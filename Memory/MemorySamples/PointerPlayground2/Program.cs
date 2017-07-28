using System;

namespace PointerPlayground2
{
    public class Program
    {
        unsafe public static void Main()
        {
            Console.WriteLine($"Size of CurrencyStruct struct is {sizeof(CurrencyStruct)}");
            CurrencyStruct amount1, amount2;
            CurrencyStruct* pAmount = &amount1;
            long* pDollars = &(pAmount->Dollars);
            byte* pCents = &(pAmount->Cents);

            Console.WriteLine($"Address of amount1 is 0x{(ulong)&amount1:X}");
            Console.WriteLine($"Address of amount2 is 0x{(ulong)&amount2:X}");
            Console.WriteLine($"Address of pAmount is 0x{(ulong)&pAmount:X}");
            Console.WriteLine($"Address of pDollars is 0x{(ulong)&pDollars:X}");
            Console.WriteLine($"Address of pCents is 0x{(ulong)&pCents:X}");
            pAmount->Dollars = 20;
            *pCents = 50;
            Console.WriteLine($"amount1 contains {amount1}");

            --pAmount;   // this should get it to point to amount2
            Console.WriteLine($"amount2 has address 0x{(ulong)pAmount:X} " +
                $"and contains {*pAmount}");

            // do some clever casting to get pCents to point to cents
            // inside amount2
            CurrencyStruct* pTempCurrency = (CurrencyStruct*)pCents;
            pCents = (byte*)(--pTempCurrency);
            Console.WriteLine("Address of pCents is now 0x{0:X}", (ulong)&pCents);

            Console.WriteLine("\nNow with classes");
            // now try it out with classes
            var amount3 = new CurrencyClass();

            fixed (long* pDollars2 = &(amount3.Dollars))
            fixed (byte* pCents2 = &(amount3.Cents))
            {
                Console.WriteLine($"amount3.Dollars has address 0x{(ulong)pDollars2:X}");
                Console.WriteLine($"amount3.Cents has address 0x{(ulong)pCents2:X}");
                *pDollars2 = -100;
                Console.WriteLine($"amount3 contains {amount3}");
            }
        }
    }
}
