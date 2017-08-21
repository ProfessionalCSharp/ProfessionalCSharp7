namespace NoDI
{
    public class HomeController
    {
        public string Hello(string name)
        {
            var service = new GreetingService();
            return service.Greet(name);
        }
    }
}
