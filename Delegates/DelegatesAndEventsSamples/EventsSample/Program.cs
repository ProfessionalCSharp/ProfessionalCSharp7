namespace Wrox.ProCSharp.Delegates
{
    class Program
    {
        static void Main()
        {
            var dealer = new CarDealer();

            var valtteri = new Consumer("Valtteri");
            dealer.NewCarInfo += valtteri.NewCarIsHere;

            dealer.NewCar("Williams");

            var max = new Consumer("Max");
            dealer.NewCarInfo += max.NewCarIsHere;

            dealer.NewCar("Mercedes");

            dealer.NewCarInfo -= valtteri.NewCarIsHere;

            dealer.NewCar("Ferrari");
        }
    }
}
