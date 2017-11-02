using CalculatorContract;
using System.Composition;
using System.Threading.Tasks;

namespace Wrox.ProCSharp.Composition
{
    [Export("Add", typeof(IBinaryOperation))]
    [SpeedMetadata(Speed = Speed.Fast)]
    public class AddOperation : IBinaryOperation
    {
        public double Operation(double x, double y) => x + y;
    }

    [Export("Subtract", typeof(IBinaryOperation))]
    public class SubtractOperation : IBinaryOperation
    {
        public double Operation(double x, double y) => x - y;
    }

    [Export("Add", typeof(IBinaryOperation))]
    [SpeedMetadata(Speed = Speed.Slow)]
    public class SlowAddOperation : IBinaryOperation
    {
        public double Operation(double x, double y)
        {
            Task.Delay(3000).Wait();
            return x + y;
        }
    }
}
