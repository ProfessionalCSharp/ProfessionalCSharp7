using System.Runtime.CompilerServices;

namespace TuplesLib
{
    public class SimpleMath
    {
        //[return: TupleElementNames(new string[] {"result", "remainder" })]
        public static (int result, int remainder) Divide(int dividend, int divisor)
        {
            int result = dividend / divisor;
            int remainder = dividend % divisor;
            return (result, remainder);
        }
    }
}
