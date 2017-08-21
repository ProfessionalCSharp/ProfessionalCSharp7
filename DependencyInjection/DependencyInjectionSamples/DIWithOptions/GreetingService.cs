using Microsoft.Extensions.Options;

namespace DIWithOptions
{
    public class GreetingService : IGreetingService
    {
        public GreetingService(IOptions<GreetingServiceOptions> options) =>
            _from = options.Value.From;

        private readonly string _from;

        public string Greet(string name) => $"Hello, {name}! Greetings from {_from}";
    }
}
