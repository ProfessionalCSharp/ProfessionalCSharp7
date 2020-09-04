using InitOnlyProps;
using System;

var book = new Book { Title = "Professional C# 9", Publisher = "Wrox Press" };
Console.WriteLine($"{book.Title} {book.Publisher}");
