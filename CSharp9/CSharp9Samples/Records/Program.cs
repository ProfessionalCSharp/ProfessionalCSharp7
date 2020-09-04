using Records;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

var b1 = new Book("Professional C# 7", "Wrox Press");
Console.WriteLine(b1.Title);

var b2 = new Book("Professional C# 7", "Wrox Press") { Isbn = "987-1-119-44927-0" };

// deconstruct

(var title, _, _) = b1;
Console.WriteLine(title);

var b3 = new Book("Professional C# 7", "Wrox Press");
if (!object.ReferenceEquals(b1, b3))
{
    Console.WriteLine("not the same instance...");
}
if (b1 == b3)
{
    Console.WriteLine("but the same values");
}

// with expressions

var b4 = b1 with { Title = "Professional C# 9" };

var b5 = new Book(b4);

Console.WriteLine($"{b4.Title} {b4.Publisher}");

var rect = new Rectangle(new Position(10, 10), 200, 100);
Console.WriteLine(rect);

var x = new Test();
var x2 = x with { Foo = 42 };


class Test
{
    public Test()
    {

    }
    public Test(Test original)
    {
        Foo = original.Foo;
    }
    public int Foo { get; init; }
}

