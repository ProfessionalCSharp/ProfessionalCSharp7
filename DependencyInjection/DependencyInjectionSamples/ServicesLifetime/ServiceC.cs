using System;

namespace ServicesLifetime
{
    public class ServiceC : IServiceC, IDisposable
    {
        private bool _isDisposed = false;
        private readonly int _n;
        public ServiceC(INumberService numberService)
        {
            _n = numberService.GetNumber();
            Console.WriteLine($"ctor {nameof(ServiceC)}, {_n}");
        }

        public void C()
        {
            if (_isDisposed)
                throw new ObjectDisposedException("ServiceC");

            Console.WriteLine($"{nameof(C)}, {_n}");
        }
        public void Dispose()
        {
            Console.WriteLine($"disposing {nameof(ServiceC)}, {_n}");
            _isDisposed = true;
        }
    }
}
