using System;

namespace UsingStatic
{
    class Resource : IDisposable
    {
        public void Foo() => Console.WriteLine("Foo");

        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Console.WriteLine("release resource");
                }
                disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);
    }
}
