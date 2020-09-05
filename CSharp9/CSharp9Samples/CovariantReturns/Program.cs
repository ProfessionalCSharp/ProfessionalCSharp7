using CovariantReturns;
using System;

Shape shape = new Ellipse(20, 20, 100, 50);
Shape shape2 = shape.Clone();
// Ellipse e1 = shape.Clone(); not possible
if (shape2 is Ellipse e)
{
    Console.WriteLine($"it's a ellipse {e}");
}
Rectangle rectangle = new(50, 50, 80, 70);
Rectangle rectangle12 = rectangle.Clone();
