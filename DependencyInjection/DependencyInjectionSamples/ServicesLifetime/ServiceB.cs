using System;

namespace ServicesLifetime
{
    public class ServiceB : IServiceB, IDisposable
    {
        private readonly int _n;
        public ServiceB(INumberService numberService)
        {
            _n = numberService.GetNumber();
            Console.WriteLine($"ctor {nameof(ServiceB)}, {_n}");
        }

        public void B() => Console.WriteLine($"{nameof(B)}, {_n}");
        public void Dispose() => Console.WriteLine($"disposing {nameof(ServiceB)}, {_n}");
    }
}
