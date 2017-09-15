using CalculatorContract;
using System;
using System.Collections.Generic;
using System.Composition;

namespace Wrox.ProCSharp.Composition
{
    [Export(typeof(ICalculator))]
    public class Calculator : ICalculator
    {
        [ImportMany("Add")]
        public Lazy<IBinaryOperation, SpeedMetadata>[] AddMethods { get; set; }

        [Import("Subtract")]
        public IBinaryOperation SubtractMethod { get; set; }

        public IList<IOperation> GetOperations() => new List<IOperation>
        {
            new Operation { Name="+", NumberOperands=2},
            new Operation { Name="-", NumberOperands=2},
            new Operation { Name="/", NumberOperands=2},
            new Operation { Name="*", NumberOperands=2}
        };

        public double Operate(IOperation operation, double[] operands)
        {
            double result = 0;
            switch (operation.Name)
            {
                case "+":
                    foreach (var addMethod in AddMethods)
                    {
                        if (addMethod.Metadata.Speed == Speed.Fast)
                        {
                            result = addMethod.Value.Operation(operands[0], operands[1]);
                        }
                    }
                    //result = operands[0] + operands[1];
                    break;
                case "-":
                    // result = operands[0] - operands[1];
                    result = SubtractMethod.Operation(operands[0], operands[1]);
                    break;
                case "/":
                    result = operands[0] / operands[1];
                    break;
                case "*":
                    result = operands[0] * operands[1];
                    break;
              case "++":
                    result = ++operands[0];
                    break;
                default:
                    throw new InvalidOperationException($"invalid operation {operation.Name}");
            }
            return result;
        }
    }
}
