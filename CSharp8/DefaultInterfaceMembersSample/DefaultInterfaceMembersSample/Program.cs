using SampleLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DefaultInterfaceMembersSample
{
    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }        
    }

    public class Shape : IShape
    {
        private IPosition _position = new Position();
        public IPosition Position
        {
            get => _position;
            set => _position = value;
        }

        public IPosition Move(IPosition newPosition)
        {
            Console.WriteLine("Shape implementation");
            _position.X = newPosition.X;
            _position.Y = newPosition.Y;
            return newPosition;
        }

        #region future feature
        // https://github.com/dotnet/docs/issues/14127
        // base call coming in C# 8.x
        //public virtual IPosition Move(IPosition newPosition)
        //{
        //    var newpos = base(IShape).Move(newPosition);
        //    Console.WriteLine("Move called");
        //    return newpos;
        //}
        #endregion

        public override string ToString() => $"X: {Position.X}, Y: {Position.Y}";
    }

    class Program
    {
        static void Move(IShape shape)
        {
            shape.Move(new Position() { X = 99, Y = 99 });
        }

        static void Main()
        {
            CustomCollectionSample();

            IShape shape = new Shape();
            shape.Position = new Position { X = 33, Y = 22 };
            Console.WriteLine(shape);
            shape.Move(new Position { X = 44, Y = 33 });

            Move(shape);
            Console.WriteLine(shape);

             UseADifferentBindableBase();
        }

        private static ICustomEnumerable<string> GetCustomCollection() =>
            new CustomCollection<string> { "James", "John", "Michael", "Lewis", "Jochen", "Juan" };

        private static void CustomCollectionSample()
        {
            var coll = GetCustomCollection();
            
            var subset = coll.Where(n => n.StartsWith("J"));
            foreach (var name in subset)
            {
                Console.WriteLine(name);
            }

            var subset2 = from n in coll
                          where n.StartsWith("J")
                          select n;
            foreach (var name in subset2)
            {
                Console.WriteLine(name);
            }
        }

        #region BindableBase
        private static void UseADifferentBindableBase()
        {
            Book book = new Book() { Id = 1, Title = "One" };
            book.PropertyChanged += (sender, e) =>
            {
                Console.WriteLine($"property changed for {e.PropertyName}");
                if (sender is Book b)
                {
                    Console.WriteLine($"new value: {b.Title}");
                }
            };

            book.Title = "Two";
        }
        #endregion
    }
}
