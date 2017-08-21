using System;

namespace ServicesLifetime
{
    public class ControllerX : IDisposable
    {
        private readonly IServiceA _serviceA;
        private readonly IServiceB _serviceB;
        private readonly int _n;
        private int _countm = 0;
        public ControllerX(IServiceA serviceA, IServiceB serviceB, INumberService numberService)
        {
            _n = numberService.GetNumber();
            Console.WriteLine($"ctor {nameof(ControllerX)}, {_n}");
            _serviceA = serviceA;
            _serviceB = serviceB;
        }

        public void M()
        {
            Console.WriteLine($"invoked {nameof(M)} for the {++_countm}. time");
            _serviceA.A();
            _serviceB.B();
        }

        public void Dispose() => Console.WriteLine($"disposing {nameof(ControllerX)}, {_n}");
    }
}
