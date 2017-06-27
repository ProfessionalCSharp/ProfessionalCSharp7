using System;

namespace Wrox.ProCSharp.Delegates
{
    public class Consumer
    {
        private string _name;

        public Consumer(string name) => _name = name;

        public void NewCarIsHere(object sender, CarInfoEventArgs e) =>
          Console.WriteLine($"{_name}: car {e.Car} is new");
    }
}
