namespace CalculatorLib
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Calculator
    {
        public double Add(double x, double y) => x + y;
        public double Subtract(double x, double y) => x - y;
    }
}
