using System;

namespace DIWithAutofac
{
    public class HomeController
    {
        private readonly IGreetingService _greetingService;
        public HomeController(IGreetingService greetingService)
        {
            _greetingService = greetingService ?? throw new ArgumentNullException(nameof(greetingService));
        }
        public string Hello(string name) =>
            _greetingService.Greet(name);
    }
}
