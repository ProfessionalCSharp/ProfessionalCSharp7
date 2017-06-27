namespace Wrox.ProCSharp.Delegates
{
    class Program
    {
        static void Main()
        {
            var dealer = new CarDealer();

            var daniel = new Consumer("Daniel");
            dealer.NewCarInfo += daniel.NewCarIsHere;

            dealer.NewCar("Mercedes");

            var sebastian = new Consumer("Sebastian");
            dealer.NewCarInfo += sebastian.NewCarIsHere;

            dealer.NewCar("Ferrari");

            dealer.NewCarInfo -= sebastian.NewCarIsHere;

            dealer.NewCar("Red Bull Racing");
        }
    }
}
