using System.Collections.Generic;

namespace Wrox.ProCSharp.Composition
{
    public interface ICalculator
    {
        IList<IOperation> GetOperations();
        double Operate(IOperation operation, double[] operands);
    }
}
